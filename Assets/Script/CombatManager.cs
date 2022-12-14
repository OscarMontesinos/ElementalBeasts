using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public bool settingUp;
    public List<Color32> teamColorList;
    public List<Unit> unitList;
    public List<Player> playerList;
    public List<int> turnos;
    public List<Player> referencia;
    public List<ObjetoInvocado> invocaciones;
    public GameObject spawnCells;
    public GameObject pathShower;
    public int turnoActual = -1;
    public bool nuevaRonda = true;
    public int ronda = 1;
    public bool casteando;
    public bool habSingle;
    public bool aliado;
    public bool enemigo;
    public int singleTeam;
    public int muertos;
    public bool centrarCamara;
    public int iniciativeDice;

    public TextMeshProUGUI rondaText;

    private void Awake()
    {
        spawnCells = FindObjectOfType<SpawnCells>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        rondaText.text = "Ronda: " + ronda;
        // Creamos un array con todos los GameObject que tengan el tag Character
        var listGameObjectChar = GameObject.FindGameObjectsWithTag("Character");
        var listGameObjectInvoc = GameObject.FindGameObjectsWithTag("Invocacion");
        playerList = new List<Player>(FindObjectsOfType<Player>());

        
        foreach (GameObject invoc in listGameObjectInvoc)
        {
            invocaciones.Add(invoc.GetComponent<ObjetoInvocado>());
        }

        foreach (Player player in playerList)
        {
            player.SetUp();
            settingUp = true;
        }
    }

    public void PlayerReady()
    {
        bool ready = true;
        foreach (Player player in playerList)
        {
            if (!player.GetReady())
            {
                ready = false;
            } 
        }
        if (ready)
        {
            foreach (Player player in playerList)
            {
                player.EndSetUpStage();
            }
            StartMatch();
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*if (casteando)
        {
            foreach (ObjetoInvocado habilidad in invocaciones)
            {
                if (habilidad != null)
                {
                    habilidad.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (ObjetoInvocado habilidad in invocaciones)
            {
                if (habilidad != null)
                {
                    habilidad.gameObject.SetActive(true);
                }
            }
        }*/
    }
    void StartMatch()
    {
        Destroy(spawnCells);
        settingUp = false;
        unitList = new List<Unit>(FindObjectsOfType<Unit>());

        foreach (Unit unit in unitList)
        {
            unit.UpdateCell(false);
        }

            IniciativaCalc();
    }
    void IniciativaCalc()
    {
        turnos.Clear();
        referencia.Clear();

        foreach (Unit unitC in unitList)
        {
            unitC.iniciativaTurno += Random.Range(0, iniciativeDice);
            turnos.Add(unitC.iniciativaTurno);
            referencia.Add(unitC.owner);
        }
        int trucu2 = 0;
        int ward = 0;
        Player playerWard; 
        foreach (Unit unitC in unitList)
        {
            foreach (Unit unitCh in unitList)
            {
                trucu2 = 0;
                while (trucu2 < turnos.Count - 1)
                {
                    if (turnos[trucu2] < turnos[trucu2 + 1])
                    {
                        ward = turnos[trucu2];
                        turnos[trucu2] = turnos[trucu2 + 1];
                        turnos[trucu2 + 1] = ward;

                        playerWard = referencia[trucu2];
                        referencia[trucu2] = referencia[trucu2 + 1];
                        referencia[trucu2 + 1] = playerWard;

                        trucu2++;
                    }
                    else
                    {
                        trucu2++;
                    }
                }
            }
        }

        SiguienteTurno();

    }

    public void SiguienteTurno()
    {
        turnoActual++;
        Debug.Log(turnoActual);
        if (turnoActual >= unitList.Count)
        {
            turnoActual = 0;
            NuevaRonda();
        }
        referencia[turnoActual].GiveTurnStage();
    }

    public void NuevaRonda()
    {
        foreach (Unit unit in unitList)
        {
            unit.NuevaRonda();
        }
        ronda++;
        rondaText.text = "Ronda: " + ronda;
    }

    public void ShowNodesInRange()
    {
        DestroyShowNodes();

        List<Pathnode> nodesInRange = new List<Pathnode>(referencia[turnoActual].beastSelected.GetNodesInRange());
        foreach (Pathnode pathnode in nodesInRange)
        {
            if (pathnode.isWalkable)
            {
                Vector3 position = new Vector2(pathnode.x / FindObjectOfType<MapPathfinder>().cellsize, pathnode.y / FindObjectOfType<MapPathfinder>().cellsize);
                position = position * FindObjectOfType<MapPathfinder>().cellsize + Vector3.one * .5f;
                Instantiate(pathShower, position, transform.rotation);
            }
        }



    }

    public void DestroyShowNodes()
    {
        List<PathShower> pathShowers = new List<PathShower>(FindObjectsOfType<PathShower>());

        foreach(PathShower pathShower in pathShowers)
        {
            Destroy(pathShower.gameObject);
        }
    }
    
    public void Position(GameObject obj)
    {
        if (obj.transform.position.x > 0 && obj.transform.position.y > 0)
        {
            obj.transform.position = new Vector3(obj.transform.position.x - (obj.transform.position.x % 1) + 0.5f, obj.transform.position.y - (obj.transform.position.y % 1) + 0.5f, obj.transform.position.z);
        }
        else if (obj.transform.position.x < 0 && obj.transform.position.y > 0)
        {
            obj.transform.position = new Vector3(obj.transform.position.x - (obj.transform.position.x % 1) - 0.5f, obj.transform.position.y - (obj.transform.position.y % 1) + 0.5f, obj.transform.position.z);
        }
        else if (obj.transform.position.x < 0 && obj.transform.position.y < 0)
        {
            obj.transform.position = new Vector3(obj.transform.position.x - (obj.transform.position.x % 1) - 0.5f, obj.transform.position.y - (obj.transform.position.y % 1) - 0.5f, obj.transform.position.z);
        }
        else if (obj.transform.position.x > 0 && obj.transform.position.y < 0)
        {
            obj.transform.position = new Vector3(obj.transform.position.x - (obj.transform.position.x % 1) + 0.5f, obj.transform.position.y - (obj.transform.position.y % 1) - 0.5f, obj.transform.position.z);
        }
    }

   
}

