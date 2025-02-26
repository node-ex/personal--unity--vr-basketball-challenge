using System;
using UnityEngine;

public enum GameStateEnum
{
    Playing,
    Won,
    Lost
}

public class BasketballGameManager : MonoBehaviour
{
    public static BasketballGameManager Instance;

    public event Action<int> ScoreUpdated;
    public event Action<int> RemainingBallCountUpdated;
    public event Action<GameStateEnum> GameStateUpdated;

    [SerializeField] private GameObject[] _basketballRoots;

    public Transform HoopTriggerTransform => _hoopTriggerTransform;
    [SerializeField] private Transform _hoopTriggerTransform;

    public int ScoreNeededToWin => _scoreNeededToWin;
    [SerializeField] private int _scoreNeededToWin = 0;

    public int TotalBallCount => _totalBallCount;
    private int _totalBallCount = 0;

    private int _remainingBallCount = 0;
    private int _score = 0;
    private GameStateEnum _gameState;

    public void IncrementScore()
    {
        if (_gameState != GameStateEnum.Playing)
        {
            return;
        }

        SetScore(_score + 1);
        DecrementRemainingBallCount();
    }

    public void DecrementRemainingBallCount()
    {
        if (_gameState != GameStateEnum.Playing)
        {
            return;
        }

        SetRemainingBallCount(_remainingBallCount - 1);
        CheckWinLoseConditions();
    }

    [ContextMenu("Reset Game")]
    public void ResetGame()
    {
        ResetBasketballRoots();
        SetTotalBallCount(_basketballRoots.Length);
        SetRemainingBallCount(_totalBallCount);
        SetScore(0);
        SetGameState(GameStateEnum.Playing);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetGame();
    }

    private void CheckWinLoseConditions()
    {
        if (_score >= _scoreNeededToWin)
        {
            SetGameState(GameStateEnum.Won);
            Invoke(nameof(PlayWonSound), 2f);
        }
        else if (_score + _remainingBallCount < _scoreNeededToWin)
        {
            SetGameState(GameStateEnum.Lost);
            Invoke(nameof(PlayLostSound), 2f);
        }
    }

    private void SetTotalBallCount(int totalBallCount)
    {
        _totalBallCount = totalBallCount;
        SetRemainingBallCount(_totalBallCount);
    }

    private void SetRemainingBallCount(int remainingBallCount)
    {
        _remainingBallCount = remainingBallCount;
        RemainingBallCountUpdated?.Invoke(_remainingBallCount);
    }

    private void SetScore(int score)
    {
        _score = score;
        ScoreUpdated?.Invoke(_score);
    }

    private void SetGameState(GameStateEnum gameState)
    {
        _gameState = gameState;
        GameStateUpdated?.Invoke(_gameState);
    }

    private void ResetBasketballRoots()
    {
        for (int i = 0; i < _basketballRoots.Length; i++)
        {
            _basketballRoots[i].SetActive(true);
            _basketballRoots[i].SendMessage("Reset");
        }
    }

    private void PlayWonSound()
    {
        AudioManager.Instance.Play("Won");
    }

    private void PlayLostSound()
    {
        AudioManager.Instance.Play("Lost");
    }
}
