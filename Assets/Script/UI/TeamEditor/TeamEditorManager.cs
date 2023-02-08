using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamEditorManager : MonoBehaviourPunCallbacks
{
    public GameObject placeToSpawn;

    public GameObject beastSheet;

    public GameObject playerPrefab;
    public BeastSelectorPlayer beastSelectorPlayer;

    public List<GameObject> beastsList;

    public int maxBeasts;

    GameObject newPlayerGo;
    Player newPlayer;
    private void Awake()
    {
        maxBeasts = FormatManager.Instance.maxBeasts;
        beastSelectorPlayer = FindObjectOfType<BeastSelectorPlayer>();
    }

    private void Start()
    {
        int beast = 0;
        while (beast < maxBeasts)
        {
            GameObject sheet = Instantiate(beastSheet, placeToSpawn.transform);
            sheet.GetComponent<BeastSheet>().beastSlot = beast;
            beast++;
        }
    }
    [PunRPC]
    public void StartCombat()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(FormatManager.Instance.map);
        newPlayerGo = Instantiate(playerPrefab);
        DontDestroyOnLoad(newPlayerGo);
        newPlayer = newPlayerGo.GetComponent<Player>();
        int index = 0;
        while (index < FormatManager.Instance.maxBeasts)
        {
            photonView.RPC("CreateBeast",RpcTarget.AllBuffered,index);
            index++;
        }
        Destroy(beastSelectorPlayer.gameObject);
        Destroy(gameObject);
    }

    [PunRPC]
    void CreateBeast(int index)
    {
        GameObject beast = Instantiate(beastSelectorPlayer.team[index].unitGO, newPlayer.transform, false);
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
}
