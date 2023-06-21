using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthBar healthbar;
    public int maxHealth = 100;
    public int currentHealth; 
    public bool isDead = false;
    // Start is called before the first frame update

    public void Start()
    {

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage){
        currentHealth -=damage;
        healthbar.SetHealth(currentHealth);
        
        if(currentHealth<=0){
            isDead = true;
            if (gameObject.CompareTag("Enemy") & isDead)
            {
                Destroy(gameObject, 0);
            }
        }
    }
    public void ResetHealth(){
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    
}
