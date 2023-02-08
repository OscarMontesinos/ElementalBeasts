using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviourPunCallbacks
{
    int playersReady;

    public void SetReadyButton()
    {

    }

    void SetReady()
    {
        playersReady ++;
        if (playersReady >= 2)
        {

        }

    }
}
