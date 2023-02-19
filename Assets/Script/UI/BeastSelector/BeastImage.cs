using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastImage : MonoBehaviourPunCallbacks
{
    public int id;
    public GameObject beastGO;
    public Unit beast;
    public string beastName;
    public Sprite beastIcon;
    public Sprite beastImage;
    public TextMeshProUGUI beastText;
    public bool beastSelected;
    public bool mirror;

    public void ChangeImageCall(int beast)
    {
        photonView.RPC("ChangeImage", RpcTarget.AllBuffered, beast);
    }

        [PunRPC]
    void ChangeImage(int beast)
    {
        id = BeastList.beastList[beast].unitID;
        beastGO = BeastList.beastList[beast].unitGO;
        this.beast = BeastList.beastList[beast].unit;
        GetComponent<Image>().sprite = BeastList.beastList[beast].image;
        beastImage = BeastList.beastList[beast].image;
        beastIcon = BeastList.beastList[beast].icon;
        beastText.text = BeastList.beastList[beast].name;
        beastName = BeastList.beastList[beast].name;
        beastSelected = true;
    }
}
