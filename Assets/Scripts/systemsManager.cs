using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class systemsManager : MonoBehaviour
{
    public Button resetLevel;
    public TextMeshProUGUI resetLevelTxt;

    private void Awake()
    {
        resetLevel.interactable = false;
        resetLevelTxt.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            resetLevel.interactable = true;
            resetLevelTxt.enabled = true;
        }
    }

    public void ResetTheLevel()
    {
        // Reload active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
