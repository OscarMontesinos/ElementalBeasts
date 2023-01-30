using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameInputManager : MonoBehaviour
{
    TMP_InputField nameIF;
    private void Awake()
    {
        nameIF = GetComponent<TMP_InputField>();
    }
    public void SetPlayerName()
    {
        if (string.IsNullOrEmpty(nameIF.text))
        {
            return;
        }

        PhotonNetwork.NickName = nameIF.text;
    }
}
