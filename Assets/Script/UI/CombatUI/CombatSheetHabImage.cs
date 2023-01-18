using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatSheetHabImage : MonoBehaviour
{
    public int hability;
    public CombatBeastSheet sheet;
    public TextMeshProUGUI cdText;
    public TextMeshProUGUI maxRText;

    private void OnMouseOver()
    {
        sheet.ChangeHabilitiesInfo();
        sheet.uiManager.ShowHabilitySign(sheet.unit.GetHabDescription(hability), sheet.unit.GetHabIcon(hability));
    }
    public void SetInfo(int cd, int rMax)
    {
        if(cd > 0)
        {
            cdText.gameObject.SetActive(true);
            cdText.text = cd.ToString();
        }
        else
        {
            cdText.gameObject.SetActive(false);
        }
        if (rMax > 0)
        {
            maxRText.gameObject.SetActive(true);
            maxRText.text = rMax.ToString();
        }
        else
        {
            maxRText.gameObject.SetActive(false);
        }
    }

}
