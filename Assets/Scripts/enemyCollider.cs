using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollider : MonoBehaviour
{
    public Health health;
    private bool invincible = false;
    public float invincibilityTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!invincible)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                health.TakeDamage(20);
                StartCoroutine(Invulnerability());
            }
        }
    }

    IEnumerator Invulnerability()
    {
        Color origColor = gameObject.GetComponent<Renderer>().material.color;
        invincible = true;
        gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
        gameObject.GetComponent<Renderer>().material.color = origColor;
    }

    private void OnTriggerStay2D(Collider2D other)
    {        
        if (other.gameObject.CompareTag("Enemy"))
        {
            // this can be used to push objects outside of others to stop clipping issues if needed
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            var magnitude = 10; // how far is character knocked back
            Vector2 force = transform.position - other.transform.position; // force vector
            force.Normalize(); // normalize force vector to get direction only and trim magnitude
            rb.AddForce(force * magnitude);
        }        
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
