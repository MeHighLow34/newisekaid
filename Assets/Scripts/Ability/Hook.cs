using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

namespace LastIsekai
{
    public class Hook : MonoBehaviour
    {
        [Header("Variation One")]
        public string[] tagsCheck;
        public float speed, returnSpeed, range, stopRange;

        public Transform caster, collidedWith;
        private LineRenderer line;
        private bool hasCollided;

        [Header("Custom")]
        public Vector3 playerDirection;
        public PhotonView playerPV;
        public PhotonView hookPV;



        [Header("Variation Two")]
        public float zOffset = 1f;
        public float hookRange = 20f;
        public float hookStopRange = 2.5f;
        private float elapsedTime;
        public float desiredDuration = 5f;
        bool canKill;
        public Transform destinationPoint;
        private GameObject playerBody;


        [Header("Hook across Network")]
        public int collidedViewId;
        public PhotonView myPhotonView;
        private void Awake()
        {
            hookPV = GetComponent<PhotonView>();
        }
        private void Start()
        {
            myPhotonView = GetLocalPhotonView();
            line = GetComponentInChildren<LineRenderer>();
            playerPV = GameObject.Find("LocalBody").GetComponent<Mediary>().mainPhotonView;
            if (hookPV.IsMine == playerPV.IsMine)
            {
                playerBody = playerPV.gameObject.GetComponent<Mediary>().playerBody;
                playerDirection = playerBody.transform.forward;
            }
            destinationPoint = playerBody.transform.Find("Hook Destination Point").transform;
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, caster.position);
            line.SetPosition(0, caster.position + new Vector3(0, 1f, 0));
            line.SetPosition(1, transform.position);
            if (hasCollided)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerBody.transform.position, Time.deltaTime * speed);
                if(distance < hookStopRange && canKill)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            else
            {
                if(distance > hookRange)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            if (hasCollided == false)
            {
                transform.Translate(playerDirection * speed * Time.deltaTime);
            }
            if(collidedWith != null)
            {
                #region Object Pull Code
                #region Old Movement Code

                /*                float distanceBetweenObjects = Vector3.Distance(collidedWith.transform.position, caster.transform.position);
                                Vector3 finalPosition = new Vector3(destinationPoint.position.x, collidedWith.position.y, destinationPoint.position.z);             
                                elapsedTime += Time.deltaTime;
                                float percentageComplete = elapsedTime / desiredDuration;
                                collidedWith.transform.position = Vector3.Lerp(collidedWith.transform.position, finalPosition, percentageComplete);
                                if(distanceBetweenObjects <= zOffset)
                                {
                                    collidedWith = null;
                                }*/
                #endregion
                float distanceBetweenObjects = Vector3.Distance(collidedWith.transform.position, playerBody.transform.position);
                Vector3 newPosition = playerBody.transform.TransformPoint(playerBody.transform.forward);
                elapsedTime += Time.deltaTime;
                Vector3 finalPosition = new Vector3(newPosition.x, collidedWith.position.y, newPosition.z);
                float percentageComplete = elapsedTime / desiredDuration;
                // collidedWith.transform.position = Vector3.Lerp(collidedWith.transform.position, finalPosition, percentageComplete);
                Vector3 directionToMove = finalPosition - collidedWith.transform.position;
                directionToMove = directionToMove.normalized * Time.deltaTime * speed;
                collidedWith.transform.position = collidedWith.transform.position + Vector3.ClampMagnitude(directionToMove, hookStopRange);
                if (distanceBetweenObjects <= hookStopRange)
                {
                    canKill = true;
                    collidedWith = null;
                }
                #endregion
            }
        }
        private void OnTriggerEnter(Collider other)
        {
/*            if (other.gameObject.tag == tagsCheck[0])
            {
                print("I collided with you lmao" + other.transform.name);
                collidedWith = other.transform;
                hasCollided = true;
            }*/
             if(other.gameObject.tag == tagsCheck[1])
             {
                collidedViewId = other.gameObject.GetComponent<Mediary>().mainPhotonView.ViewID;
                print("I collided with player " + collidedViewId);
                GetComponent<PhotonView>().RPC("Pull", RpcTarget.All, collidedViewId, playerBody.transform.TransformPoint(playerBody.transform.forward));
                hasCollided = true;
                canKill = true;
             }
        }


        [PunRPC]
        public void Pull(int viewID, Vector3 hookedDestination)
        {
            if(myPhotonView.ViewID == viewID)
            {
                print("hooked");
                var hookedOfhookedPlayer = myPhotonView.gameObject.GetComponent<Mediary>().hooked;
                hookedOfhookedPlayer.GotHooked(hookedDestination, hookStopRange, speed);
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
