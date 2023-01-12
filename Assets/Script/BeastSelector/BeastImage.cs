using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastImage : MonoBehaviour
{
    public TextMeshProUGUI beastText;

    public void ChangeImage(Sprite image, string name)
    {
        GetComponent<Image>().sprite = image;
        beastText.text = name;
    }
}
