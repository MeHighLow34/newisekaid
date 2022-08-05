using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LastIsekai
{
    public class Hotbar : MonoBehaviour
    {
        public delegate void OnAbilityAdded();
        public OnAbilityAdded onAbilityAddedCallback;
        public List<Ability> abilities = new List<Ability>();
        public bool Add(Ability newAbility)
        {
            if(FindAMatch(newAbility) == true)
            {
                print("You already posses this ability");
                return false;
            }
            abilities.Add(newAbility);  
            if(onAbilityAddedCallback != null)
            {
                onAbilityAddedCallback.Invoke();
            }
            return true;
        }

        private bool FindAMatch(Ability newAbility)
        {
            foreach(Ability ability in abilities)
            {
                if(ability == newAbility)
                {
                    return true;
                }
            }
            return false;
        }

        public void Remove(Ability abilityToRemove)
        {
            abilities.Remove(abilityToRemove);
            if (onAbilityAddedCallback != null)
            {
                onAbilityAddedCallback.Invoke();
            }
        }

    }
}