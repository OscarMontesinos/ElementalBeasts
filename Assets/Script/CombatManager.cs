using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public bool settingUp;
    public List<Color32> teamColorList;
    public List<Unit> unitList;
    public List<Player> turnOrderList;
    public List<Player> playerList;
    public List<int> turnos;
    public List<ObjetoInvocado> invocaciones = new List<ObjetoInvocado>();
    public GameObject spawnCells;
    public GameObject pathShower;
    public int turnoActual = -1;
    public bool nuevaRonda = true;
    public int ronda = 1;
    public bool casteando;
    public bool diggeyeSearcherCasting;
    public bool habSingle;
    public bool aliado;
    public bool enemigo;
    public int singleTeam;
    public int muertos;
    public bool centrarCamara;
    public bool giveTurn;

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
        if (casteando || giveTurn)
        {
            foreach (ObjetoInvocado habilidad in invocaciones)
            {
                if (habilidad != null)
                {
                    if (diggeyeSearcherCasting && habilidad.GetComponent<MinerSearcher>())
                    {

                    }
                    else
                    {
                        habilidad.gameObject.SetActive(false);
                    }
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
        }
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
        foreach (Unit unitC in unitList)
        {
            if (unitC.team == playerList[0].team)
            {
                playerList[0].iniciative += unitC.iniciativaTurno;
            }
            else
            {
                playerList[1].iniciative += unitC.iniciativaTurno;
            }
        }

        bool playerArrow;

        if (playerList[0].iniciative > playerList[1].iniciative)
        {
            playerArrow = false;
        }
        else
        {
            playerArrow = true;
        }

        foreach (Unit unitC in unitList)
        {
            if (!playerArrow)
            {
                turnOrderList.Add(playerList[0]);
            }
            else
            {
                turnOrderList.Add(playerList[1]);
            }
            playerArrow = !playerArrow;
        }

        SiguienteTurno();

    }

    public void SiguienteTurno()
    {
        foreach(ObjetoInvocado invoc in invocaciones)
        {
            if (invoc != null)
            {
                invoc.Actualizar();
            }
        }
        turnoActual++;
        if (turnoActual >= unitList.Count)
        {
            turnoActual = 0;
            NuevaRonda();
        }
        turnOrderList[turnoActual].GiveTurnStage(); 
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

        List<Pathnode> nodesInRange = new List<Pathnode>(turnOrderList[turnoActual].beastSelected.GetNodesInRange());
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

