using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerSearcher : ObjetoInvocado
{
    public int team;
    public Unit owner;
    public int walls;
    public int range;
    public int speed;
    public Vector3 destinyPos;
    public bool avaliable;
    public int x;
    public int y;

    public override void Start()
    {
    }

    public override void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
        }
    }

    public override void Actualizar()
    {
    }
    public void SetUp(int team, Vector3 destinyPos, int range, Unit owner)
    {
        this.owner = owner;
        this.team = team;
        this.range = range;
        this.destinyPos = destinyPos;
        Pathfinding.Instance.GetGrid().GetXY(transform.position, out x, out y);
        owner.GetManager().invocaciones.Add(this);
        StartCoroutine(goForward());
    }

    IEnumerator goForward()
    {
        int newx;
        int newy;
        Vector3 dir = destinyPos - transform.position;
        while (range > 0 || walls > 0)
        {
            Pathfinding.Instance.GetGrid().GetXY(transform.position, out newx, out newy);
            if (range != 0)
            {
                int result = x - newx;
                if (result < 0)
                {
                    result *= -1;
                }
                if (result >= range)
                {
                    range = 0;
                }
                result = y - newy;
                if (result < 0)
                {
                    result *= -1;
                }
                if (result >= range)
                {
                    range = 0;
                }
            }
            transform.Translate(dir.normalized * speed*Time.deltaTime);
            yield return null;
        }
        owner.manager.Position(gameObject);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Ward>().team = team;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NotWalkableWall>())
        {
            walls++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NotWalkableWall>())
        {
            walls--;
        }
    }
    private void OnMouseEnter()
    {
        if(owner.castingHability == 4 && owner.turno)
        {
            avaliable = true;
        }
    }
    private void OnMouseExit()
    {
        avaliable = false;
    }


}
