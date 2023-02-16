using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonPlayerManager : MonoBehaviourPunCallbacks
{
    public TeamEditorManager manager;
    int playersReady;

    public void SetReadyButton()
    {
        
            photonView.RPC("SetReady", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void SetReady()
    {

        playersReady++;
        if (playersReady >= 2)
        {
                manager.StartCombat();
        }


    }
}
