using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int healthIncreaseAmount = 10; 

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.IncreaseHealth(healthIncreaseAmount);
                AudioManager.instance.PlaySound("heal");
            }

            Destroy(gameObject);
        }
    }
}
