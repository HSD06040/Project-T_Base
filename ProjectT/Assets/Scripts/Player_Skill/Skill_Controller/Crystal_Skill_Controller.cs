using UnityEditor.Build;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private Player player;
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();

    private float crystalExistTimer;
    private bool canExplode;
    private bool canMove;
    private float moveSpeed;
    private bool canGrow;
    private float growSpeed = 5;
    private Transform closestTarget;

    [SerializeField] private LayerMask WhatIsEnemy;
    public void SetupCrystal(float _crystalDuration,bool _canExplode,bool _canMove,float _moveSpeed,Transform _closestTarget, Player _player)
    {
        player = _player;
        crystalExistTimer = _crystalDuration;
        canExplode = _canExplode;
        canMove = _canMove;
        moveSpeed = _moveSpeed;
        closestTarget = _closestTarget;
    }

    private void Update()
    {
        crystalExistTimer -= Time.deltaTime;

        if (crystalExistTimer < 0)
        {
            FinishiCrystal();
        }

        if (canMove)
        {
            if (closestTarget != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, closestTarget.position) < 1)
                {
                    FinishiCrystal();
                    canMove = false;
                }
            }
        }

        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growSpeed * Time.deltaTime);
    }
    public void FinishiCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            anim.SetTrigger("Explode");
        }
        else
            SelfDestroy();
    }

    private void AnimationExplodeTrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position,cd.radius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Entity>().KnockbackSetup(transform);

                player.stats.DoMagicalDamage(hit.GetComponent<CharacterStats>());

                ItemData_Equipment equipmentAmulet = Inventory.Instance.GetEquipment(EquipmentType.Amulet);

                if (equipmentAmulet != null)
                    equipmentAmulet.Effect(hit.transform);
            }
        }
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManager.instance.blackhole.GetBlackholeRadius();

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, radius, WhatIsEnemy);

        if (collider.Length > 0)
            closestTarget = collider[Random.Range(0, collider.Length)].transform;
    }
    public void SelfDestroy() => Destroy(gameObject);
}
