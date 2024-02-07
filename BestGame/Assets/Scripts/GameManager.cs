using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    public int Gold { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI countDownText;
    public int countDown = 10;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Gold = 29;
        UpdateGameState(GameState.Playing);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == GameState.Playing)
            {
                UpdateGameState(GameState.Paused);
                Debug.Log("Game Paused");
            }
            else if (state == GameState.Paused)
            {
                UpdateGameState(GameState.Playing);
                Debug.Log("Game Resumed");
            }
        }
    }


    //Simple state machine to update the game state
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
        Debug.Log("Gold: " + Gold);
    }

    public void StartGame(){
        StartCoroutine(StartCountDown());
    }
    
    IEnumerator StartCountDown(){
        countDownText.gameObject.SetActive(true);
        while(countDown > 0){
            countDownText.text = "Game starts in " + countDown.ToString();
            yield return new WaitForSeconds(1f);
            countDown--;
        }
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
    }
}

public enum GameState{
    Playing,
    Paused,
    GameOver
}
