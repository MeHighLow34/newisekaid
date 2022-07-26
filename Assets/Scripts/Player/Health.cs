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
        float damage;
        public BaseStats stats;
        public MMFeedbacks hitFeedback;
        public AnimationManager animationManager;
        public AnimationEvents animationEvents;
        private bool isDead;
        private float privateHealth;
        public int hitAnimation;
        [Header("Visual Effects")]
        public Volume hurtVisual;
        public Transform bloodInstantiationPoint;
        public GameObject floatingText;
        [Header("Network")]
        public PhotonView view;
        public PhotonView damageDetectorPhotonView;
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
            if (view.IsMine)
            {
                
                if (health < privateHealth)
                {
                    damage = privateHealth - health;
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
        public void HitReaction()
        {
         //   if(animationEvents != null) animationEvents.DisableWeaponCollider(); // when we are hit we don't damage anyone
            if (hitAnimation != 99) // Hold Damage
            {
                animationManager.animator.SetInteger("hitIndex", hitAnimation);
                animationManager.animator.SetBool("hit", true);
                animationManager.animator.SetBool("isInteracting", true);
                animationManager.animator.applyRootMotion = true;
                PhotonNetwork.Instantiate("BloodVFX", bloodInstantiationPoint.position, Quaternion.identity);
                PhotonNetwork.Instantiate("Spark", bloodInstantiationPoint.position, Quaternion.identity);
            }
            var floatingDamage = Instantiate(floatingText, bloodInstantiationPoint.position, Quaternion.identity);
            floatingDamage.GetComponent<FloatingDamage>().SetText(damage.ToString());
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
        

        [PunRPC]
        void HealAfterBlock(int viewID)
        {
            if(view.ViewID == viewID)
            {
                if (view.IsMine)
                {
                    // HitReaction();
                    health += 0.5f;
                }
            }
        }

        public void ChangeHitAnimation(int change, int viewID)
        {
            hitAnimation = change;
            view.RPC("HitAnimation", RpcTarget.All, change, viewID);
        }

        [PunRPC]
        public void HitAnimation(int change, int viewID)
        {
            if(view.ViewID == viewID)
            {
                hitAnimation = change;
            }
        }

        public void BlockReaction()
        {
                view.RPC("HealAfterBlock", RpcTarget.All, view.ViewID);
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