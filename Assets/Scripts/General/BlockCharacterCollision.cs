using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class BlockCharacterCollision : MonoBehaviour
    {
        public CharacterController characterController;
        public CapsuleCollider characterBlockCollider;


        private void Awake()
        {
             Physics.IgnoreCollision(characterController, characterBlockCollider, true);
        }
    }
}
