using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastImage : MonoBehaviour
{
    public Unit beast;
    public Sprite beastIcon;
    public Sprite beastImage;
    public TextMeshProUGUI beastText;
    public bool beastSelected;
    public bool mirror;

    public void ChangeImage(Sprite image, Sprite icon, string name, Unit unit)
    {
        beast = unit;
        GetComponent<Image>().sprite = image;
        beastImage = image;
        beastIcon =  icon;
        beastText.text = name;
        beastSelected = true;
    }
}
