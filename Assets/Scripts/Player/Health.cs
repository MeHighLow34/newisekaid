using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using MoreMountains.Feedbacks;
using UnityEngine.Rendering;

namespace LastIsekai
{
    public class Health : MonoBehaviourPunCallbacks, IDamageable, IPunObservable
    {
        public float health;
        float maxHealth;
        public BaseStats stats;
        public PhotonView view;
        public MMFeedbacks hitFeedback;
        public AnimationManager animationManager;
        private bool isDead;
        private float privateHealth;
        [Header("Visual Effects")]
        public Volume hurtVisual;
        public Transform bloodInstantiationPoint;

        private void Awake()
        {
            hurtVisual = GameObject.FindGameObjectWithTag("HurtVISUAL").GetComponent<Volume>();
            maxHealth = stats.GetStat(Stat.Health);
            health = maxHealth;
            privateHealth = health;
            view = GetComponent<PhotonView>();
            hitFeedback = GetComponentInChildren<MMFeedbacks>();
            animationManager = GetAnimationManager();
        }


        void Start()
        {
            animationManager = GetAnimationManager();
        }


        void Update()
        {
            //testing
            if (Input.GetKeyDown(KeyCode.H))
            {
                PhotonNetwork.Instantiate("FloatingDamage", bloodInstantiationPoint.position, Quaternion.identity);
            }
        }
        void LateUpdate()
        {
            if (view.IsMine)
            {
                
                if (health != privateHealth)
                {
                    float damage = privateHealth - health;
                    if (damage == 0.5)
                    {
                        BlockReaction();
                    }
                    else
                    {
                        HitReaction();
                    }
                    privateHealth = health;
                }
                VisualEffects();
                if(privateHealth<=0 && isDead==false)
                {
                  //  animationManager.animator.SetBool("isDead", true);
                  //  animationManager.animator.SetBool("isInteracting", true);
                    isDead = true;
                }
            }
        }

        
        private void VisualEffects()
        {
            if (GetDecimal() <= 0.35)
            {
                hurtVisual.weight = 1f;
            }
            else
            {
                hurtVisual.weight = 0f;
            }
        }
        private void HitReaction()
        {
            animationManager.animator.SetInteger("hitIndex", Random.Range(0, 6));
            animationManager.animator.SetBool("hit", true);
            animationManager.animator.SetBool("isInteracting", true);
            animationManager.animator.applyRootMotion = true;
            PhotonNetwork.Instantiate("BloodVFX", bloodInstantiationPoint.position, Quaternion.identity);
            PhotonNetwork.Instantiate("Spark", bloodInstantiationPoint.position, Quaternion.identity);
            PhotonNetwork.Instantiate("FloatingDamage", bloodInstantiationPoint.position, Quaternion.identity);
        }
        public float GetDecimal()
        {
            return health / maxHealth;
        }
        public void HandleDeath()
        {
            print("Dead boih");
        }

        public void TakeDamage(float damage)
        {
            view.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        }
        [PunRPC]
        void RPC_TakeDamage(float damage)
        {
            if (view.IsMine == false) return;
            health -= damage;
            if (health <= 0)
            {
                HandleDeath();
            }
        }
        
        private void BlockReaction()
        {
                animationManager.animator.SetBool("blockImpact", true);
                var blockedText = PhotonNetwork.Instantiate("FloatingText", bloodInstantiationPoint.position, Quaternion.identity);
                blockedText.GetComponent<FloatingDamage>().SetText("Blocked!");
        }
        public AnimationManager GetAnimationManager()
        {
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    print("I found it");
                    return manager.gameObject.GetComponent<AnimationManager>();
                }
            }
            return null;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(health);
            }
            else if(stream.IsReading)
            {
                health = (float)stream.ReceiveNext();
            }
        }
    }
}