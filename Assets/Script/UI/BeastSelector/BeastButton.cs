using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastButton : MonoBehaviour
{
    BeastSelectorManager beastSelectorManager;
    public int id;

    private void Awake()
    {
        beastSelectorManager = FindObjectOfType<BeastSelectorManager>();
    }

    public void SelectBeast()
    {
        beastSelectorManager.SelectBeast(id);
    }


}
