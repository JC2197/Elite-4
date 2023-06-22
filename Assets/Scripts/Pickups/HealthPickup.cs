using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Health health;
    public HealthBar healthbar;
    public int healthBonus = 50;

    private void Awake()
    {
        //health = FindObjectOfType<Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (health.currentHealth < health.maxHealth)
            {
                Destroy(gameObject);
                health.currentHealth += healthBonus;
                healthbar.SetHealth(health.currentHealth);
            }
        }               
    }
}
