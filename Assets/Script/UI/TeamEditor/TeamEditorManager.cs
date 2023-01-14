using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamEditorManager : MonoBehaviour
{
    public GameObject placeToSpawn;

    public GameObject beastSheet;

    public int maxBeasts;

    private void Start()
    {
        int beast = 0;
        while (beast < maxBeasts)
        {
            GameObject sheet = Instantiate(beastSheet, placeToSpawn.transform);
            sheet.GetComponent<BeastSheet>().beastSlot = beast;
            beast++;
        }
    }
}
