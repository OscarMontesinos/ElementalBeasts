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

        player.beastsToPlace.Add(beast);
    }

    void CloseEditor()
    {
        content.SetActive(false);
    }
}
