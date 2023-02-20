﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CombatManager : MonoBehaviourPunCallbacks
{
    public CombatUIManager uiManager;
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
    int playersReady;

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



    // Update is called once per frame
    void Update()
    {
        if(playerList.Count != 2)
        {
           playerList = new List<Player>(FindObjectsOfType<Player>());
        }
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



    public void PlayerReady(int value)
    {
        photonView.RPC("ReplicatePlayerReady", RpcTarget.All, value);
    }
    [PunRPC]
    void ReplicatePlayerReady(int value) 
    {
        playersReady += value;
        if (playersReady ==2)
        {
            foreach (Player player in playerList)
            {
                player.EndSetUpStage();
            }
            StartMatch();
        }
    }

    void StartMatch()
    {
        /*List<Player> newPlayerList = playerList;
        playerList.Clear();
        foreach (Player player in newPlayerList)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (player.photonView.IsMine)
                {
                    playerList.Add(player);
                }
            }
            else
            {
                if (!player.photonView.IsMine)
                {
                    playerList.Add(player);
                }
            }
        }
        foreach (Player player in newPlayerList)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (!player.photonView.IsMine)
                {
                    playerList.Add(player);
                }
            }
            else
            {
                if (player.photonView.IsMine)
                {
                    playerList.Add(player);
                }
            }
        }*/
        foreach (Player player in playerList)
        {
            if (player.photonView.IsMine)
            {
                foreach (Unit unit in player.beasts)
                {
                    uiManager.CreateBeastSheet(unit, false);
                }
            }
            else
            {
                foreach (GameObject unit in player.beastsToPlace)
                {
                    player.beasts.Add(unit.GetComponent<Unit>());
                }
                player.beastsToPlace.Clear();

            }
        }
            photonView.RPC("StartMatchRPC", RpcTarget.AllBuffered);

    }

    [PunRPC]
    void StartMatchRPC()
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
        

        bool playerArrow;

        if (PhotonNetwork.IsMasterClient)
        {
            playerArrow = false;
        }
        else
        {
            playerArrow = true;
        }

        foreach (Unit unitC in unitList)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (!playerArrow)
                {
                    turnOrderList.Add(playerList[0]);
                }
                else
                {
                    turnOrderList.Add(playerList[1]);
                }
            }
            else
            {
                if (playerArrow)
                {
                    turnOrderList.Add(playerList[0]);
                }
                else
                {
                    turnOrderList.Add(playerList[1]);
                }
            }
            playerArrow = !playerArrow;
        }
        SiguienteTurno();

    }

    public void SiguienteTurno()
    {
        photonView.RPC("RpcSiguienteTurno", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RpcSiguienteTurno()
    {
        foreach (ObjetoInvocado invoc in invocaciones)
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

