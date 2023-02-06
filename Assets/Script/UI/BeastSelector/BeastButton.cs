using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastButton : MonoBehaviour
{
    BeastSelectorManager beastSelectorManager;
    public GameObject unit;
    public Sprite beastImage;
    public Sprite beastIcon;
    public string beastName;
    public int beast;

    private void Awake()
    {
        beastSelectorManager = FindObjectOfType<BeastSelectorManager>();
    }

    public void SelectBeast()
    {
        beastSelectorManager.SelectBeastCall(beast);
    }


}
