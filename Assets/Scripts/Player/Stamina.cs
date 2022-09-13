using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class Stamina : MonoBehaviour
    {
        public float stamina;
        float maxStamina;
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            maxStamina = baseStats.GetStat(Stat.Stamina);   
            stamina = maxStamina;
        }

        public bool ReduceStamina(float staminaCost)
        {
            if (stamina < staminaCost)
            {
                print("Not Enough Stamina to Perform an Action");
                return false;
            }
            else
            {
                stamina -= staminaCost;
                return true;
            }
        }

        private void Update()
        {
            if (GetDecimalValue() < 1)
            {
                stamina += Time.deltaTime * 7;
            }
        }

        public float GetDecimalValue()
        {
            return stamina / maxStamina;
        }

    }
}
