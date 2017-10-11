using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar {

    public Vector2[] Astar(Vector2 start, Vector2 goal)
    {
        List<Vector2> closedSet = new List<Vector2>();

        List<Vector2> openSet = new List<Vector2>
        {
            start
        };


        return openSet.ToArray();
    }

}
