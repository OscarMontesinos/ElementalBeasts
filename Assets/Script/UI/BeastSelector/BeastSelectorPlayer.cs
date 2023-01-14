using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastSelectorPlayer : MonoBehaviour
{
    public List<UnitData> team = new List<UnitData>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
