using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTrigger : MonoBehaviour

{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            print("ENTER");
            //TakeDamage(20)
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            print("STAY");

            var magnitude = 2500; // how far is character knocked back
            var force = transform.position - other.transform.position; // force vector

            force.Normalize(); // normalize force vector to get direction only and trim magnitude
            gameObject.GetComponent<Rigidbody2D>().AddForce(force * magnitude);


        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            print("EXIT");
        }
    }

}
