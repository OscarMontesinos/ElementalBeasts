using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool ready;
    public int team;
    public bool giveTurno;
    public bool settingUnitsUp;
    public List<Unit> beasts;
    public List<GameObject> beastsToPlace;
    int indexBeastToPlace;
    public CombatManager manager;
    [Header("UI")]
    public GameObject panelSetUp;
    public GameObject panelTurn;
    public bool GetReady()
    {
        return ready;
    }

    private void Update()
    {
        if (settingUnitsUp)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                if (indexBeastToPlace >= beastsToPlace.Count - 1)
                {
                    indexBeastToPlace = 0;
                }
                else indexBeastToPlace++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                indexBeastToPlace--;
                if (indexBeastToPlace <= 0)
                {
                    indexBeastToPlace = beastsToPlace.Count - 1;
                }
            }
        }
    }
    public void SetUp()
    {
        settingUnitsUp = true;
        panelSetUp.SetActive(true);
    }

    public  void EndSetUpStage()
    {
        settingUnitsUp = false;
        panelSetUp.SetActive(false);
    }
    
    public void GiveTurnStage()
    {
        giveTurno = true;
    }
}
