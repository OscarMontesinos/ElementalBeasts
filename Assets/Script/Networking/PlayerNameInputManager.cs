using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameInputManager : MonoBehaviour
{
    public void SetPlayerName(string playername)
    {
        if (string.IsNullOrEmpty(playername))
        {
            return;
        }

        PhotonNetwork.NickName = playername;
    }
}
