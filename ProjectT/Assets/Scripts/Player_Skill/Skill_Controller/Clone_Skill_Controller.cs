using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private Player player;
    private SpriteRenderer sr;
    private Animator anim;

    private float attackMultiplier;

    [SerializeField] private float colorLoosingSpeed;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackRadius;
    private Transform closestEnemy;
    private int facingDir=1;

    private bool canDuplicateClone;
    private float chanceToDuplicate;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
        }
        if (sr.color.a <= 0)
        {
            Destroy(gameObject);
        }


    }
    public void SetUpClone(Transform _newTransform, float cloneDuration, bool _canAttack, Vector3 _offset,
        Transform _closestEnemy, bool _canDuplicate, float _chanceToDuplicate, Player _player, float _attackMultiplier)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 4));

        player = _player;

        transform.position = _newTransform.position + _offset;
        cloneTimer = cloneDuration;
        closestEnemy = _closestEnemy;

        canDuplicateClone = _canDuplicate;
        chanceToDuplicate = _chanceToDuplicate;
        attackMultiplier = _attackMultiplier;

        FaceClosestTarget();
    }
    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Entity>().KnockbackSetup(transform);

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();

                playerStats.CloneDoDamage(enemyStats, attackMultiplier);

                if(player.skill.clone.canApplyOnHitEffect)
                {
                    ItemData_Equipment equipmentWeapon = Inventory.Instance.GetEquipment(EquipmentType.Weapon);

                    if (equipmentWeapon != null)
                        equipmentWeapon.Effect(hit.transform);
                }


                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < chanceToDuplicate)
                    {
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(1.5f*facingDir, 0));
                    }
                }
            }
        }
    }
    private void FaceClosestTarget()
    {
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
