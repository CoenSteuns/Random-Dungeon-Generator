using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour {

    #region Serializable tile
    [System.Serializable]
    private class TileObject
    {
        public string Name;
        public GameObject Object;
    }
    [SerializeField] private TileObject[] Tiles;
    #endregion

    public static GridBuilder Instance = null;
    private Dictionary<string, GameObject> _tiles = new Dictionary<string, GameObject>();


    public void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        for (int i = 0; i < Tiles.Length; i++)
        {
            _tiles.Add(Tiles[i].Name, Tiles[i].Object);
        }
    }

    public Grid<GameObject> BuildGrid(Grid<string> layout, Transform parent = null)
    {
        var grid = new Grid<GameObject>(layout.Width, layout.Height);
        grid.ForEachNode((node, x, y) =>
        {
            //var tile = Instantiate(_tiles[layout.GetNode(x, y)]);
            var tile = ObjectPool.Instance.GetObject(layout.GetNode(x, y));
            tile.transform.parent = parent;
            tile.transform.localPosition = new Vector3(x, 0, y);

            return tile;
        });
        return grid;
    }
}
