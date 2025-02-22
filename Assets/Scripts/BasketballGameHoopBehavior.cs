using UnityEngine;

public class BasketballGameHoopBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            var basketballComponent = other.GetComponentInParent<BasketballGameBasketballBehavior>();
            if (basketballComponent != null)
            {
                BasketballGameManager.Instance.IncrementScore();
                basketballComponent.SetHasScored();
            }
            else
            {
                throw new System.Exception("Component not found on collided object.");
            }
        }
    }
}
