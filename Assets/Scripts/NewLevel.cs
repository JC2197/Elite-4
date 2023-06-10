using UnityEngine;
using UnityEngine.SceneManagement;
public class EndTrigger : MonoBehaviour
{
 
    public GameObject levelCompleteUI;
 
 
    [SerializeField] private string newLevel;
 
 
    void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Player"))
        {
 
            levelCompleteUI.SetActive(true);
           
            SceneManager.LoadScene(newLevel);
 
        }
 
 
    }
 
 
 
}
