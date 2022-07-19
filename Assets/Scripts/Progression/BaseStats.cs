using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        public int level;
        public CharacterClass characterClass;
        public Progression progression;


        private void Start()
        {
           print( GetStat(Stat.Health));
        }
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, level);
        }
    }
}