using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject PauseUI;
    public GameObject Player;
    public GameObject PlayerSound;

    public bool isPaused = false;

    void Start()
    {
        PauseUI = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            Player.GetComponent<FirstPersonLook>().enabled = false;
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Player.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameUI.SetActive(true);
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Player.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
