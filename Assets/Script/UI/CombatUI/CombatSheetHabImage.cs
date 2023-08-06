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

    public enum HabCdType
    {
        none, cd, repetitions
    }

    private void OnMouseOver()
    {
        sheet.ChangeHabilitiesInfo();
        sheet.uiManager.ShowHabilitySign(sheet.unit.GetHabDescription(hability), sheet.unit.GetHabIcon(hability));
    }
    public void SetInfo(Unit.HabCd info)
    {
        if (info.type == HabCdType.cd)
        {
            if (info.value > 0)
            {
                cdText.transform.parent.gameObject.SetActive(true);
                cdText.text = info.value.ToString();
            }
            else
            {
                cdText.transform.parent.gameObject.SetActive(false);
            }
        }
        else if (info.type == HabCdType.repetitions)
        {
            maxRText.transform.parent.gameObject.SetActive(true);
            maxRText.text = info.value.ToString();
        }
    }

}
