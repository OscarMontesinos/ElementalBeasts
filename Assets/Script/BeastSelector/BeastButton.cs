using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastButton : MonoBehaviour
{
    BeastSelectorManager beastSelectorManager;
    public Unit unit;
    public Sprite beastImage;
    public string beastName;

    private void Awake()
    {
        beastSelectorManager = FindObjectOfType<BeastSelectorManager>();
    }

    public void SelectBeast()
    {
        beastSelectorManager.SelectBeast(beastImage, beastName);
    }


}
