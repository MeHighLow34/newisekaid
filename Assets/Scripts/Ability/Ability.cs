using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create a New Ability", order = 1)]
    public class Ability : ScriptableObject
    {
        public string name;
        [TextArea]
        public string description;
        public Sprite icon;
        public PlayerClass playerClass;

        public virtual void UseAbility()
        {
            Debug.Log("Using an ability " + name);
        }
    }
}
