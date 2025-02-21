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

    public Transform HoopTriggerTransform => _hoopTriggerTransform;
    [SerializeField] private Transform _hoopTriggerTransform;

    public int TotalBallCount => _totalBallCount;
    [SerializeField] private int _totalBallCount = 0;

    public int ScoreNeededToWin => _scoreNeededToWin;
    [SerializeField] private int _scoreNeededToWin = 0;

    private int _remainingBallCount = 0;
    private int _score = 0;
    private GameStateEnum _gameState;

    public void IncrementScore()
    {
        if (_gameState != GameStateEnum.Playing)
        {
            return;
        }

        _score += 1;
        ScoreUpdated?.Invoke(_score);

        DecrementRemainingBallCount();
    }

    public void DecrementRemainingBallCount()
    {
        if (_gameState != GameStateEnum.Playing)
        {
            return;
        }

        _remainingBallCount -= 1;
        RemainingBallCountUpdated?.Invoke(_remainingBallCount);

        CheckWinLoseConditions();
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

        ResetGame();
    }

    private void ResetGame()
    {
        _score = 0;
        _remainingBallCount = _totalBallCount;
        _gameState = GameStateEnum.Playing;
    }

    private void CheckWinLoseConditions()
    {
        if (_score >= _scoreNeededToWin)
        {
            _gameState = GameStateEnum.Won;
            GameStateUpdated?.Invoke(_gameState);
        }
        else if (_score + _remainingBallCount < _scoreNeededToWin)
        {
            _gameState = GameStateEnum.Lost;
            GameStateUpdated?.Invoke(_gameState);
        }
    }
}
