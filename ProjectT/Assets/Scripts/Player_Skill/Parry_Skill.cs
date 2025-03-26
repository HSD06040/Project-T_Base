using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked {  get; private set; }

    [Header("Parry restore")]
    [SerializeField] private UI_SkillTreeSlot parryRestoreUnlcokButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthAmount;
    public bool restoreUnlocked { get; private set; }

    [Header("Parry with mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
    public bool parryWithMirageUnlocked { get; private set; }


    protected override void Start()
    {
        base.Start();
        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        parryRestoreUnlcokButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }

    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();
    }
    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthAmount);
            player.stats.IncreaseHealthBy(restoreAmount);
        }
    }

    public void UnlockParry()
    {
        if(parryUnlockButton.unlocked)
            parryUnlocked = true;
    }

    public void UnlockParryRestore()
    {
        if(parryRestoreUnlcokButton.unlocked)
            restoreUnlocked = true;
    }

    public void UnlockParryWithMirage()
    {
        if(parryWithMirageUnlockButton.unlocked)
            parryWithMirageUnlocked = true;
    }

    public void MakeMirageOnParry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked)
            player.skill.clone.CreateCloneWithDelay(_respawnTransform);
    }
}
