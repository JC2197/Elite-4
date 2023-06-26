using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollider : MonoBehaviour

{
    public Health health;
    private bool invincible = false;
    public float invincibilityTime = 1f;
    public AudioSource PlayerTakeDamageSFX;
    void Awake()
    {
        PlayerTakeDamageSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!invincible)
        {            
            if (other.gameObject.CompareTag("Enemy"))
            {
                PlayerTakeDamageSFX.Play();
                health.TakeDamage(20);
                StartCoroutine(Invulnerability());  
            }
        }
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

    public IEnumerator AttackInvulnerability()
    {        
        invincible = true;
        yield return new WaitForSeconds(0.25f);
        invincible = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        /*
        if (other.gameObject.CompareTag("Enemy") && !invincible)
        {
            // this can be used to push objects outside of others to stop clipping issues if needed
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            var magnitude = 500; // how far is character knocked back
            Vector2 force = transform.position - other.transform.position; // force vector
            force.Normalize(); // normalize force vector to get direction only and trim magnitude
            rb.AddForce(force * magnitude);            
        }
        */
    }

    /* 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("EXIT");
        }
    }
    */

}
