using UnityEngine;
using UnityEngine.SceneManagement;
public class EndTrigger : MonoBehaviour
{
 
 
 
    [SerializeField] private string newLevel;
 
 
    void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Player"))
        {
 
           
            SceneManager.LoadScene(newLevel);
 
        }
 
 
    }
 
 
 
}
