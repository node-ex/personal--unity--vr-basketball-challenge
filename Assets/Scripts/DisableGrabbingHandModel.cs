using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DisableGrabbingHandModel : MonoBehaviour
{
    [SerializeField] private GameObject leftHandModel;
    [SerializeField] private GameObject rightHandModel;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        ChangeModelState(args.interactorObject.transform, false);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        ChangeModelState(args.interactorObject.transform, true);
    }

    private void ChangeModelState(Transform transform, bool isActive)
    {
        if (transform.CompareTag("Left Hand"))
        {
            leftHandModel.SetActive(isActive);
        }
        else if (transform.CompareTag("Right Hand"))
        {
            rightHandModel.SetActive(isActive);
        }
    }
}
