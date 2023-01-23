using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseIndicator : DebuffIndicator
{
    public List<GameObject> icons;
    public override void Activate(float value, int curse)
    {
        base.Activate(value, curse);
        foreach(GameObject icon in icons)
        {
            icon.SetActive(false);
        }
        icons[curse].SetActive(true);

    }
}
