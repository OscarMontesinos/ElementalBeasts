using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode
{
    Grid<Pathnode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public Pathnode cameFromNode;
     

    public Pathnode(Grid<Pathnode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void SetIsWalkable(bool value)
    {
        isWalkable = value;
    }
    public override string ToString()
    {
        return x + " " + y;
    }
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
