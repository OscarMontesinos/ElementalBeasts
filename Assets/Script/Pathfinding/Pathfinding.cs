using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    const int MOVE_STRAIGHT_COST = 10;
    const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    Grid<Pathnode> grid;

    List<Pathnode> openList;
    List<Pathnode> closedList;
    public Pathfinding(int width,int height,float cellsize)
    {
        Instance = this;
        grid = new Grid<Pathnode>(width, height, cellsize, (Grid<Pathnode> g, int x, int y) => new Pathnode(g, x, y));
    }

    public Grid<Pathnode> GetGrid()
    {
        return grid;
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int enxY);

        List<Pathnode> path = FindPath(startX, startY, endX, enxY);
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(Pathnode pathnode in path)
            {
                vectorPath.Add(new Vector3(pathnode.x, pathnode.y) * grid.GetCellsize() + Vector3.one * grid.GetCellsize() * .5f);
            }
            return vectorPath;
        }
    }
    public List<Pathnode> FindPath(int startX, int startY, int endX, int endY)
    {
        Pathnode startNode = grid.GetGridObject(startX, startY);
        Pathnode endNode = grid.GetGridObject(endX, endY);
        openList = new List<Pathnode> { startNode };
        closedList = new List<Pathnode>();

        int x = 0;
        int y = 0;
        while (x < grid.GetWidth())
        {
            while (y < grid.GetHeight())
            {
                Pathnode pathnode = grid.GetGridObject(x, y);
                pathnode.gCost = int.MaxValue;
                pathnode.CalculateFCost();
                pathnode.cameFromNode = null;
                y++;
            }
            y = 0;
            x++;
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode,endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            Pathnode currentNode = GetLowestFcostNode(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Pathnode neigbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neigbourNode)) continue;
                if (!neigbourNode.isWalkable)
                {
                    closedList.Add(neigbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neigbourNode);
                if(tentativeGCost < neigbourNode.gCost)
                {
                    neigbourNode.cameFromNode = currentNode;
                    neigbourNode.gCost = tentativeGCost;
                    neigbourNode.hCost = CalculateDistanceCost(neigbourNode, endNode);
                    neigbourNode.CalculateFCost();

                    if (!openList.Contains(neigbourNode))
                    {
                        openList.Add(neigbourNode);
                    }
                }
            }
        }

        //Out of nodes onn open list
        return null;
    }

    public List<Pathnode> GetNeighbourList(Pathnode currentNode)
    {
        List<Pathnode> neighbourList = new List<Pathnode>();

        if (currentNode.x - 1 >= 0 && GetNode(currentNode.x-1,currentNode.y).isWalkable)
        {
            //Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            //Left Down
            if (currentNode.y - 1 >= 0 && GetNode(currentNode.x, currentNode.y-1).isWalkable) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            //Left Up
            if (currentNode.y + 1 < grid.GetHeight() && GetNode(currentNode.x, currentNode.y + 1).isWalkable) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth() && GetNode(currentNode.x + 1, currentNode.y).isWalkable)
        {
            //Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //Right Down
            if (currentNode.y - 1 >= 0 && GetNode(currentNode.x, currentNode.y - 1).isWalkable) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            //Right Up
            if (currentNode.y + 1 < grid.GetHeight() && GetNode(currentNode.x, currentNode.y + 1).isWalkable) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        //Down
        if(currentNode.y - 1 >=0) neighbourList.Add(GetNode(currentNode.x, currentNode.y-1));
        //Up
        if(currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y+1));

        
        return neighbourList;
    }

    List<Pathnode> CalculatePath(Pathnode endNode)
    {
        List<Pathnode> path = new List<Pathnode>();
        path.Add(endNode);
        Pathnode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
    int CalculateDistanceCost(Pathnode a, Pathnode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    Pathnode GetLowestFcostNode(List<Pathnode> pathnodeList)
    {
        Pathnode lowestFCostNode = pathnodeList[0];

        int i=1;
        while (i < pathnodeList.Count)
        {
            if(pathnodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathnodeList[i];
            }
            i++;
        }
        return lowestFCostNode;
    }

    public Pathnode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
}
