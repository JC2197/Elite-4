using UnityEngine;

public class Pickups : MonoBehaviour
{

    public Health playerHealth;

    public int healthBonus = 20;

    private void Awake()
    {
        playerHealth = FindObjectOfType<Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerHealth.currentHealth < playerHealth.maxHealth)
        {
            Destroy(gameObject);
            playerHealth.currentHealth += healthBonus;
        }
    }

}
