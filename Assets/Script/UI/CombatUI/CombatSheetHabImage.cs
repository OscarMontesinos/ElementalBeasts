using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSheetHabImage : MonoBehaviour
{
    public int hability;
    public CombatBeastSheet sheet;
    public Text cdText;
    public Text maxRText;

    private void OnMouseOver()
    {
        sheet.ChangeHabilitiesInfo();
        switch (hability)
        {
            case 1:
                sheet.uiManager.ShowHabilitySign(sheet.unit.GetHabDescription(1));
                break;
            case 2:
                sheet.uiManager.ShowHabilitySign(sheet.unit.GetHabDescription(2));
                break;
            case 3:
                sheet.uiManager.ShowHabilitySign(sheet.unit.GetHabDescription(3));
                break;
            case 4:
                sheet.uiManager.ShowHabilitySign(sheet.unit.GetHabDescription(4));
                break;
        }
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
