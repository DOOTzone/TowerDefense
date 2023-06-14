using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width,_height;

    [SerializeField] private Tile _groundTile, _mountainTile, _roadTile, _endTile;

    [SerializeField] private Point _movePoint;

    [SerializeField] private Transform _worldCam;
    private int _lastX;
    private int _lastY;

    private void Awake()
    {
        Instance = this;
    }
    private Dictionary<Vector3, Tile> _tiles;
    private void Start()
    {

    }
   
   public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector3, Tile> ();
        for (int x = 0; x < _width*2; x++)
        {
            for(int y = 0; y < _height*2; y++)
            {
                var randTile = Random.Range(0, 16) == 5 ? _mountainTile : _groundTile;
                var spawnedTile = Instantiate(randTile, new Vector3((float)x/2,(float)y/2), Quaternion.identity);
                spawnedTile.name = "Tile " + x + " " + y;
                spawnedTile.Init(x,y);
                _tiles[new Vector3((float)x / 2, (float)y / 2)] = spawnedTile;

            }
        }
        _worldCam.transform.position = new Vector3((float)_width / 2 -0.25f, (float)_height / 2 -0.25f,-10);

        GameManager.Instance.ChangeGameState(GameManager.GameState.GenerateRoad);
    }
    /// <summary>
    /// Generates the road on the map
    /// </summary>
    /// <param name="startX">the starting position on the x axis of the generation</param>
    /// <param name="startY">the starting position on the y axis of the generation</param>
    public virtual void GenerateRoad(int startX, int startY)
    {
        _lastX = startX;
        _lastY = startY;
        genUp(11,false);
        genRight(7,false);
        genDown(5,false);
        genLeft(3,true);
        GameManager.Instance.ChangeGameState(GameManager.GameState.WaitForStart);
    }
    /// <summary>
    /// generates a certain amount of squares upwards from the last position and if it is the last piece of road generates the end point of the road
    /// </summary>
    /// <param name="amount">amount of squares to generate</param>
    /// <param name="last">decides if it is the last piece of road</param>
    public void genUp(int amount,bool last)
    {
        for (int y = 1; y <= amount; y++)
        {
            var _startTile = _tiles[new Vector3((float)_lastX / 2, (float)(_lastY+y) / 2)];
            Destroy(GetTileAtPosition(new Vector3((float)_lastX / 2, (float)(_lastY + y) / 2)).gameObject);
            _startTile = Instantiate(_roadTile, new Vector3((float)_lastX / 2, (float)(_lastY + y) / 2), Quaternion.identity);
            _startTile.name = "Tile " + _lastX + " " + (_lastY + y);
            Instantiate(_movePoint, new Vector3((float)_lastX / 2, (float)(_lastY+y) / 2), Quaternion.identity);

        }
        _lastY=_lastY + amount;
        Instantiate(_movePoint, new Vector3((float)_lastX / 2, (float)_lastY / 2), Quaternion.identity);
    }
    /// <summary>
    /// generates a certain amount of squares downwards from the last position and if it is the last piece of road generates the end point of the road
    /// </summary>
    /// <param name="amount">amount of squares to generate</param>
    /// <param name="last">decides if it is the last piece of road</param>
    public void genDown(int amount, bool last)
    {
        for (int y = amount; y > 0; y--)
        {
            var _startTile = _tiles[new Vector3((float)_lastX / 2, (float)(_lastY - y) / 2)];
            Destroy(GetTileAtPosition(new Vector3((float)_lastX / 2, (float)(_lastY - y) / 2)).gameObject);
            _startTile = Instantiate(_roadTile, new Vector3((float)_lastX / 2, (float)(_lastY - y) / 2), Quaternion.identity);
            _startTile.name = "Tile " + _lastX + " " + (_lastY + y);
            Instantiate(_movePoint, new Vector3((float)_lastX / 2, (float)(_lastY-(amount+1-y)) / 2), Quaternion.identity);
        }
        _lastY = _lastY - amount;
        Instantiate(_movePoint, new Vector3((float)_lastX / 2, (float)_lastY / 2), Quaternion.identity);
    }
    /// <summary>
    /// generates a certain amount of squares to the right of the last position and if it is the last piece of road generates the end point of the road
    /// </summary>
    /// <param name="amount">amount of squares to generate</param>
    /// <param name="last">decides if it is the last piece of road</param>
    public void genRight(int amount, bool last)
    {
        for (int x = 1; x <= amount; x++)
        {
            var _startTile = _tiles[new Vector3((float)(_lastX+x) / 2, (float)_lastY/2)];
           Destroy(GetTileAtPosition(new Vector3((float)(_lastX + x) / 2, (float)_lastY / 2)).gameObject);
            _startTile = Instantiate(_roadTile, new Vector3((float)(_lastX + x) / 2, (float)_lastY / 2), Quaternion.identity);
            _startTile.name = "Tile " + (_lastX + x) + " "+ _lastY;
            Instantiate(_movePoint, new Vector3((float)(_lastX +x)/ 2, (float)_lastY / 2), Quaternion.identity);
        }
        _lastX = _lastX + amount;
        
    }
    /// <summary>
    /// generates a certain amount of squares to the left of the last position and if it is the last piece of road generates the end point of the road
    /// </summary>
    /// <param name="amount">amount of squares to generate</param>
    /// <param name="last">decides if it is the last piece of road</param>
    public void genLeft(int amount, bool last)
    {
        for (int x = amount; x >0; x--)
        {
            var _startTile = _tiles[new Vector3((float)(_lastX - x) / 2, (float)_lastY / 2)];
            Destroy(GetTileAtPosition(new Vector3((float)(_lastX - x) / 2, (float)_lastY / 2)).gameObject);
            _startTile = Instantiate(_roadTile, new Vector3((float)(_lastX - x) / 2, (float)_lastY / 2), Quaternion.identity);
            _startTile.name = "Tile " + (_lastX - x) + " " + _lastY;
            Instantiate(_movePoint, new Vector3((float)(_lastX-(amount+1 - x)) / 2, (float)_lastY / 2), Quaternion.identity);

        }
        _lastX = _lastX - amount;
        if (last)
        {
            var _EndTile = _tiles[new Vector3((float)(_lastX - 1) / 2, (float)_lastY / 2)];
            Destroy(GetTileAtPosition(new Vector3((float)(_lastX - 1) / 2, (float)_lastY / 2)).gameObject);
            _EndTile = Instantiate(_endTile, new Vector3((float)(_lastX - 1) / 2, (float)_lastY / 2),Quaternion.identity);
            _EndTile.name = "End Tile";
            Instantiate(_movePoint, new Vector3((float)(_lastX - 1) / 2, (float)_lastY / 2), Quaternion.identity);
        }
       
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
