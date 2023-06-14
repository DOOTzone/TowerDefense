using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Canvas can;

    public GameState State;

    public int Health=25;

    public int Wave = 1;

    public int StartX;

    public int StartY;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private BaseEnemy _enemy1, _enemy2, _enemy3;

    [SerializeField] SceneLoader _sceneLoader;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeGameState(GameState.GenerateGrid);
        

    }

    public void RoundStart()
    {
        ChangeGameState(GameState.SpawnEnemies);
    }
    void Update()
    {
        if (Health <= 0)
            _sceneLoader.LoadScene("Menu");   

    }
    public void HealthDecrease(int power)
    {
        if (Health > power)
            Health -= power;
        else
            Health = 0;
    }

    public void ChangeGameState(GameState NewGS)
    {
        State = NewGS;

        switch (NewGS)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.GenerateRoad:
                GridManager.Instance.GenerateRoad(StartX,StartY);
                break;
            case GameState.WaitForStart:
                break;
            case GameState.SpawnEnemies:
                if (Wave == 1)
                {
                    SpawnEnemy1(1);
                }
                ChangeGameState(GameState.WaitForStart);
                break;
            case GameState.Victory:
                break;
            case GameState.Loss:
                break;
            default:
                throw new KeyNotFoundException("Game State not Found");
        }

        OnGameStateChanged?.Invoke(NewGS);

    }

    private void NextWave()
    {
        Wave++;
    }
    public enum GameState
    {
        GenerateGrid,
        GenerateRoad, 
        WaitForStart,
        SpawnEnemies,
        Victory,
        Loss
    }
    
    private void SpawnEnemy1(int count)
    {
        Instantiate(_enemy1, new Vector3((float)StartX / 2, (float)StartY / 2), Quaternion.identity);
    }
}
