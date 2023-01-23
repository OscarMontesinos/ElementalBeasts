using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LaunchManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToPhotonServer()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
    }

    public override void OnConnected()
    {
        base.OnConnected();
    }
}
