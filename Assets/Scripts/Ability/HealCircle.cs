using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class HealCircle : MonoBehaviour
    {
        [Header("Network Parameters")]
        public int collidedViewId;
        public PhotonView myPhotonView;

        private void Start()
        {
            myPhotonView = GetLocalPhotonView();
        }
        private void OnTriggerStay(Collider other)
        {
            var victim = other.GetComponent<IDamageable>();
            if (victim == null) return;
            collidedViewId = other.gameObject.GetComponent<Mediary>().mainPhotonView.ViewID;
            GetComponent<PhotonView>().RPC("Heal", RpcTarget.All, collidedViewId);
        }
        [PunRPC]
        public void Heal(int viewID)
        {
            if(myPhotonView.ViewID == viewID)
            {
                print("I should heal" + myPhotonView.Owner.NickName + " " + myPhotonView.Owner.UserId + " " + myPhotonView.ViewID + " " + myPhotonView.name);
                var healther = myPhotonView.GetComponent<Mediary>().healther;
                float speed = 2f;
                healther.health += Time.deltaTime * speed;
            }
        }


        private PhotonView GetLocalPhotonView()
        {
            WeaponManager[] wholeWM = FindObjectsOfType<WeaponManager>();
            foreach (var manager in wholeWM)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager.gameObject.GetComponentInParent<PhotonView>();
                }
            }
            return null;
        }
    }
}