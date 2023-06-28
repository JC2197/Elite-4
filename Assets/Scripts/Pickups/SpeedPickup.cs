using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public PlayerScript player;
    public float speed = 5f; //does not seem to actually determine anything at the moment?
    private float boostTimer = 2f;
    private bool boosting = false;
    public AudioSource PickupSpeedSFX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!boosting)
        {
            if (collision.gameObject.CompareTag("PickupSpeed"))
            {
                PickupSpeedSFX.Play();
                Destroy(collision.gameObject);
                StartCoroutine(PickupSpeed());
                /*
                if (boosting)
                {
                    boostTimer += Time.deltaTime;
                    if (boostTimer >= boostDuration)
                    {
                        player.moveSpeed -= speed;
                        boostTimer = 0;
                        boosting = false;
                    }
                }
                */
            }
        }
    }

    public IEnumerator PickupSpeed()
    {
        //Color origColor = gameObject.GetComponent<Renderer>().material.color;
        boosting = true;
        player.moveSpeed += speed;
        //gameObject.GetComponent<Renderer>().material.color = new Color(55, 55, 55, 55);
        yield return new WaitForSeconds(boostTimer);
        boosting = false;
        player.moveSpeed -= speed;
        //gameObject.GetComponent<Renderer>().material.color = origColor;
    }
}
