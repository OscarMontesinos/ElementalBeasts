using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastImage : MonoBehaviour
{
    public GameObject beastGO;
    public Unit beast;
    public string beastName;
    public Sprite beastIcon;
    public Sprite beastImage;
    public TextMeshProUGUI beastText;
    public bool beastSelected;
    public bool mirror;

    public void ChangeImage(Sprite image, Sprite icon, string name, GameObject unit)
    {
        beastGO = unit;
        beast = unit.GetComponent<Unit>();
        GetComponent<Image>().sprite = image;
        beastImage = image;
        beastIcon =  icon;
        beastText.text = name;
        beastName = name;
        beastSelected = true;
    }
}
