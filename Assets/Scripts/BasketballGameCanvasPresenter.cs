using TMPro;
using UnityEngine;

public class BasketballGameUIPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _ballCountText;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _loseText;
    [SerializeField] private GameObject _resetButton;

    private void Awake()
    {
        BasketballGameManager.Instance.ScoreUpdated += OnScoreUpdated;
        BasketballGameManager.Instance.RemainingBallCountUpdated += OnRemainingBallCountUpdated;
        BasketballGameManager.Instance.GameStateUpdated += OnGameStateUpdated;

        OnScoreUpdated(0);
        OnRemainingBallCountUpdated(BasketballGameManager.Instance.TotalBallCount);
        OnGameStateUpdated(GameStateEnum.Playing);
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
            case GameStateEnum.Playing:
                _winText.gameObject.SetActive(false);
                _loseText.gameObject.SetActive(false);
                _resetButton.SetActive(false);
                break;
            case GameStateEnum.Won:
                Invoke(nameof(ShowWinUI), 2f);
                break;
            case GameStateEnum.Lost:
                Invoke(nameof(ShowLoseUI), 2f);
                break;
        }
    }

    private void ShowWinUI()
    {
        _winText.gameObject.SetActive(true);
        _resetButton.SetActive(true);
    }

    private void ShowLoseUI()
    {
        _loseText.gameObject.SetActive(true);
        _resetButton.SetActive(true);
    }
}
