using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeastSelectorManager : MonoBehaviour
{
    public int maxBeasts;
    int selectionTurn;
    public float maxTime;
    float actualTime;
    public TextMeshProUGUI timeText;

    public BeastSelectorPlayer player1;
    public BeastSelectorPlayer player2;

    public GameObject contentTeam1;
    public GameObject contentTeam2;
    public GameObject baseBeastImage;
    public GameObject baseBeastImageMirror;
    public List<BeastImage> beastImages;

    public GameObject defaultUnit;
    public Sprite defaultBeast;
    public Sprite defaultIcon;
    public string defaultBeastName = "Diggeye";


    public GameObject teamBuilder;

    private void Awake()
    {
        actualTime = maxTime;
    }
    void Start()
    {
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
        CreateBeastImage(contentTeam1.transform, baseBeastImage);
        CreateBeastImage(contentTeam2.transform, baseBeastImageMirror);
        CreateBeastImage(contentTeam2.transform, baseBeastImageMirror);
        CreateBeastImage(contentTeam1.transform, baseBeastImage);
        CreateBeastImage(contentTeam1.transform, baseBeastImage);
        CreateBeastImage(contentTeam2.transform, baseBeastImageMirror);
        if (maxBeasts > 3)
        {
            CreateBeastImage(contentTeam2.transform, baseBeastImageMirror);
            CreateBeastImage(contentTeam1.transform, baseBeastImage);
            if (maxBeasts > 4)
            {
                CreateBeastImage(contentTeam1.transform, baseBeastImage);
                CreateBeastImage(contentTeam2.transform, baseBeastImageMirror);
            }
            
        }
    }

    void CreateBeastImage(Transform targetParent, GameObject objectToInstantiate)
    {
        GameObject instance = Instantiate(objectToInstantiate, targetParent);
        beastImages.Add(instance.GetComponent<BeastImage>());
    }

    public void SelectBeast(Sprite beastImage, Sprite beastIcon, string beastName, GameObject unit)
    {
        beastImages[selectionTurn].ChangeImage(beastImage, beastIcon, beastName, unit);
    }

    public void Lock()
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
            if (beastImages[selectionTurn].mirror)
            {
                player2.team.Add(newBeast);
            }
            else
            {
                player1.team.Add(newBeast);
            }
            selectionTurn++;
            if (selectionTurn>beastImages.Count-1)
            {
                actualTime = 1000;
                StartCoroutine(LoadTeamEditor());
            }
        }
    }
    IEnumerator LoadTeamEditor()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("TeamBuilder");
        yield return new WaitForSeconds(0.5f);
        GameObject builder1 = Instantiate(teamBuilder);
        builder1.GetComponent<TeamEditorPlayerManager>().beastSelectorPlayer = player1;
        builder1.GetComponent<TeamEditorPlayerManager>().team = 0;
        DontDestroyOnLoad(builder1);
        GameObject builder2 = Instantiate(teamBuilder);
        builder2.GetComponent<TeamEditorPlayerManager>().beastSelectorPlayer = player2;
        builder1.GetComponent<TeamEditorPlayerManager>().team = 1;
        TeamEditorManager manager = FindObjectOfType<TeamEditorManager>();
        manager.player1 = builder1.GetComponent<TeamEditorPlayerManager>();
        manager.player2 = builder2.GetComponent<TeamEditorPlayerManager>();
        DontDestroyOnLoad(builder2);
        Destroy(gameObject);
    }
    public void TimeUp()
    {
        if (beastImages[selectionTurn].beastSelected)
        {
            Lock();
        }
        else
        {
            SelectBeast(defaultBeast, defaultIcon, defaultBeastName, defaultUnit);
            Lock();
        }
    }
}
