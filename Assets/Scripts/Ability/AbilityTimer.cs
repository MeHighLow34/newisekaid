using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class AbilityTimer : MonoBehaviour
    {
        public Ability ability;
        public float coolDown = 10f;
        private float timeElapsed;

        private void Update()
        {
            if (ability.used)
            {
                timeElapsed += Time.deltaTime;
                if(timeElapsed >= coolDown)
                {
                    timeElapsed = 0;
                    ability.ChangeState();
                }
            }
        }
    }
}
