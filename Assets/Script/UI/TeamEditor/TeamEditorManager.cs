using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamEditorManager : MonoBehaviourPunCallbacks
{
    int sorted=1000;
    bool sheets;
    public GameObject placeToSpawn;

    public GameObject beastSheet;

    public GameObject playerPrefab;
    public BeastSelectorPlayer beastSelectorPlayer;

    public List<GameObject> beastsList;

    public int maxBeasts;

    GameObject newPlayerGo;
    private void Awake()
    {
        maxBeasts = FormatManager.Instance.maxBeasts;
        beastSelectorPlayer = FindObjectOfType<BeastSelectorManager>().player1;
        Destroy(FindObjectOfType<BeastSelectorManager>().gameObject);

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("CreateBeastSheets", RpcTarget.AllBuffered);
        }
    }

    private void Update()
    {
        if (sorted > 0)
        {
            SortSheets();
        }
    }
    [PunRPC]
    void CreateBeastSheets()
    {
        int beast = 0;
        while (beast < maxBeasts)
        {
            GameObject sheet = PhotonNetwork.Instantiate(beastSheet.name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            sheet.transform.parent = placeToSpawn.transform;
            beast++;
        }
        sheets = true;
    }

    void SortSheets()
    {
        int count = maxBeasts-1;
        int count2 = maxBeasts-1;
        foreach (BeastSheet sheet in FindObjectsOfType<BeastSheet>())
        {
            if (sheet.transform.parent!=null)
            {
                sheet.beastSlot = count2;
                count2--;

            }
            else
            {
                sheet.beastSlot = count;
                count--;
            }
        }
        sorted--;
    }
    public void StartCombat()
    {
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.LoadLevel(FormatManager.Instance.map);
        object[] instanceData;
        if (PhotonNetwork.IsMasterClient)
        {
            instanceData = new object[] { 0 };
        }
        else
        {
            instanceData = new object[] { 1 };
        }

        newPlayerGo = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), 0, instanceData);
        DontDestroyOnLoad(newPlayerGo);


        int index = 0;
        while (index < FormatManager.Instance.maxBeasts)
        {
            CreateBeast(index);
            index++;
        }

        Destroy(beastSelectorPlayer.gameObject);
        Destroy(gameObject);
    }


    void CreateBeast(int index)
    {
            int item1 = beastSelectorPlayer.team[index].item1Selected;
            int item2 = beastSelectorPlayer.team[index].item1Selected;
            int hab1 = beastSelectorPlayer.team[index].hab1Selected;
            int hab2 = beastSelectorPlayer.team[index].hab2Selected;
            int hab3 = beastSelectorPlayer.team[index].hab3Selected;
            int hab4 = beastSelectorPlayer.team[index].hab4Selected;
        while(hab1 == 0 || hab1==hab2 || hab1 == hab3 || hab1 == hab4 || hab1 > 8)
        {
            hab1 = Random.Range(1, 9);
        }
        while(hab2 == 0 || hab2 == hab1 || hab2 == hab3 || hab2 == hab4 || hab2 > 8)
        {
            hab2 = Random.Range(1, 9);
        }
        while(hab3 == 0 || hab3 == hab2 || hab3 == hab1 || hab3 == hab4 || hab3 > 8)
        {
            hab3 = Random.Range(1, 9);
        }
        while(hab4 == 0 || hab4 == hab2 || hab4 == hab3 || hab4 == hab1 || hab4 > 8)
        {
            hab4 = Random.Range(1, 9);
        }
            int team;
            if (PhotonNetwork.IsMasterClient)
            {
                team = 0;
            }
            else
            {
                team = 1;
            }

        object[] instanceData = new object[] { item1, item2, hab1, hab2, hab3, hab4, team };

            GameObject beast = PhotonNetwork.Instantiate(beastSelectorPlayer.team[index].name, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0),0,instanceData);
            DontDestroyOnLoad(beast);
    }
}
