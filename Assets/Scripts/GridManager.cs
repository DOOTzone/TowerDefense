using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int _width,_height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _worldCam;


    private Dictionary<Vector3, Tile> _tiles;
    private void Start()
    {
        GenerateGrid();

    }
    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector3, Tile> ();
        for (int x = 0; x < _width*2; x++)
        {
            for(int y = 0; y < _height*2; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3((float)x/2,(float)y/2), Quaternion.identity);
                spawnedTile.name = "Tile " + x + " " + y;

                var isOffset = (x%2==0 && y%2!=0)|| (y % 2 == 0 && x % 2 != 0);
                Debug.Log(isOffset);
                spawnedTile.Init(isOffset);
                _tiles[new Vector3((float)x / 2, (float)y / 2)] = spawnedTile;

            }
        }
        _worldCam.transform.position = new Vector3((float)_width / 2 -0.25f, (float)_height / 2 -0.25f,-10);
    }

    public Tile GetTileAtPosition(Vector3 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        else
            return null;
    }
}
