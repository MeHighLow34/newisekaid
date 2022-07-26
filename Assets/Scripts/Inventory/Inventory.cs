using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace LastIsekai
{
    public class Inventory : MonoBehaviour
    {
        #region Singleton
        PhotonView view;
        public static Inventory instance;
        private void Awake()
        {
            view = GetComponentInParent<PhotonView>();
            if (!view.IsMine)
            {
                Destroy(this);
            }
            if (view.IsMine)
            {
                if (instance != null)
                {
                    Debug.LogWarning("More than one instance of Inventory");
                }
                instance = this;
            }
        }

        private void Start()
        {
            if (view.IsMine)
            {
                instance = this;
            }
        }
        #endregion
        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;
        public List<Item> items = new List<Item>();
        public int space = 40;
        public bool Add(Item item)
        {
            if(items.Count >= space)
            {
                print("Not enough room");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            return true;
        }

        public void Remove(Item item)
        {
            items.Remove(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }
}
