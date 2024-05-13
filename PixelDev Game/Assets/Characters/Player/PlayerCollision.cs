/*using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            HealthManager.health--;
            if (HealthManager.health <= 0)
            {
                // Game over condition
                Time.timeScale = 0; // Stop the game
                Debug.Log("Game Over!"); // You can replace this with your game over logic
                gameObject.SetActive(false); // Deactivate the player
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 8); // 6 is player, 8 is enemy layers
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
*/