using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    [Header("Offensive stat")]
    public Stat damage;
    public Stat critChance;
    public Stat critDamage;

    [Header("Defensive stat")]
    public Stat maxHealth;
    public Stat defense;

    [Header("Primary stat")]
    public Stat strength;
    public Stat agility;
    public Stat vitality;
    public Stat luck;

    public int currentHealth;

    public Action onHealthChanged;


    public void DoDamage(CharacterStats enemyStats, float attackPower = 1) => 
        DamageableManager.Instance.CalculateTotalDamage(this, enemyStats,attackPower);

    private void Start()
    {
        currentHealth = DamageableManager.Instance.GetMaxHealth(this);
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die();
        }

        if(onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {
        Debug.Log("ав╬З╫ю╢о╢ы!");
    }
}
