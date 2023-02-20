using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private float cellsize;
    private TGridObject[,] gridArray;
    public Grid(int width, int height, float cellsize,Func<Grid<TGridObject>,int, int,TGridObject> createdGridObject)
    {
        this.cellsize = cellsize;
        this.width = width;
        this.height = height;

        gridArray = new TGridObject[width, height];


        int x = 0;
        int y = 0;
        while (x < gridArray.GetLength(0))
        {
            while (y < gridArray.GetLength(1))
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
                gridArray[x, y] = createdGridObject(this, x, y);

                y++;
            }
            y = 0;
            x++;
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public float GetCellsize()
    {
        return cellsize;
    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize;
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x,y;
        GetXY(worldPosition, out x, out y);
        return gridArray[x, y];

    }public TGridObject GetGridObject(int x, int y)
    {
        
        return GetGridObject(new Vector3(x,y));

    }
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        if (worldPosition.x >= 0 && worldPosition.y >= 0 && worldPosition.x < width && worldPosition.y < height)
        {
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

    }

    public void GetXY(Vector3 worldposition, out int x, out int y)
    {
            x = Mathf.FloorToInt(worldposition.x / cellsize);
            y = Mathf.FloorToInt(worldposition.y / cellsize);
    }

    
}
