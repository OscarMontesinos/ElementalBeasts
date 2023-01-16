using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatBeastSheet : MonoBehaviour
{
    [Header("UI")]
    public Unit unit;
    public TextMeshProUGUI nameText;
    public GameObject turnContent;
    public GameObject buffsContent;
    public TextMeshProUGUI statsText;
    public GameObject turnUpIndicator;
    public Image hab1Image;
    public TextMeshProUGUI hab1Name;
    public Image hab2Image;
    public TextMeshProUGUI hab2Name;
    public Image hab3Image;
    public TextMeshProUGUI hab3Name;
    public Image hab4Image;
    public TextMeshProUGUI hab4Name;

    [Header("Resources")]
    public GameObject turnImage;
    List<GameObject> turnObjects;

    private void UpdateSheet()
    {
        nameText.text = unit.name;

        float hp = unit.mHp;
        float sinergiaElemental = unit.sinergiaElemental;
        float fuerza = unit.fuerza;
        float control = unit.control;
        float resistenciaFisica =unit.resistenciaFisica;
        float resistenciaMagica = unit.resistenciaMagica;
        float movementPoints = unit.maxMovementPoints;

        statsText.text =    hp + "\n" +
                            sinergiaElemental + "\n" +
                            fuerza + "\n" +
                            control + "\n" +
                            resistenciaFisica + "\n" +
                            resistenciaMagica + "\n" +
                            movementPoints;


        ChangeHabilitiesInfo();

        turnUpIndicator.SetActive(!unit.pasar);
        if (turnObjects != null)
        {
            turnObjects.Clear();
        }
        int index = 0;
        while(index >= unit.turnoRestante)
        {
            GameObject turnImageGO = Instantiate(turnImage, turnContent.transform);
            turnObjects.Add(turnImageGO);
            index++;
        }
    }

    void ChangeHabilitiesInfo()
    {
        hab1Image.sprite = unit.GetHabIcon(unit.chosenHab1);
        hab1Name.text = unit.GetHabName(unit.chosenHab1);

        hab2Image.sprite = unit.GetHabIcon(unit.chosenHab2);
        hab2Name.text = unit.GetHabName(unit.chosenHab2);

        hab3Image.sprite = unit.GetHabIcon(unit.chosenHab3);
        hab3Name.text = unit.GetHabName(unit.chosenHab3);

        hab4Image.sprite = unit.GetHabIcon(unit.chosenHab4);
        hab4Name.text = unit.GetHabName(unit.chosenHab4);

    }

}
