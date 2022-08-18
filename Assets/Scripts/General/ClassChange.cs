using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class ClassChange : Interactable
    {
        public PlayerClass cls;
        public override void Interact()
        {
            ChangeClass();
            base.Interact();
        }
        private bool ChangeClass()
        {
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    PlayerManager localPlayerManager = manager.gameObject.GetComponent<PlayerManager>();
                    localPlayerManager.playerClass = cls;
                    return true;
                }
            }
            return false;
        }
    }
}
