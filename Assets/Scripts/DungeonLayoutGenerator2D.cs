using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonLayoutGenerator2D {

    [System.Serializable]
    private class dungeonTile
    {
        public float _min;
        public float _max;
        public string _name;
    }

    [Header("Tiles")]
    [SerializeField] private string _default;
    [SerializeField] private dungeonTile[] _tiles;
    [Space(7), Header("Grid")]
    [SerializeField] private int _width = 80;
    [SerializeField] private int _height = 50;
    [SerializeField] private float _scale = 6;


    public Grid<string> CreateDungeon()
    {
        var valueGrid = PerlinNoiseGrid.CreatePerlinNoiseGrid(_width,_height, _scale);

        var grid = new Grid<string>(valueGrid.Width, valueGrid.Height);
        grid.ForEachNode((node, x, y) =>
        {
            var valueNode = valueGrid.GetNode(x, y);
            var avalibleTileExists = false;
            for (int i = 0; i < _tiles.Length; i++)
            {
                if(valueNode > _tiles[i]._min && valueNode < _tiles[i]._max)
                {
                    node = _tiles[i]._name;
                    avalibleTileExists = true;
                    break;
                }
            }
            if (!avalibleTileExists)
                node = _default;

            return node;
        });
        return grid;

    }

}
