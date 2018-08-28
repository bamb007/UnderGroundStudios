using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                PlayerRestrictions();
            }
            else
            {
                Pause();
                PlayerRestrictions();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;     
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayerRestrictions()
    {
        if (gameIsPaused)
        {
            PlayerController.Instance.canAttack = false;
            PlayerController.Instance.canMove = false;
            PlayerController.Instance.canTakeDamage = false;
        }
        else if (!gameIsPaused)
        {
            PlayerController.Instance.canAttack = true;
            PlayerController.Instance.canMove = true;
            PlayerController.Instance.canTakeDamage = true;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
