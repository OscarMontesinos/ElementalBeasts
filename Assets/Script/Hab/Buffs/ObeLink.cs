using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeLink : Buff
{
    Obe owner;
    public bool active;
    int range;
    float prot;
    // Start is called before the first frame update

    private void Update()
    {
        if (owner == null)
        {
            End();
        }
        CalculateDistance();
    }

    public void BuffStart(Obe owner, float prot, int range)
    {
        type = BuffType.buff;
        this.owner = owner;
        this.range = range;
        this.prot = prot;
    }


    public override void End()
    {
        if (active)
        {
            unit.prot -= prot;
        }
    }


    void CalculateDistance()
    {

        int ownerPosX;
        int ownerPosY;

        int unitPosX;
        int unitPosY;

        MapPathfinder.Instance.GetPathfinding().GetGrid().GetXY(owner.transform.position, out ownerPosX, out ownerPosY);
        MapPathfinder.Instance.GetPathfinding().GetGrid().GetXY(transform.position, out unitPosX, out unitPosY);

        int distX = ownerPosX - unitPosX;
        int distY = ownerPosY - unitPosY;

        if (distX < 0)
        {
            distX = -distX;
        }

        if (distY < 0)
        {
            distY = -distY;
        }

        if (distX + distY > range)
        {
            if (active)
            {
                unit.prot -= prot;
            }
            active = false;
        }
        else
        {
            if (!active)
            {
                unit.prot += prot;
            }
            active = true;
        }
    }
}
