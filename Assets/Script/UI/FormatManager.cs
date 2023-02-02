using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatManager : MonoBehaviourPunCallbacks
{
    public static FormatManager Instance;
    public int maxBeasts;
    public string map;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    public void SetMap(string map)
    {
        photonView.RPC("PunSetMap", RpcTarget.AllBuffered, map);
    }
    public void SetBeasts(int beast)
    {
        photonView.RPC("PunSetBeasts", RpcTarget.AllBuffered, beast);
    }
    public void StartGame(int beast)
    {
        photonView.RPC("PunStartGame", RpcTarget.AllBuffered);
    }


    [PunRPC]
    void PunSetMap(string map)
    {
        this.map = map;
    }

    [PunRPC]
    void PunSetBeasts(int beast)
    {
        this.maxBeasts = beast;
    }
    [PunRPC]
    void PunStartGame()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}


