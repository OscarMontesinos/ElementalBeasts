using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatBeastSheet : MonoBehaviour
{
    public Unit unit;
    public TextMeshProUGUI nameText;
    public GameObject turnContent;
    public GameObject buffsContent;
    public TextMeshProUGUI statsText;
    public Image hab1Image;
    public TextMeshProUGUI hab1Name;
    public Image hab2Image;
    public TextMeshProUGUI hab2Name;
    public Image hab3Image;
    public TextMeshProUGUI hab3Name;
    public Image hab4Image;
    public TextMeshProUGUI hab4Name;

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
    }

}
