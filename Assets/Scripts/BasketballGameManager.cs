using UnityEngine;

public enum GameState
{
    Playing,
    Won,
    Lost
}

public class BasketballGameManager : MonoBehaviour
{
    public static BasketballGameManager Instance;

    public Transform HoopTriggerTransform => _hoopTriggerTransform;
    [SerializeField] private Transform _hoopTriggerTransform;

    public int TotalBallCount => _totalBallCount;
    [SerializeField] private int _totalBallCount = 0;

    public int ScoreNeededToWin => _scoreNeededToWin;
    [SerializeField] private int _scoreNeededToWin = 0;

    public int RemainingBallCount => _remainingBallCount;
    private int _remainingBallCount = 0;

    public int Score => _score;
    private int _score = 0;

    public GameState GameState => _gameState;
    private GameState _gameState;

    public void IncrementScore()
    {
        _score += 1;
        DecrementRemainingBallCount();
    }

    public void DecrementRemainingBallCount()
    {
        _remainingBallCount -= 1;

        if (_gameState == GameState.Playing)
        {
            CheckWinLoseConditions();
        }
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
        _gameState = GameState.Playing;
    }

    private void CheckWinLoseConditions()
    {
        if (_score >= _scoreNeededToWin)
        {
            Debug.Log("You won!");
            _gameState = GameState.Won;
        }
        else if (_remainingBallCount == 0)
        {
            Debug.Log("You lost!");
            _gameState = GameState.Lost;
        }
    }
}
