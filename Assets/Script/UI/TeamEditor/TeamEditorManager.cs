using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamEditorManager : MonoBehaviourPunCallbacks
{
    bool sheets;
    public GameObject placeToSpawn;

    public GameObject beastSheet;

    public GameObject playerPrefab;
    public BeastSelectorPlayer beastSelectorPlayer;
    public BeastSelectorPlayer beastSelectorPlayer2;

    public List<GameObject> beastsList;

    public int maxBeasts;

    GameObject newPlayerGo;
    GameObject newPlayerGo2;
    Player newPlayer;
    Player newPlayer2;
    private void Awake()
    {
        maxBeasts = FormatManager.Instance.maxBeasts;
        beastSelectorPlayer = FindObjectOfType<BeastSelectorManager>().player1;
        beastSelectorPlayer2 = FindObjectOfType<BeastSelectorManager>().player2;
        Destroy(FindObjectOfType<BeastSelectorManager>().gameObject);
    }

    private void Start()
    {
        photonView.RPC("CreateBeastSheets", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void CreateBeastSheets()
    {
            int beast = 0;
            while (beast < maxBeasts)
            {
                GameObject sheet = PhotonNetwork.Instantiate(beastSheet.name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                sheet.transform.parent = placeToSpawn.transform;
                sheet.GetComponent<BeastSheet>().beastSlot = beast;
                beast++;
            }
            sheets = true;
    }
    public void StartCombat()
    {
        photonView.RPC("StartCombatPhoton", RpcTarget.AllBuffered);
    }



    [PunRPC]
    void StartCombatPhoton()
    {
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.LoadLevel(FormatManager.Instance.map);
        if (photonView.IsMine)
        {
            newPlayerGo = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            DontDestroyOnLoad(newPlayerGo);
            newPlayer = newPlayerGo.GetComponent<Player>();
        
        int index = 0;
        while (index < FormatManager.Instance.maxBeasts)
        {
            photonView.RPC("CreateBeast",RpcTarget.AllBuffered,index);
            index++;
        }
        if (!PhotonNetwork.IsMasterClient)
        {
            newPlayer.team = 1;
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            newPlayer2.team = 1;
        }
        Destroy(beastSelectorPlayer.gameObject);
        Destroy(beastSelectorPlayer2.gameObject);
        Destroy(gameObject);}
    }

    [PunRPC]
    void CreateBeast(int index)
    {
        if (photonView.IsMine)
        {
            GameObject beast = PhotonNetwork.Instantiate(beastSelectorPlayer.team[index].name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            DontDestroyOnLoad(beast);
            Items items = beast.GetComponent<Items>();
            Unit unit = beast.GetComponent<Unit>();
            if (beastSelectorPlayer.team[index].item1Selected != items.items.Count - 1)
            {
                items.items[beastSelectorPlayer.team[index].item1Selected].equipado = true;
                items.items[beastSelectorPlayer.team[index].item1Selected].cantidad++;
            }
            if (beastSelectorPlayer.team[index].item2Selected != items.items.Count - 1)
            {
                items.items[beastSelectorPlayer.team[index].item2Selected].equipado = true;
                items.items[beastSelectorPlayer.team[index].item2Selected].cantidad++;
            }

            unit.chosenHab1 = beastSelectorPlayer.team[index].hab1Selected;
            unit.chosenHab2 = beastSelectorPlayer.team[index].hab2Selected;
            unit.chosenHab3 = beastSelectorPlayer.team[index].hab3Selected;
            unit.chosenHab4 = beastSelectorPlayer.team[index].hab4Selected;

            newPlayer.beastsToPlace.Add(beast);
        }
        else
        {
            GameObject beast = PhotonNetwork.Instantiate(beastSelectorPlayer2.team[index].name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            DontDestroyOnLoad(beast);
            Items items = beast.GetComponent<Items>();
            Unit unit = beast.GetComponent<Unit>();
            if (beastSelectorPlayer2.team[index].item1Selected != items.items.Count - 1)
            {
                items.items[beastSelectorPlayer2.team[index].item1Selected].equipado = true;
                items.items[beastSelectorPlayer2.team[index].item1Selected].cantidad++;
            }
            if (beastSelectorPlayer2.team[index].item2Selected != items.items.Count - 1)
            {
                items.items[beastSelectorPlayer2.team[index].item2Selected].equipado = true;
                items.items[beastSelectorPlayer2.team[index].item2Selected].cantidad++;
            }

            unit.chosenHab1 = beastSelectorPlayer2.team[index].hab1Selected;
            unit.chosenHab2 = beastSelectorPlayer2.team[index].hab2Selected;
            unit.chosenHab3 = beastSelectorPlayer2.team[index].hab3Selected;
            unit.chosenHab4 = beastSelectorPlayer2.team[index].hab4Selected;

            newPlayer2.beastsToPlace.Add(beast);
        }
    }
}
