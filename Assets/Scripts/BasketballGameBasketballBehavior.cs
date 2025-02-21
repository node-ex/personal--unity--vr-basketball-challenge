using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BasketballGameBasketballBehavior : MonoBehaviour
{
    [SerializeField] float _yPositionThresholdBuffer = 0.5f;

    private XRGrabInteractable _grabInteractable;
    private bool _wasGrabbed;
    private bool _isWaitingForDestruction;
    private bool _hasScored;
    private float _lastYPosition;

    public void SetHasScored()
    {
        _hasScored = true;
        DestroyBall();
    }

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();

        _hasScored = false;
        _wasGrabbed = false;
        _isWaitingForDestruction = false;
        _lastYPosition = _grabInteractable.transform.position.y;
    }

    private void Update()
    {
        CheckFailureCondition();
    }

    private void CheckFailureCondition()
    {
        if (_hasScored || _isWaitingForDestruction)
        {
            return;
        }

        if (_grabInteractable.isSelected)
        {
            _wasGrabbed = true;
            _lastYPosition = _grabInteractable.transform.position.y;
            return;
        }

        if (!_wasGrabbed)
        {
            _lastYPosition = _grabInteractable.transform.position.y;
            return;
        }

        float yPositionThreshold = BasketballGameManager.Instance.HoopTriggerTransform.position.y - _yPositionThresholdBuffer;

        bool isYPositionDecreasing = _grabInteractable.transform.position.y < _lastYPosition;
        bool isLowerThanHoop = _grabInteractable.transform.position.y < yPositionThreshold;

        if (isYPositionDecreasing && isLowerThanHoop)
        {
            BasketballGameManager.Instance.DecrementRemainingBallCount();
            DestroyBall();
        }

        _lastYPosition = _grabInteractable.transform.position.y;
    }

    private void DestroyBall()
    {
        _isWaitingForDestruction = true;
        Destroy(gameObject, 1f);
    }
}
