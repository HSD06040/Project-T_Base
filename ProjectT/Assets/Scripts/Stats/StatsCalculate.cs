using UnityEngine;
using UnityEngine.TextCore.Text;

namespace StatsCalculator
{
    public static class StatsCalculate
    {
        static int strDamage     = 1;
        static int strMaxHealth  = 1;

        static int agiDamage     = 1;
        static int agiCritDamage = 1;
        static int agiCritChance = 1;

        static int vitMaxHealth  = 1;
        static int vitDefense    = 1;

        public static void CalculateTotalDamage(CharacterStats myStats, CharacterStats enemyStats, float attackPower)
        {
            float totalDamage;

            totalDamage = GetDamage(myStats) * attackPower;

            if(CanCrit(myStats))
            {
                totalDamage *= (GetCritDamage(myStats) / 100);
            }
            totalDamage = CheckTargetDefense(totalDamage, enemyStats);

            enemyStats.DecreaseHealth((int)totalDamage);
        }

        public static bool CanCrit(CharacterStats myStats)
        {
            if(GetCritChance(myStats) > Random.Range(0,100))
            {
                return true;
            }
            return false;
        }

        public static float CheckTargetDefense(float totalDamage,CharacterStats enemyStats)
        {
            totalDamage -= totalDamage * (GetDefense(enemyStats) / (GetDefense(enemyStats) + 50));

            return totalDamage;
        }

        #region Get Stats
        public static int GetDamage(CharacterStats Stats)
        {
            return Stats.damage.GetValue() +
                  (Stats.strength.GetValue() * strDamage + Stats.agility.GetValue() * agiDamage);
        }

        public static float GetDefense(CharacterStats stats)
        {
            return stats.defense.GetValue() + 
                   stats.vitality.GetValue() * vitDefense; 
        }

        public static int GetMaxHealth(CharacterStats stats)
        {
            return stats.maxHealth.GetValue() +
                   stats.vitality.GetValue() * vitMaxHealth + stats.strength.GetValue() * strMaxHealth;
        }

        public static int GetCritChance(CharacterStats stats)
        {
            return stats.critChance.GetValue() +
                   stats.agility.GetValue() * agiCritChance;
        }

        public static int GetCritDamage(CharacterStats stats)
        {
            return stats.critDamage.GetValue() +
                   stats.agility.GetValue() * agiCritDamage;
        }
        #endregion
    }

}

    