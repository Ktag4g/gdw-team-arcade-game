using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void QuitGame()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
