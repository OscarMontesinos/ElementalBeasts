using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatBeastSheet : MonoBehaviour
{
    [Header("UI")]
    public CombatUIManager uiManager;
    public Unit unit;
    public Slider hpBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI nameText;
    public GameObject turnContent;
    public GameObject buffsContent;
    public TextMeshProUGUI statsText;
    public Image turnUpIndicator;
    public GameObject selectedIndicator;
    public Image hab1Image;
    public string hab1Name;
    public CombatSheetHabImage hab1cd;
    public Image hab2Image;
    public string hab2Name;
    public CombatSheetHabImage hab2cd;
    public Image hab3Image;
    public string hab3Name;
    public CombatSheetHabImage hab3cd;
    public Image hab4Image;
    public string hab4Name;
    public CombatSheetHabImage hab4cd;
    public List<DebuffIndicator> debuffsIndicators; 

    [Header("Resources")]
    public GameObject turnImage;
    List<GameObject> turnObjects = new List<GameObject>();
    public Color32 turnUpColor;
    public Color32 turnDownColor;

    private void Awake()
    {

    }
    private void Start()
    {
        hpBar.maxValue = unit.mHp;
        UpdateSheet();
    }
    private void Update()
    {
        selectedIndicator.SetActive(unit.selected);
    }
    public void UpdateSheet()
    {
        if(unit == null)
        {
            Destroy(gameObject);
            return;
        }
        hpBar.value = unit.hp;
        hpText.text = unit.hp.ToString("F0");

        nameText.text = unit.name;

        float hp = unit.mHp;
        float sinergiaElemental = unit.sinergiaElemental;
        float fuerza = unit.fuerza;
        float control = unit.control;
        float resistenciaFisica = unit.resistenciaFisica;
        float resistenciaMagica = unit.resistenciaMagica;
        float movementPoints = unit.maxMovementPoints;

        statsText.text = hp + "\n" +
                            sinergiaElemental + "\n" +
                            fuerza + "\n" +
                            control + "\n" +
                            resistenciaFisica + "\n" +
                            resistenciaMagica + "\n" +
                            movementPoints;


        ChangeHabilitiesInfo();

        if (unit.pasar)
        {
            turnUpIndicator.color = turnDownColor;
        }
        else
        {
            turnUpIndicator.color = turnUpColor;
        }
        if (turnObjects != null)
        {
            foreach (GameObject turnObject in turnObjects)
            {
                Destroy(turnObject);
            }
            turnObjects.Clear();
        }
        int index = 0;
        while (index < unit.turnoRestante)
        {
            GameObject turnImageGO = Instantiate(turnImage, turnContent.transform);
            turnObjects.Add(turnImageGO);
            index++;
        }
    }

    public void ChangeHabilitiesInfo()
    {
        hab1Image.sprite = unit.GetHabIcon(unit.chosenHab1);
        hab1Name = unit.GetHabName(unit.chosenHab1);
        hab1cd.SetInfo(unit.GetHabCds(unit.chosenHab1));

        hab2Image.sprite = unit.GetHabIcon(unit.chosenHab2);
        hab2Name = unit.GetHabName(unit.chosenHab2);
        hab2cd.SetInfo(unit.GetHabCds(unit.chosenHab2));

        hab3Image.sprite = unit.GetHabIcon(unit.chosenHab3);
        hab3Name = unit.GetHabName(unit.chosenHab3);
        hab3cd.SetInfo(unit.GetHabCds(unit.chosenHab3));

        hab4Image.sprite = unit.GetHabIcon(unit.chosenHab4);
        hab4Name = unit.GetHabName(unit.chosenHab4);
        hab4cd.SetInfo(unit.GetHabCds(unit.chosenHab4));
    }

    public void ShowDebuff(int buff, float value)
    {
        debuffsIndicators[buff].gameObject.SetActive(true);
        debuffsIndicators[buff].Activate(value);
    }

    

}
