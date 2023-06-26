using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Health health;
    public HealthBar healthbar;
    public int healthBonus = 50;
    public AudioSource PickupHealthSFX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (health.currentHealth < health.maxHealth)
            {
                PickupHealthSFX.Play();
                Destroy(gameObject);
                health.currentHealth += healthBonus;
                healthbar.SetHealth(health.currentHealth);
            }
        }               
    }
}
