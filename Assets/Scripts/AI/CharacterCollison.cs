using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class CharacterCollison : MonoBehaviour
    {
        public CapsuleCollider characterCollider;
        public CapsuleCollider characterBlockerCollider;

/*        private void Start()
        {
            do
            {
                Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
            }while(characterCollider.enabled == false || characterBlockerCollider.enabled == false);
        }*/

        public void IgnoreCollision()
        {
            Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
        }
    }
}