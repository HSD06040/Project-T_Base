using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collition info")]
    public Transform attackCheck;
    public float attackRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask Ground;
    [SerializeField] protected Transform groundCheck2;
    [SerializeField] protected float groundCheckDistance2;

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnockback;

    public int knockbackDir {  get; private set; }

    #region Component
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public CapsuleCollider2D cd { get; private set; }

    public SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }
    #endregion

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onFlipped;

    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Update()
    {

    }

    public virtual void DamageImpect() => StartCoroutine("Knockback");

    public virtual void SlowEntityBy(float _slowPercentage,float _slowDuration)
    {

    }

    public virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }

    public void SetZeroVelocity()
    {
        if (isKnockback)
            return;
       rb.linearVelocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnockback)
            return;
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);

        FlipContoroller(_xVelocity);
    }

    public virtual void KnockbackSetup(Transform _damageDir)
    {
        if (_damageDir.position.x > transform.position.x)
            knockbackDir = -1;
        else if(_damageDir.position.x < transform.position.x)
            knockbackDir = 1;

    }
    protected virtual IEnumerator Knockback()
    {
        isKnockback = true;

        rb.linearVelocity = new Vector2(knockbackDirection.x * knockbackDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
    }

    #region Collision
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, Ground);
    public bool IsGroundDetected2() => Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance2, Ground);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, Ground);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck2.position, new Vector3(groundCheck2.position.x, groundCheck2.position.y - groundCheckDistance2));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }
    #endregion

    #region Flip
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(onFlipped != null)
            onFlipped();
    }
    public void FlipContoroller(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion

    public virtual void Die()
    {

    }
}
