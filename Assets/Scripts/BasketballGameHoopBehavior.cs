using UnityEngine;

public class BasketballGameHoopBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            var basketball = other.GetComponentInParent<BasketballGameBasketballBehavior>();
            if (basketball != null)
            {
                BasketballGameManager.Instance.IncrementScore();
                basketball.SetHasScored();
            }
            else
            {
                throw new System.Exception("Component not found on collided object.");
            }
        }
    }
}
