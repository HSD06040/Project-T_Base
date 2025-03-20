using System;
using UnityEngine;

public class DamageableManager
{
    private int strDamage = 1;
    private int strMaxHealth = 1;

    private int agiDamage = 1;
    private int agiCritDamage = 1;
    private int agiCritChance = 1;

    private int vitMaxHealth = 1;
    private int vitDefense = 1;

    private static WeakReference<DamageableManager> instance;

    private DamageableManager() { }

    public static DamageableManager Instance
    {
        get
        {
            if (instance == null || !instance.TryGetTarget(out var singleton))
            {
                singleton = new DamageableManager();
                instance = new WeakReference<DamageableManager>(singleton);
            }
            return singleton;
        }
    }

    public void CalculateTotalDamage(CharacterStats myStats, CharacterStats enemyStats, float attackPower)
    {
        float totalDamage;

        totalDamage = GetDamage(myStats) * attackPower;

        if (CanCrit(myStats))
        {
            totalDamage *= (GetCritDamage(myStats) / 100);
        }
        totalDamage = CheckTargetDefense(totalDamage, enemyStats);

        enemyStats.DecreaseHealth((int)totalDamage);
    }

    public bool CanCrit(CharacterStats myStats)
    {
        if (GetCritChance(myStats) > UnityEngine.Random.Range(0, 100))
        {
            return true;
        }
        return false;
    }

    public float CheckTargetDefense(float totalDamage, CharacterStats enemyStats)
    {
        totalDamage -= totalDamage * (GetDefense(enemyStats) / (GetDefense(enemyStats) + 50));

        return totalDamage;
    }

    #region Get Stats
    public int GetDamage(CharacterStats Stats)
    {
        return Stats.damage.GetValue() +
              (Stats.strength.GetValue() * strDamage + Stats.agility.GetValue() * agiDamage);
    }

    public float GetDefense(CharacterStats stats)
    {
        return stats.defense.GetValue() +
               stats.vitality.GetValue() * vitDefense;
    }

    public int GetMaxHealth(CharacterStats stats)
    {
        return stats.maxHealth.GetValue() +
               stats.vitality.GetValue() * vitMaxHealth + stats.strength.GetValue() * strMaxHealth;
    }

    public int GetCritChance(CharacterStats stats)
    {
        return stats.critChance.GetValue() +
               stats.agility.GetValue() * agiCritChance;
    }

    public int GetCritDamage(CharacterStats stats)
    {
        return stats.critDamage.GetValue() +
               stats.agility.GetValue() * agiCritDamage;
    }
    #endregion
}
