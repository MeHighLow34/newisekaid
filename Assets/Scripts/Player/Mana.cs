using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class Mana : MonoBehaviour
    {
        public float mana;

        float maxMana;
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            maxMana = baseStats.GetStat(Stat.Mana);
            mana = maxMana;
        }
        public bool ReduceMana(float manaCost)
        {
            if(mana < manaCost)
            {
                print("Not Enough To Cast a Spell");
                return false;
            }
            else
            {
                mana -= manaCost;
                return true;
            }
        }

        public float GetDecimalValue()
        {
            return mana / maxMana;
        }
    }
}
