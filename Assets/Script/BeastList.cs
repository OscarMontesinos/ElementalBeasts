using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class BeastList : MonoBehaviour
{
    public static List<UnitData> beastList;
    public List<UnitData> beastListRaw = new List<UnitData>();

    private void Awake()
    {
        beastList = beastListRaw;
    }
}
