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
    public List<GameObject> beastPlaced;
    public int indexBeastToPlace;
    public CombatManager manager;
    [Header("UI")]
    public GameObject panelSetUp;
    public GameObject panelTurn;
    public Cursor cursor;
    public bool GetReady()
    {
        return ready;
    }

    public int GetIndex()
    {
        return indexBeastToPlace;
    }

    private void Start()
    {
        foreach(GameObject beast in beastsToPlace)
        {
            beastsToPlaceReference.Add(true);
        }
    }

    private void Update()
    {
        if (settingUnitsUp)
        {
            if (Input.GetMouseButtonDown(0) && cursor.GetCell()!=null)
            {
                cursor.GetCell().Interact(this);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                IncreaseIdex();
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                DecreaseIndex();
            }
        }
    }
    public void IncreaseIdex()
    {


        if (indexBeastToPlace >= beastsToPlace.Count - 1)
        {
            indexBeastToPlace = 0;
        }
        else
        {
            indexBeastToPlace++;

        }
    }
    public void DecreaseIndex()
    {
        indexBeastToPlace--;
        if (indexBeastToPlace < 0)
        {
            indexBeastToPlace = beastsToPlace.Count - 1;
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
