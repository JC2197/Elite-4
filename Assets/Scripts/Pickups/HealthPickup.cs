using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Health health;
    public HealthBar healthbar;
    public int healthBonus = 50;
    public AudioSource PickupHealthSFX;

    void OnCollisionEnter2D(Collision2D collision)
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
