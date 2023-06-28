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

    private void OnCollisionEnter2D(Collision2D other)
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
        yield return new WaitForSeconds(0.10f);
        invincible = false;
    }

    // Code is used below to prevent player from standing still and taking no damage
    private void OnCollisionStay2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Enemy") && !invincible)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            var magnitude = 200;
            Vector2 force = transform.position - other.transform.position;
            force.Normalize();
            rb.AddForce(force * magnitude);            
        }        
    }
}
