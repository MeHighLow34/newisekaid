using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create a New Ability/ElectroAOEAbility", order = 1)]
    public class ElectroAOEAbility : Ability
    {
        PlayerAttacker playerAttacker;
        public override void UseAbility()
        {
            playerAttacker = GetPlayerAttacker();
            playerAttacker.HandleAOE("electroAbilityVFX");
        }
        private PlayerAttacker GetPlayerAttacker()
        {

            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager.gameObject.GetComponent<PlayerAttacker>();
                }
            }
            return null;
        }
    }
}