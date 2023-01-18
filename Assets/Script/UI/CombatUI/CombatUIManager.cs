using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    CombatManager manager;

    public GameObject leftTeamVisualizer;
    public GameObject rightTeamVisualizer;
    public GameObject habInformationSign;
    public Text habInformationSignText;

    public GameObject beastSheet;
    public GameObject mirrorBeastSheet;



    private void Awake()
    {
        manager = FindObjectOfType<CombatManager>();
    }
    public void ShowHabilitySign(string habText)
    {
        habInformationSign.SetActive(true);
        habInformationSignText.text = habText; 
    }

    public void HideHabilitySign()
    {
        habInformationSign.SetActive(false);
    }

    public void CreateBeastSheet(Unit unit, bool mirror)
    {
        GameObject sheet;
        if (mirror)
        {
            sheet = Instantiate(mirrorBeastSheet,rightTeamVisualizer.transform);
        }
        else
        {
            sheet = Instantiate(beastSheet, leftTeamVisualizer.transform);
        }

        sheet.GetComponent<CombatBeastSheet>().uiManager = this;
        sheet.GetComponent<CombatBeastSheet>().unit = unit;

    }

}
