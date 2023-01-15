using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    Player player;
    int team;
    public SpawnCell cell;
    private void Awake()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        
    }
    private void Start()
    {
        team = player.team;
    }
    public int GetTeam()
    {
        return team;
    }

    public Player GetPlayer()
    {
        return player;
    }
    public SpawnCell GetCell()
    {
        return cell;
    }
    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SpawnCell>() && collision.gameObject.GetComponent<SpawnCell>().GetTeam() == team)
        {
            cell = collision.gameObject.GetComponent<SpawnCell>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SpawnCell>() && collision.gameObject.GetComponent<SpawnCell>().GetTeam() == team)
        {
            cell = null;
        }
    }
}
