using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LastIsekai
{
    public class HotbarSlot : MonoBehaviour, IDragHandler, IDropHandler,IEndDragHandler, IBeginDragHandler
    {
        private CanvasGroup canvasGroup;
        private Vector2 startAnchoredPosition;
        private RectTransform rectTransform;
        private Canvas canvas;
        public Ability ability;
        [Header("UI")]
        public Image iconHolder;

        public Sprite defaultImage;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            canvas = transform.parent.parent.GetComponent<Canvas>();
        }

        private void Start()
        {
            startAnchoredPosition = rectTransform.anchoredPosition; 
        }
        public void AddAbility(Ability newAbility)
        {
            ability = newAbility;
            iconHolder.sprite = ability.icon;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if(eventData.pointerDrag != null)
            {
                HotbarSlot hotbarSlot = eventData.pointerDrag.GetComponent<HotbarSlot>();
                if(hotbarSlot != null)
                {
                    Ability firstElement = hotbarSlot.ability;
                    Ability secondElement = GetAbility();
                    if(secondElement == null)
                    {
                        AddAbility(firstElement);
                        hotbarSlot.Remove();
                    }
                    else
                    {
                        AddAbility(firstElement);
                        hotbarSlot.AddAbility(secondElement);
                    }
                }
            }
        }

        public Ability GetAbility()
        {
            return ability;
        }
        public void Remove()
        {
            ability = null;
            iconHolder.sprite = defaultImage;   
        }

        public void Use()
        {
            ability.UseAbility();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition = startAnchoredPosition;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
