using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathfinder : MonoBehaviour
{
    CombatManager cManager;
    public int gridWidth;
    public int gridHeight;
    public float cellsize;
    Pathfinding pathfinding;
    public List<NotWalkable> notWalkableList;
    public List<Unit> unitList;
    public Unit unitToMove;
    // Start is called before the first frame update
    void Start()
    {
        cManager = FindObjectOfType<CombatManager>();
        unitToMove = FindObjectOfType<Unit>();
        pathfinding = new Pathfinding(gridWidth,gridHeight,cellsize);
        notWalkableList = new List<NotWalkable>( FindObjectsOfType<NotWalkable>());
        unitList = new List<Unit>( FindObjectsOfType<Unit>());
        CalculateNotWalkables();
        BlockAllOccupiedCells();
    }

    public Pathfinding GetPathfinding()
    {
        return pathfinding;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cManager.settingUp && unitToMove != null )
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            /*pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
            List<Pathnode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                int i = 0;
                while (i < path.Count - 1)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * cellsize + Vector3.one * .5f, new Vector3(path[i + 1].x, path[i + 1].y) * cellsize + Vector3.one * .5f, Color.green, 3);
                    i++;
                }
            }*/
                unitToMove.SetTargetPosition(mousePosition);
        }
    }
    void CalculateNotWalkables()
    {
        foreach (NotWalkable notWalkable in notWalkableList)
        {
            GetPathfinding().GetGrid().GetXY(notWalkable.transform.position, out int x, out int y);
            GetPathfinding().GetNode(x, y).SetIsWalkable(false);
        }
    }
    void BlockAllOccupiedCells()
    {
        foreach(Unit unit in unitList)
        {
            unit.UpdateCell(false);
        }
    }

}
