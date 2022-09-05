using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TestingKMKLDSF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Cube", transform.position, Quaternion.identity); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
