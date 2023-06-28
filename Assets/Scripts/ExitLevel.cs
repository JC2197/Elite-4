using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
