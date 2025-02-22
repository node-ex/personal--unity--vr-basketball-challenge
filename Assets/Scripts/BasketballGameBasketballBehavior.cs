using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BasketballGameBasketballBehavior : MonoBehaviour
{
    [SerializeField] float _yPositionThresholdBuffer = 0.5f;

    private XRGrabInteractable _grabInteractable;
    private Rigidbody _rigidbody;
    private Vector3? _initialPosition;
    private Quaternion? _initialRotation;
    private bool _wasGrabbed;
    private bool _isWaitingForDisabling;
    private bool _hasScored;
    private float _lastYPosition;

    public void SetHasScored()
    {
        _hasScored = true;
        DisableBallAfterDelay();
    }

    public void Reset()
    {
        transform.SetPositionAndRotation(_initialPosition.Value, _initialRotation.Value);
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _hasScored = false;
        _wasGrabbed = false;
        _isWaitingForDisabling = false;
        _lastYPosition = transform.position.y;
    }

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _rigidbody = GetComponent<Rigidbody>();

        StoreInitialTransform();
        Reset();
    }

    private void Update()
    {
        CheckFailureCondition();
    }

    private void CheckFailureCondition()
    {
        if (_hasScored || _isWaitingForDisabling)
        {
            return;
        }

        if (_grabInteractable.isSelected)
        {
            _wasGrabbed = true;
            _lastYPosition = transform.position.y;
            return;
        }

        if (!_wasGrabbed)
        {
            _lastYPosition = transform.position.y;
            return;
        }

        float yPositionThreshold = BasketballGameManager.Instance.HoopTriggerTransform.position.y - _yPositionThresholdBuffer;

        bool isYPositionDecreasing = transform.position.y < _lastYPosition;
        bool isLowerThanHoop = transform.position.y < yPositionThreshold;

        if (isYPositionDecreasing && isLowerThanHoop)
        {
            BasketballGameManager.Instance.DecrementRemainingBallCount();
            DisableBallAfterDelay();
        }

        _lastYPosition = transform.position.y;
    }

    private void DisableBallAfterDelay()
    {
        _isWaitingForDisabling = true;
        Invoke(nameof(DisableBall), 1f);
    }

    private void DisableBall()
    {
        gameObject.SetActive(false);
    }

    private void StoreInitialTransform()
    {
        if (_initialPosition != null && _initialRotation != null)
        {
            return;
        }

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }
}
