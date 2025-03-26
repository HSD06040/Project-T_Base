using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Spin info")]
    [SerializeField] private UI_SkillTreeSlot spinUnlockButton;
    [SerializeField] private float hitCooldown = 0.35f;
    [SerializeField] private float spinDuration = 7;
    [SerializeField] private float maxTraveDistance = 2;
    [SerializeField] private float spinGravity = 1;

    [Header("Bounce info")]
    [SerializeField] private UI_SkillTreeSlot bounceUnlockButton;
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;

    [Header("Peirce info")]
    [SerializeField] private UI_SkillTreeSlot pierceUnlockButton;
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Skill info")]
    [SerializeField] private UI_SkillTreeSlot swordUnlockButton;
    public bool swordUnlocked {  get; private set; }
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    [Header("Passive info")]
    [SerializeField] private UI_SkillTreeSlot timeStopUnlockButton;
    public bool timeStopUnlocked {  get; private set; }
    [SerializeField] private UI_SkillTreeSlot vulnrableUnlockButton;
    public bool vulnerableUnlocked {  get; private set; }

    [Header("Aim dots")]
    [SerializeField] private int numberOfDot;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] Transform dotParent;

    private GameObject[] dots;

    private Vector2 finalDir;

    protected override void Start()
    {
        base.Start();

        GenereateDots();
        SetUpGravity();


        swordUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSword);
        pierceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockPierceSword);
        bounceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBounceSword);
        spinUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSpinSword);
        timeStopUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockTimeStop);
        vulnrableUnlockButton.GetComponent<Button>().onClick.AddListener(Unlockvulnerable);
    }

    protected override void CheckUnlock()
    {
        UnlockBounceSword();
        UnlockPierceSword();
        UnlockSpinSword();
        UnlockSword();
        UnlockTimeStop();
        Unlockvulnerable();
    }
    private void SetUpGravity()
    {
        if (swordType == SwordType.Bounce)
        {
            swordGravity = bounceGravity;
        }
        else if (swordType == SwordType.Pierce)
        {
            swordGravity = pierceGravity;
        }
        else if (swordType == SwordType.Spin)
        {
            swordGravity = spinGravity;
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
    }
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        if (swordType == SwordType.Bounce)
            newSwordScript.SetupBounce(true,bounceAmount,bounceSpeed);
        if (swordType == SwordType.Pierce)
            newSwordScript.SetupPierce(pierceAmount);
        if (swordType == SwordType.Spin)
            newSwordScript.SetupSpin(true,maxTraveDistance,spinDuration,hitCooldown);

        newSwordScript.SetupSword(finalDir, swordGravity, player, freezeTimeDuration, returnSpeed);
        
        player.AssignNewSword(newSword);

        DotActive(false);
    }

    #region Unlocked

    private void Unlockvulnerable()
    {
        if(vulnrableUnlockButton.unlocked)
            vulnerableUnlocked = true;
    }
    private void UnlockTimeStop()
    {
        if(timeStopUnlockButton.unlocked)
            timeStopUnlocked = true;
    }
    private void UnlockSword()
    {
        if (swordUnlockButton.unlocked)
        {
            swordType = SwordType.Regular;
            swordUnlocked = true;
        }
    }
    private void UnlockBounceSword()
    {
        if(bounceUnlockButton.unlocked)
            swordType = SwordType.Bounce;
    }
    private void UnlockPierceSword()
    {
        if(pierceUnlockButton.unlocked)
            swordType = SwordType.Pierce;
    }
    private void UnlockSpinSword()
    {
        if(spinUnlockButton.unlocked)
            swordType = SwordType.Spin;
    }


    #endregion

    #region Aim region
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenereateDots()
    {
        dots = new GameObject[numberOfDot];
        for (int i = 0; i < numberOfDot; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
        }
    }
    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;
    }
    #endregion
}
