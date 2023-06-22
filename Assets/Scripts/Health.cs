using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Get rid of SceneManagement after demo
using UnityEngine.SceneManagement;

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
            //for demo, just bring back to main menu
            if (gameObject.CompareTag("Player") & isDead)
            {
                SceneManager.LoadScene(0);
            }
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
