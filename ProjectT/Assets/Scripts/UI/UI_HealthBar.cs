using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity;
    private Slider slider;
    private RectTransform myTransform;
    private CharacterStats myStats;


    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        myStats = GetComponentInParent<CharacterStats>();
        myTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
    }
    private void Start()
    { 
        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHealth;
    }
    private void FlipUI()=> myTransform.Rotate(0, 180, 0);

    private void OnEnable()
    {
        entity.onFlipped -= FlipUI;
        myStats.onHealthChanged -= UpdateHealthUI;
    } 



}
