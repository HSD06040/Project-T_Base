using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    [Header("dash")]
    public bool dashUnlocked { get; private set; }
    [SerializeField] private UI_SkillTreeSlot dashUnlockButton;

    [Header("Clone on dash")]
    public bool cloneOnDashUnlocked { get; private set; }
    [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockButton;

    [Header("Clone on arrival")]
    public bool cloneOnArrivalUnlocked { get; private set; }
    [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockButton;

    public override void UseSkill()
    {
        base.UseSkill();
    }

    protected override void Start()
    {
        base.Start();

        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
    }

    protected override void CheckUnlock()
    {
        UnlockDash();
        UnlockCloneOnDash();
        UnlockCloneOnArrival();
    }

    private void UnlockDash()
    {
        if(dashUnlockButton.unlocked)
            dashUnlocked = true;
    }

    private void UnlockCloneOnDash()
    {
        if (cloneOnDashUnlockButton.unlocked)
            cloneOnDashUnlocked = true;
    }
    private void UnlockCloneOnArrival()
    {
        if(cloneOnArrivalUnlockButton.unlocked)
            cloneOnArrivalUnlocked = true;
    }

    public void CloneOnDash()
    {
        if (cloneOnDashUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }
    public void CloneOnArrival()
    {
        if (cloneOnArrivalUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }
}
