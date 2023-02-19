using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCell : MonoBehaviour
{
    CombatManager cManager;
    Unit unitPlaced;
    SpriteRenderer spriteRenderer;
    public int team;

    private void Awake()
    {
        cManager = FindObjectOfType<CombatManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        cManager.Position(gameObject);
        spriteRenderer.color = new Color32(cManager.teamColorList[team].r, cManager.teamColorList[team].g, cManager.teamColorList[team].b,125);
    }
    public int GetTeam()
    {
        return team;
    }
    public void Interact(Player player)
    {
        if (unitPlaced != null)
        {
            ReplaceOrSelectBeast(player);
        }
        else if (unitPlaced == null)
        {
            if (player.beastsToPlace.Count <= 0)
            {
                if (player.beastSelected != null)
                {
                    ReplaceBeast(player);
                }
            }
            else
            {
                SpawnUnit(player.beastsToPlace[player.indexBeastToPlace], player);
            }
        }
    }

    public void SpawnUnit(GameObject unitToSpawn, Player player)
    {
        if (player.team == team)
        {
            unitToSpawn.GetComponent<Unit>().spawnCell = this;

            player.beastsToPlace.Remove(unitToSpawn);
            player.beasts.Add(unitToSpawn.GetComponent<Unit>());

            if (player.beastsToPlace.Count <= 0)
            {
                player.buttonReady.SetActive(true);
            }
            else if(player.indexBeastToPlace> player.beastsToPlace.Count - 1)
            {
                player.indexBeastToPlace--;
            }
            unitPlaced = unitToSpawn.GetComponent<Unit>();
            unitToSpawn.transform.position = transform.position;
            unitToSpawn.GetComponent<Unit>().ReplicateTransformCall(unitToSpawn.transform.position.x, unitToSpawn.transform.position.y);
        }
    }

    public void ReplaceOrSelectBeast(Player player)
    {
        if (player.beastSelected == null)
        {
            SelectBeast(player);
        }
        else
        {
            ReplaceBeast(player);
        }
    }

    public void SelectBeast(Player player)
    {
        player.beastSelected = unitPlaced;
    }

    public void ReplaceBeast(Player player)
    {
        if (unitPlaced != null)
        {
            Unit auxiliarBeast = player.beastSelected;
            SpawnCell auxiliarSpawnCell = player.beastSelected.spawnCell;
            //Intercambio transform
            unitPlaced.transform.position = player.beastSelected.transform.position;
            player.beastSelected.transform.position = unitPlaced.spawnCell.transform.position;
            unitPlaced.ReplicateTransformCall(unitPlaced.transform.position.x, unitPlaced.transform.position.y);
            player.beastSelected.ReplicateTransformCall(player.beastSelected.transform.position.x, player.beastSelected.transform.position.y);
            //Intercambio spawnCells
            unitPlaced.spawnCell = player.beastSelected.spawnCell;
            player.beastSelected.spawnCell = this;
            //Intercambio de unidad colocada entre spawnCells
            auxiliarSpawnCell.unitPlaced = unitPlaced;
            unitPlaced = auxiliarBeast;

        }
        else
        {

            player.beastSelected.spawnCell.unitPlaced = null;
            unitPlaced = player.beastSelected;
            unitPlaced.transform.position = transform.position;
            unitPlaced.spawnCell = this;
            unitPlaced.ReplicateTransformCall(unitPlaced.transform.position.x, unitPlaced.transform.position.y);
        }

            player.beastSelected = null;
        


    }


    public void EndSetupStage()
    {
        Destroy(gameObject);
    }

}
