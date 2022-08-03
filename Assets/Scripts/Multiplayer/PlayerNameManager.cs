using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
namespace LastIsekai
{
    public class PlayerNameManager : MonoBehaviour
    {
        [SerializeField] TMP_InputField usernameInput;

        public void OnUsernameInputValueChanged()
        {
            PhotonNetwork.NickName = usernameInput.text;    
        }
    }
}