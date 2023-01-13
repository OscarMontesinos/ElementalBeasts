using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeastSelectorManager : MonoBehaviour
{
    bool firstPick = true;
    public int maxBeasts;
    int selectionTurn;
    public float maxTime;
    float actualTime;
    public TextMeshProUGUI timeText;

    public BeastSelectorPlayer player1;
    List<UnitData> team1;

    public BeastSelectorPlayer player2;
    List<UnitData> team2;

    public GameObject contentTeam1;
    public GameObject contentTeam2;
    public GameObject baseBeastImage;
    public GameObject baseBeastImageMirror;
    public List<BeastImage> beastImages;



    bool player1Turn;
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
    }

    void CreateTeams()
    {
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

    public void SelectBeast(Sprite beastImage, string beastName)
    {
        beastImages[selectionTurn].ChangeImage(beastImage, beastName);
    }
}
