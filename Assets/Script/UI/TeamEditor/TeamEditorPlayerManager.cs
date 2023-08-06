using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamEditorPlayerManager : MonoBehaviour
{ 
    bool playerReady;
    public int team;
    public GameObject content;

    public GameObject placeToSpawn;

    public GameObject beastSheet;

    public GameObject playerPrefab;
    public BeastSelectorPlayer beastSelectorPlayer;
    public BeastEditor editor;

    public List<GameObject> beastsList;

    public int maxBeasts;
    private void Awake()
    {
        maxBeasts = FormatManager.Instance.maxBeasts;
    }

    private void Start()
    {
        editor.player = beastSelectorPlayer;
        transform.SetParent(FindObjectOfType<Canvas>().transform);
        transform.localPosition = Vector3.zero;
        int beast = 0;
        while (beast < maxBeasts)
        {
            GameObject sheet = Instantiate(beastSheet, placeToSpawn.transform);
            sheet.GetComponent<BeastSheet>().player = beastSelectorPlayer;
            sheet.GetComponent<BeastSheet>().beastSlot = beast;
            sheet.GetComponent<BeastSheet>().editor = editor;
            beast++;
        }
    }

    public void PlayerReady()
    {
        TeamEditorManager.Instance.PlayerReady();
        CloseEditor();
        
    }
    public void StartCombat()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        DontDestroyOnLoad(playerGO);
        Player player = playerGO.GetComponent<Player>();
        player.team = team;
        int index = 0;
        while (index < FormatManager.Instance.maxBeasts)
        {
            CreateBeast(index,player);
            index++;
        }

        TeamEditorManager.Instance.PlayerReadyForCombat();
    }

    void CreateBeast(int index, Player player)
    {
        GameObject beast = Instantiate(beastSelectorPlayer.team[index].unitGO, player.transform, false);
        Items items = beast.GetComponent<Items>();
        Unit unit = beast.GetComponent<Unit>();
        if (beastSelectorPlayer.team[index].item1Selected != items.items.Count - 1)
        {
            items.items[beastSelectorPlayer.team[index].item1Selected].equipado = true;
        }
        if (beastSelectorPlayer.team[index].item2Selected != items.items.Count - 1)
        {
            items.items[beastSelectorPlayer.team[index].item2Selected].equipado = true;
        }



        int hab1 = beastSelectorPlayer.team[index].hab1Selected;
        int hab2 = beastSelectorPlayer.team[index].hab2Selected;
        int hab3 = beastSelectorPlayer.team[index].hab3Selected;
        int hab4 = beastSelectorPlayer.team[index].hab4Selected;

        while(hab1==0 ||hab1==hab2 || hab1 == hab3 || hab1 == hab4)
        {
            hab1 = Random.Range(1, 9);
        }
        unit.chosenHab1 = hab1;

        while (hab2 == 0 || hab2 == hab1 || hab2 == hab3 || hab2 == hab4)
        {
            hab2 = Random.Range(1, 9);
        }
        unit.chosenHab2 = hab2;

        while (hab3 == 0 || hab3 == hab2 || hab3 == hab1 || hab3 == hab4)
        {
            hab3 = Random.Range(1, 9);
        }
        unit.chosenHab3 = hab3;

        while (hab4 == 0 || hab4 == hab2 || hab4 == hab3 || hab4 == hab1)
        {
            hab4 = Random.Range(1, 9);
        }
        unit.chosenHab4 = hab4;



        player.beastsToPlace.Add(beast);
    }

    void CloseEditor()
    {
        content.SetActive(false);
    }
}
