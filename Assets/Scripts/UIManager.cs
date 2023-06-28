using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button resetLevel;
    public TextMeshProUGUI resetLevelTxt;
    public Button backToMenu;
    public TextMeshProUGUI backToMenuTxt;
    public TextMeshProUGUI youWinTxt;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        resetLevel.interactable = false;
        resetLevelTxt.enabled = false;
        backToMenu.interactable = false;
        backToMenuTxt.enabled = false;
        youWinTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            resetLevel.interactable = true;
            resetLevelTxt.enabled = true;
            backToMenu.interactable = true;
            backToMenuTxt.enabled = true;
            youWinTxt.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
            
        }

    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void StatScreen()
    {
        Debug.Log("Show stat screen...");
    }

    public void ResetTheLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
