using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatManager : MonoBehaviour
{
    public static FormatManager Instance;
    public int maxBeasts;
    public int map;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }
}


