using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeastSelectorManager : MonoBehaviourPunCallbacks
{
    bool firstPick = true;
    public int maxBeasts;
    int selectionTurn;
    public float maxTime;
    float actualTime;
    public TextMeshProUGUI timeText;

    public GameObject playerPrefab;

    public BeastSelectorPlayer player1;

    public GameObject contentTeam1;
    public GameObject contentTeam2;
    public GameObject baseBeastImage;
    public GameObject baseBeastImageMirror;
    public List<BeastImage> beastImages;




    


    private void Awake()
    {
        actualTime = maxTime;
        Hashtable turnController = new Hashtable();
        turnController.Add("turn", 0);
        PhotonNetwork.LocalPlayer.CustomProperties = turnController;
    }
    void Start()
    {
        GameObject instance = Instantiate(playerPrefab,new Vector3(0,0,0),new Quaternion(0,0,0,0));
        player1 = instance.GetComponent<BeastSelectorPlayer>();
        CreateTeams();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime -= Time.deltaTime;
        timeText.text = actualTime.ToString("F0");
        if(actualTime < 0)
        {
            photonView.RPC("TimeUp", RpcTarget.AllBuffered);
        }
    }
    void CreateTeams()
    {

        maxBeasts = FormatManager.Instance.maxBeasts;

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 0);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 1);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 1);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 0);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 0);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 1);
            if (maxBeasts > 3)
            {
                photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 1);
                photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 0);
                if (maxBeasts > 4)
                {
                    photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 0);
                    photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, 1);
                }

            }
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["turn"] = 1;
            }
        }
    }

    [PunRPC]
    public void CreateBeastImage(int mirrored)
    {
        if (mirrored==0)
        {
            GameObject instance = PhotonNetwork.Instantiate(baseBeastImage.name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            instance.transform.parent = contentTeam1.transform;
            beastImages.Add(instance.GetComponent<BeastImage>());
            
        }
        else
        {
            GameObject instance = PhotonNetwork.Instantiate(baseBeastImageMirror.name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            instance.transform.parent = contentTeam2.transform;
            beastImages.Add(instance.GetComponent<BeastImage>());
        }
    }

    

    public void SelectBeastCall(int beast)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["turn"].Equals(1))
        {
            photonView.RPC("SelectBeast", RpcTarget.AllBuffered, beast);
        }
    }

    [PunRPC]
    void SelectBeast(int beast)
    {
        
            beastImages[selectionTurn].ChangeImageCall(beast);
        
    }

    public void LockButton()
    {
        if (beastImages[selectionTurn].beastSelected)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["turn"].Equals(1))
            {
                UnitData newBeast = new UnitData();
                newBeast.unitGO = beastImages[selectionTurn].beastGO;
                newBeast.unit = beastImages[selectionTurn].beast;
                newBeast.name = beastImages[selectionTurn].beastName;
                newBeast.icon = beastImages[selectionTurn].beastIcon;
                newBeast.image = beastImages[selectionTurn].beastImage;
                player1.team.Add(newBeast);
                photonView.RPC("Lock", RpcTarget.AllBuffered);
            }
        }
    }
    [PunRPC]
    void Lock()
    {
        if (selectionTurn % 2 == 0)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["turn"].Equals(1))
            {
                PhotonNetwork.LocalPlayer.CustomProperties["turn"] = 0;
            }
            else
            {
                PhotonNetwork.LocalPlayer.CustomProperties["turn"] = 1;
            }
        }
        actualTime = maxTime; 
        selectionTurn++;
        if (selectionTurn > beastImages.Count - 1)
        {
            PhotonNetwork.LoadLevel("TeamBuilder");
        }

    }

    [PunRPC]
    public void TimeUp()
    {
        if (beastImages[selectionTurn].beastSelected)
        {
            photonView.RPC("Lock", RpcTarget.AllBuffered);
        }
        else
        {
            SelectBeastCall(0);
        }
    }
}
