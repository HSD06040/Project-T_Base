using UnityEngine;
using UnityEngine.UI;

public class Dodge_Skill : Skill
{
    [Header("Dodge")]
    [SerializeField] private UI_SkillTreeSlot unlockDodgeButton;
    [SerializeField] private int evasionAmount;
    public bool dodgeUnlocked {  get; private set; }

    [Header("Mirage dodge")]
    public bool dodgeMirageUnlocked;
    [SerializeField] private UI_SkillTreeSlot unlockDodgeMirageButton;

    protected override void Start()
    {
        base.Start();
        unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        unlockDodgeMirageButton.GetComponent<Button>().onClick.AddListener(UnlockDodgeMirage);
    }

    protected override void CheckUnlock()
    {
        UnlockDodge();
        UnlockDodgeMirage();
    }
    public void UnlockDodge()
    {
        if(unlockDodgeButton.unlocked && !dodgeUnlocked)
        {
            player.stats.evasion.AddModifier(evasionAmount);
            Inventory.Instance.UpdateStatUI();
            dodgeUnlocked = true;
        }
    }

    public void UnlockDodgeMirage()
    {
        if(unlockDodgeMirageButton.unlocked)
            dodgeMirageUnlocked = true;
    }

    public void CreateMirageOnDodge()
    {
        if (dodgeMirageUnlocked)
            player.skill.clone.CreateClone(player.transform, new Vector3(2*player.facingDir,0));
    }
}
