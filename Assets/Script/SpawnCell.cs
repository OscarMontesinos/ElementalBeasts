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

    public void SpawnUnit(GameObject unitToSpawn, Player player)
    {
        if (player.team == team)
        {
            GameObject unit = Instantiate(unitToSpawn, transform.position, transform.rotation);
            unitPlaced = unit;
            player.beastsToPlace.Remove(unitPlaced);
            player.beasts.Add(unitPlaced.GetComponent<Unit>());
        }
    }

    public void DestroyUnit(Player player)
    {
        if (player.team == team)
        {
            player.beasts.Remove(unitPlaced.GetComponent<Unit>());
            player.beastsToPlace.Add(unitPlaced);
            Destroy(unitPlaced);
        }
    }

    public void EndSetupStage()
    {
        Destroy(gameObject);
    }

    
}
