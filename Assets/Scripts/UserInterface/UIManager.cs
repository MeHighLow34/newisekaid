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
                ShowCanvas(inventory);
                HideCanvas(hud);
                return;
            }
            ShowCanvas(hud);
            HideCanvas(inventory);
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

        public void ShowCanvas(CanvasGroup canvas)
        {
            canvas.alpha = 1f;
            canvas.blocksRaycasts = true;
        }

        public void HideCanvas(CanvasGroup canvas)
        {
            canvas.alpha = 0f;
            canvas.blocksRaycasts = false;
        }
    }
}