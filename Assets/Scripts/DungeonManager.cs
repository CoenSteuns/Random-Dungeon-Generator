using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    [SerializeField] private DungeonLayoutGenerator2D _generator;

    private FloodFill _filler;

    private List<Vector2>[] _rooms;

    private Grid<string> _layout;
    private Grid<GameObject> _dungeon;

	void Start () {
        _filler = new FloodFill();

        _layout = _generator.CreateDungeon();

        _rooms = _filler.GetAllRooms<string>(_layout, "Floor");

        _dungeon = GridBuilder.Instance.BuildGrid(_layout, transform);

        for (int i = 0; i < _rooms.Length; i++)
        {
            Color Coler = Random.ColorHSV();
            for (int j = 0; j < _rooms[i].Count; j++)
            {
                _dungeon.GetNode(_rooms[i][j]).GetComponent<MeshRenderer>().material.color = Coler;
            }
        }
    }

}
