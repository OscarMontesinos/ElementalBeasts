using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatManager : MonoBehaviour
{
    public static FormatManager Instance;
    public int maxBeasts;
    public string map;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    public void SetMap(string map)
    {
        this.map = map;
    }
    public void SetBeasts(int beast)
    {
        this.maxBeasts = beast;
    }
}


