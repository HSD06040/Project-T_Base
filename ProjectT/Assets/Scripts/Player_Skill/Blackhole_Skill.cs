using UnityEngine;
using UnityEngine.UI;

public class Blackhole_Skill : Skill
{
    [SerializeField] private UI_SkillTreeSlot blackholeUnlockButton;
    public bool blackholeUnlocked {  get; private set; }
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float cloneAttackCooldown;
    [SerializeField] private int amountOfAttack;
    [Space]
    [SerializeField] private float maxSize;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float growSpeed;
    [SerializeField] private float blackholeDuration;


    Blackhole_Skill_Controller currentBlackhole;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    private void UnlockBlackhole()
    {
        if (blackholeUnlockButton.unlocked)
            blackholeUnlocked = true;
    }
    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackhole = Instantiate(blackholePrefab,player.transform.position,Quaternion.identity);

        currentBlackhole = newBlackhole.GetComponent<Blackhole_Skill_Controller>();

        currentBlackhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttack, cloneAttackCooldown, blackholeDuration);
    }

    protected override void CheckUnlock()
    {
        UnlockBlackhole();
    }

    protected override void Start()
    {
        base.Start();

        blackholeUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBlackhole);
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool SkillCompleted()
    {
        if (!currentBlackhole)
            return false;

        if(currentBlackhole.playerCanExitState)
        {
            currentBlackhole = null;
            return true;
        }
        return false;
    }
    public float GetBlackholeRadius()
    {
        return maxSize / 2;
    }
}
