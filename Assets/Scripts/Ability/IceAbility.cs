using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace LastIsekai
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create a New Ability/IceAbility", order = 1)]
    public class IceAbility : Ability
    {
        PlayerAttacker playerAttacker;
        public override void UseAbility()
        {
            playerAttacker = GetPlayerAttacker();
            Debug.Log("Im gonna fucking freeze everyone");
            base.UseAbility();
            playerAttacker.HandleAOE("iceAbilityVFX");
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
