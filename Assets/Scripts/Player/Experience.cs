using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class Experience : MonoBehaviour
    {
        [Header("Dependencies")]
        public BaseStats baseStats;
        [Header("Exp")]
        public float experiencePoints;
        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();  
        }

        public void GainExp(float expToGain)
        {
            experiencePoints += expToGain;
            baseStats.CheckLevel();
        }
        public float CurrentExperience()
        {
            return experiencePoints;
        }
    }
}
