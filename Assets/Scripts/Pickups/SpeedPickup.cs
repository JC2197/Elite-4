using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public PlayerScript player;
    public float speedBoost = 162f; //does not seem to actually determine anything at the moment?
    private float boostTimer = 2f;
    private bool boosting = false;
    public AudioSource PickupSpeedSFX;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!boosting)
        {
            if (collision.gameObject.CompareTag("PickupSpeed"))
            {
                PickupSpeedSFX.Play();
                Destroy(collision.gameObject);
                StartCoroutine(PickupSpeed());
            }
        }
    }

    public IEnumerator PickupSpeed()
    {
        boosting = true;
        player.moveSpeed += speedBoost;
        yield return new WaitForSeconds(boostTimer);
        boosting = false;
        player.moveSpeed -= speedBoost;
    }
}
