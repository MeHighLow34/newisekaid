using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastIsekai
{
    public class UIManager : MonoBehaviour
    {
        [Header("Canvases")]
        public CanvasGroup hud;
        public CanvasGroup inventory;
        [Header("Bools")]
        public bool inventoryEnabled;
        private void Update()
        {
            #region Input
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inventoryEnabled = !inventoryEnabled;
            }
            #endregion // Will update everything later when we are finishing the game
            if (inventoryEnabled)
            {
                EnableCursor();
                hud.alpha = 0f;
                inventory.alpha = 1f;
                hud.blocksRaycasts = false;
                inventory.blocksRaycasts = true;
                return;
            }

            hud.alpha = 1f;
            inventory.alpha = 0f;
            inventory.blocksRaycasts = false;
            hud.blocksRaycasts = true;
            DisableCursor();
        }

        private void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }
        private void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}