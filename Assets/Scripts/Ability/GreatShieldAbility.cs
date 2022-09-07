using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create a New Ability/GreatShieldAbility", order = 1)]
    public class GreatShieldAbility : Ability
    {
        PlayerAttacker playerAttacker;
        public override void UseAbility()
        {
            playerAttacker = GetPlayerAttacker();
            playerAttacker.HandleAOE("GreatShield");
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

