using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class Health : MonoBehaviour, IDamageable
    {
        public float health;
        float maxHealth;
        public BaseStats stats;

        private void Awake()
        {
            maxHealth = stats.GetStat(Stat.Health);
            health = maxHealth;
        }

        public float GetDecimal()
        {
            return health / maxHealth;
        }
        public void HandleDeath()
        {
            print("Dead boih");
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                HandleDeath();
            }
        }
    }
}