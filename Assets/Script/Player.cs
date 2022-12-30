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
    public Unit beastSelected;
    public GameObject selector;
    public int indexBeastToPlace;
    CombatManager manager;
    [Header("UI")]
    public GameObject panelSetUp;
    public GameObject buttonReady;
    public GameObject panelTurn;
    public Cursor cursor;
    public int iniciative;

    private void Awake()
    {
        manager = FindObjectOfType<CombatManager>();
    }
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
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !settingUnitsUp && beastSelected !=null && !beastSelected.moving)
        {
            beastSelected.AcabarTurno();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && settingUnitsUp)
        {
            Ready();
        }
        if (settingUnitsUp)
        {
            List<Unit> discardedUnits = new List<Unit>(manager.unitList);
            List<Unit> allUnits = new List<Unit>();
            foreach (Unit unit_ in beasts)
            {
                if (unit_ != null && !discardedUnits.Contains(unit_))
                {
                    foreach (Unit unit in allUnits)
                    {
                        if (unit_.team != unit.team)
                        {
                            var dir = unit_.transform.position - transform.position;
                            if (!Physics2D.Raycast(transform.position, dir, dir.magnitude, unit.wallLayer))
                            {
                                unit.escondido = false;
                                discardedUnits.Add(unit);
                            }
                            else
                            {
                                unit.escondido = true;
                            }
                        }
                        else
                        {
                            discardedUnits.Add(unit);
                        }
                    }
                }
            }
            if (beastSelected != null)
            {
                selector.SetActive(true);
                selector.transform.position = beastSelected.transform.position;
            }
            else
            {

                selector.SetActive(false);
            }
            if (Input.GetMouseButtonDown(0) && cursor.GetCell() != null)
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
        else
        {
            selector.SetActive(false);
            panelSetUp.SetActive(false);
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
        cursor.gameObject.SetActive(false);
    }
    
    public void GiveTurnStage()
    {
        manager.giveTurn = true;
        giveTurno = true;
        panelTurn.SetActive(true);
        foreach (Unit unit in beasts)
        {
            if (!unit.pasar)
            {
                unit.SetElegibleMarcador(true);
            }
        }
    }

    public void TurnGived(Unit unit)
    {
        foreach (Unit beast in beasts)
        {
            beast.SetElegibleMarcador(false);
        }
        beastSelected = unit;
        giveTurno = false;
        manager.giveTurn = false;
        panelTurn.SetActive(false);
    }

    public void Ready()
    {
        ready = !ready;
        manager.PlayerReady();
    }
}
