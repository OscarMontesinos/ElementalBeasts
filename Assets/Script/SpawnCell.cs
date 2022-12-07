using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCell : MonoBehaviour
{
    CombatManager cManager;
    GameObject unitPlaced;
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
            DestroyUnit(player);
        }
        else if (unitPlaced == null)
        {
            if (player.beastsToPlace.Count > 0)
            {
                SpawnUnit(player.beastsToPlace[player.GetIndex()], player);
            }
        }
    }

    public void SpawnUnit(GameObject unitToSpawn, Player player)
    {
        if (player.team == team)
        {
            GameObject unit = Instantiate(unitToSpawn, transform.position, transform.rotation);
            unitPlaced = unit;
            unit.GetComponent<Unit>().owner = player;
            player.beastPlaced.Add(unitToSpawn);
            player.beastsToPlace.Remove(unitToSpawn);
            player.beasts.Add(unitPlaced.GetComponent<Unit>());
        }
    }

    public void DestroyUnit(Player player)
    {


        if (player.team == team)
        {
            
            player.beasts.Remove(unitPlaced.GetComponent<Unit>());
            foreach(GameObject beast in player.beastPlaced)
            {

            }
            player.beastPlaced.Remove(unitPlaced);
            Destroy(unitPlaced);
        }
    }

    public void EndSetupStage()
    {
        Destroy(gameObject);
    }

}
