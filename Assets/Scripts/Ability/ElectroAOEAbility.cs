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
            if (used)
            {

                Debug.Log("I'm on a fucking cooldown tebralino");
                return;
            }
            var enoughMana = FindObjectOfType<Mana>().ReduceMana(manaCost);
            if (enoughMana == false)
            {
                Debug.Log("Holy sheet i don't have enough");

            }
            else
            {
                used = true;
                playerAttacker = GetPlayerAttacker();
                playerAttacker.HandleAOE("TestingelectroAbilityVFX");
            }
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