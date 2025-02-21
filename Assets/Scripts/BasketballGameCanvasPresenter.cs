using TMPro;
using UnityEngine;

public class BasketballGameUIPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _ballCountText;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _loseText;

    private void Start()
    {
        BasketballGameManager.Instance.ScoreUpdated += OnScoreUpdated;
        BasketballGameManager.Instance.RemainingBallCountUpdated += OnRemainingBallCountUpdated;
        BasketballGameManager.Instance.GameStateUpdated += OnGameStateUpdated;

        OnScoreUpdated(0);
        OnRemainingBallCountUpdated(BasketballGameManager.Instance.TotalBallCount);
    }

    private void OnDestroy()
    {
        BasketballGameManager.Instance.ScoreUpdated -= OnScoreUpdated;
        BasketballGameManager.Instance.RemainingBallCountUpdated -= OnRemainingBallCountUpdated;
        BasketballGameManager.Instance.GameStateUpdated -= OnGameStateUpdated;
    }

    private void OnScoreUpdated(int currentScore)
    {
        int scoreNeededToWin = BasketballGameManager.Instance.ScoreNeededToWin;
        _scoreText.text = $"{currentScore}/{scoreNeededToWin}";
    }

    private void OnRemainingBallCountUpdated(int remainingBallCount)
    {

        int totalBallCount = BasketballGameManager.Instance.TotalBallCount;
        _ballCountText.text = $"{remainingBallCount}/{totalBallCount}";
    }

    private void OnGameStateUpdated(GameStateEnum gameState)
    {
        switch (gameState)
        {
            case GameStateEnum.Won:
                _winText.gameObject.SetActive(true);
                break;
            case GameStateEnum.Lost:
                _loseText.gameObject.SetActive(true);
                break;
        }
    }
}
