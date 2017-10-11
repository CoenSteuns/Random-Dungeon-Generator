using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill {

    private List<Vector2> check;

    public List<Vector2>[] GetAllRooms<T>(Grid<T> grid, T example)
    {
        List<List<Vector2>> rooms = new List<List<Vector2>>();

        grid.ForEachNode((node, x, y) => {

            var pos = new Vector2(x, y);

            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].Contains(pos))
                    return node;
            }

            var room = GetRoomAround<T>(x, y, grid, example);
            if (room.Count > 0)
                rooms.Add(room);

            return node;
        });


        return rooms.ToArray();
    }
    public List<Vector2> GetRoomAround<T>(int x, int y, Grid<T> grid, T example)
    {
        check = new List<Vector2>();
        var room = Floodfill<T>(x, y, grid, example);
        return room == null ? new List<Vector2>() : room;
    }

    private List<Vector2>Floodfill<T>(int x, int y, Grid<T> grid, T example)
    {

        List<Vector2> room = new List<Vector2>();
        check.Add(new Vector2(x, y));
        if (!EqualityComparer<T>.Default.Equals(grid.GetNode(x, y), example))
            return null;

        var pos = new Vector2(x, y);
        room.Add(pos);
        var neighboursPosition = grid.GetNeighboursPositions(pos);
        for (int i = 0; i < neighboursPosition.Length; i++)
        {

            if (check.Contains(neighboursPosition[i]))
                continue;

            check.Add(neighboursPosition[i]);
            var neighbours = Floodfill<T>((int)neighboursPosition[i].x, (int)neighboursPosition[i].y, grid, example);


            if (neighbours == null)
                continue;

            for (int j = 0; j < neighbours.Count; j++)
            {
                room.Add(neighbours[j]);
            }
        }

        return room;
    }
}
