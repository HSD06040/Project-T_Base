using Unity.Cinemachine;
using UnityEngine;

public class PlayerFX : EntityFX
{
    [Space]
    [Header("Screen shake FX")]
    private CinemachineImpulseSource screenShake;
    [SerializeField] private float shakeMultiplier;
    public Vector3 shakeSwordPower;
    public Vector3 shakeHighDamage;

    [Header("After image fx")]
    [SerializeField] private GameObject afterimagePrefab;
    [SerializeField] private float colorLooseRate;
    [SerializeField] private float afterImageCooldown;
    private float afterImageTimer;

    [Space]
    [SerializeField] private ParticleSystem dustFx;

    protected override void Start()
    {
        base.Start();
        screenShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        afterImageTimer -= Time.deltaTime;
    }

    public void CreateAfterImage()
    {
        if (afterImageTimer < 0)
        {
            afterImageTimer = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterimagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorLooseRate, sr.sprite);
        }
    }
    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }

    public void PlayDustFX()
    {
        if (dustFx != null)
            dustFx.Play();
    }

}
