using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace LastIsekai
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create a New Ability/IceAbility", order = 1)]
    public class IceAbility : Ability
    {
      
        public override void UseAbility()
        {
            Debug.Log("Im gonna fucking freeze everyone");
            base.UseAbility();
        }
        private Transform FindInstantiationPoint()
        {
            WeaponManager weaponManager;
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    weaponManager = manager;
                    return weaponManager.transform.Find("Geometry");
                }
            }
            return null;
        }
    }
}
