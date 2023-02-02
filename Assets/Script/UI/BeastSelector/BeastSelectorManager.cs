using Photon.Pun;
using System.Collections;
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

    public GameObject contentTeam;
    public GameObject contentTeam1;
    public GameObject contentTeam2;
    public GameObject baseBeastImage;
    public GameObject baseBeastImageMirror;
    public List<BeastImage> beastImages;

    public GameObject defaultUnit;
    public Sprite defaultBeast;
    public Sprite defaultIcon;
    public string defaultBeastName = "Diggeye";

    Dictionary<string, GameObject> beastSheetsDic = new Dictionary<string, GameObject>();


    public bool turn;

    


    private void Awake()
    {
        actualTime = maxTime;
    }
    void Start()
    {
        GameObject instance = PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(0,0,0),new Quaternion(0,0,0,0));
        player1 = instance.GetComponent<BeastSelectorPlayer>();
        beastSheetsDic.Add("beastSheet", baseBeastImage);
        beastSheetsDic.Add("beastSheetMirror", baseBeastImageMirror);
        CreateTeams();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime -= Time.deltaTime;
        timeText.text = actualTime.ToString("F0");
        if(actualTime < 0)
        {
            TimeUp();
        }
    }
    void CreateTeams()
    {
        
        maxBeasts = FormatManager.Instance.maxBeasts;
        if (PhotonNetwork.IsMasterClient)
        {
            contentTeam = contentTeam1;
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImage);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImage);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImage);
            if (maxBeasts > 3)
            {
                photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImage);
                if (maxBeasts > 4)
                {
                    photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImage);
                }

            }
            turn = true;

        }
        else
        {
            contentTeam = contentTeam2;
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam ,baseBeastImageMirror);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImageMirror);
            photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImageMirror);
            if (maxBeasts > 3)
            {
                photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImageMirror);
                if (maxBeasts > 4)
                {
                    photonView.RPC("CreateBeastImage", RpcTarget.AllBuffered, contentTeam, baseBeastImageMirror);
                }

            }
        }
    }

    [PunRPC]
    void CreateBeastImage(string objectToInstantiate)
    {
        GameObject instance = PhotonNetwork.Instantiate(beastSheetsDic[objectToInstantiate], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        instance.transform.parent = contentTeam.transform;
        beastImages.Add(instance.GetComponent<BeastImage>());
    }

    

    public void SelectBeastCall(Sprite beastImage, Sprite beastIcon, string beastName, GameObject unit)
    {
        photonView.RPC("SelectBeast", RpcTarget.AllBuffered, beastImage,  beastIcon, beastName, unit);
    }

    [PunRPC]
    void SelectBeast(Sprite beastImage, Sprite beastIcon, string beastName, GameObject unit)
    {
        if (turn)
        {
            beastImages[selectionTurn].ChangeImage(beastImage, beastIcon, beastName, unit);
        }
    }

    public void LockButton()
    {
        photonView.RPC("Lock", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void Lock()
    {
        if (beastImages[selectionTurn].beastSelected)
        {
            actualTime = maxTime;
            UnitData newBeast = new UnitData();
            newBeast.unitGO = beastImages[selectionTurn].beastGO;
            newBeast.unit = beastImages[selectionTurn].beast;
            newBeast.name = beastImages[selectionTurn].beastName;
            newBeast.icon = beastImages[selectionTurn].beastIcon;
            newBeast.image = beastImages[selectionTurn].beastImage;
            if (turn)
            {
                player1.team.Add(newBeast);
                turn = false; 
            }
            else
            {
                turn = true;
            }
            selectionTurn++;
            if (selectionTurn>beastImages.Count-1)
            {
                PhotonNetwork.LoadLevel("TeamBuilder");
            }
        }
    }
    
    public void TimeUp()
    {
        if (beastImages[selectionTurn].beastSelected)
        {
            photonView.RPC("Lock", RpcTarget.AllBuffered);
        }
        else
        {
            photonView.RPC("SelectBeast", RpcTarget.AllBuffered, defaultBeast, defaultIcon, defaultBeastName, defaultUnit);
            photonView.RPC("Lock", RpcTarget.AllBuffered);
        }
    }
}
