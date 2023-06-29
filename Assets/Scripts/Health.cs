using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthBar healthbar;
    public int maxHealth = 100;
    public int currentHealth; 
    public bool isDead = false;
    bool invincible = false;
    private float invincibilityTime = 1f;
    // Start is called before the first frame update

    public void Start()
    {

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    public IEnumerator Invulnerability()
    {
        Color origColor = gameObject.GetComponent<Renderer>().material.color;
        invincible = true;
        gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
        gameObject.GetComponent<Renderer>().material.color = origColor;
    }
    public void TakeDamage(int damage){
        
        if(!invincible){
            currentHealth -=damage;
            healthbar.SetHealth(currentHealth);
            if(currentHealth<=0){
                isDead = true;
            }
            if(gameObject.tag == "Player"){
                StartCoroutine(Invulnerability());
            }
           
        }
        
    }
    public void ResetHealth(){
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    
}
