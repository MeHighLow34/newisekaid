using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class GreatShieldTimer : MonoBehaviour
    {
       public GreatShieldAbility greatShieldAbility;
        public float coolDown = 10f;
        private float timeElapsed;
        private void Update()
        {
            if (greatShieldAbility.used)
            {
                timeElapsed += Time.deltaTime;  
                if(timeElapsed >= coolDown)
                {
                    print("Now you can use greatshield again");
                    greatShieldAbility.ChangeState();
                }
            }
        }

    }
}