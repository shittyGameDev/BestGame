using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    public int Gold { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateGameState(GameState.Playing);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == GameState.Playing)
            {
                UpdateGameState(GameState.Paused);
            }
            else if (state == GameState.Paused)
            {
                UpdateGameState(GameState.Playing);
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        this.state = newState;

        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        Debug.Log("Gold: " + Gold);
    }

    public void SpendGold(int amount)
    {
        Gold -= amount;
    }
}

public enum GameState{
    Playing,
    Paused,
    GameOver
}
