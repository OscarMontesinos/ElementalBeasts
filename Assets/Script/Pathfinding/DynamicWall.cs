using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWall : MonoBehaviour
{
    public void SetWalkable()
    {
        MapPathfinder.Instance.GetPathfinding().GetGrid().GetXY(transform.position, out int x, out int y);
        MapPathfinder.Instance.GetPathfinding().GetNode(x, y).SetIsWalkable(true);
    }

    public void SetNotWalkable()
    {
        MapPathfinder.Instance.GetPathfinding().GetGrid().GetXY(transform.position, out int x, out int y);
        MapPathfinder.Instance.GetPathfinding().GetNode(x, y).SetIsWalkable(false);
    }
}
