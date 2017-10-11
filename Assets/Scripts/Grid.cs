using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{

    private T[,] _grid;

    public T[,] Array
    {
        get { return _grid; }
    }
    public int Width
    {
        get { return _grid.GetLength(0); }
    }
    public int Height
    {
        get { return _grid.GetLength(1); }
    }

    public Grid(int x, int y)
    {
        _grid = new T[x, y];
        ResetGrid();
    }

    public T GetNode(Vector2 node)
    {
        return GetNode((int)node.x, (int)node.y);
    }
    public T GetNode(int x, int y)
    {
        if (x > Width-1 || y > Height-1) { return default(T); }
        return _grid[x, y];
    }

    public void SetNode(T newNode, Vector2 node)
    {
        SetNode(newNode, (int)node.x, (int)node.y);
    }
    public void SetNode(T newNode, int x, int y)
    {
        if (x > Width-1 || y > Height-1) { return; }
        _grid[x, y] = newNode;
    }

    public void ForEachNode(Func<T,int, int, T> test)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _grid[x, y] = test(_grid[x, y], x, y);
            }
        }
    }

    public void ResetGrid(T node = default(T))
    {
        _grid = ResetGrid(_grid, node);
    }

    public void ResizeGrid(int width, int height, T newNodes = default(T), bool keepOldValues = true)
    {
        var grid = new T[width, height];
        grid = ResetGrid(grid, newNodes);
        if (!keepOldValues)
        {
            _grid = grid;
            return;
        }

        var resetX = width > _grid.GetLength(0) ? _grid.GetLength(0) : width;
        var resetY = height > _grid.GetLength(1) ? _grid.GetLength(1) : height;

        for (int fx = 0; fx < resetX; fx++)
        {
            for (int fy = 0; fy < resetY; fy++)
            {
                grid[fx, fy] = _grid[fx, fy];
            }
        }
        _grid = grid;
    }

    public Vector2[] GetNeighboursPositions(Vector2 nodePosition, bool cross = true)
    {
        List<Vector2> neighbours = new List<Vector2>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {

                if ((!cross && (y == 1 || y == -1) && (x == 1 || x == -1)) ||
                    x == 0 && y == 0)
                    continue;

                var position = new Vector2(x, y) + nodePosition;

                if (position.x > Width-1 || position.y > Height-1 || position.x < 0 || position.y < 0)
                    continue;

                neighbours.Add(position);
            }
        }
        return neighbours.ToArray();

    }
    public T[] GetNeighbours(Vector2 nodePosition, bool cross = true)
    {
        List<T> neighbours = new List<T>();
        var positions = GetNeighboursPositions(nodePosition, cross);
        for (int i = 0; i < positions.Length; i++)
        {
            neighbours.Add(GetNode(positions[i]));
        }
        return neighbours.ToArray();

    }

    public T[] GetReletives(Vector2 nodePosition, Vector2[] reletivesPosition)
    {
        var nodeX = (int)nodePosition.x;
        var nodeY = (int)nodePosition.y;

        var reletives = new List<T>();

        for (int i = 0; i < reletivesPosition.Length; i++)
        {
            reletives.Add(GetNode(nodeX + (int)reletivesPosition[i].x, nodeY + (int)reletivesPosition[i].y));
        }

        return reletives.ToArray();
    }

    private T[,] ResetGrid(T[,] grid, T node = default(T))
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = node;
            }
        }
        return grid;
    }

}