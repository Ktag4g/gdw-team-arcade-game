using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    timer timer;

    public GameObject gameoverScreen;
    public TextMeshProUGUI fishGameoverText;
    public TextMeshProUGUI pteriGameoverText;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<timer>();
    }

    public void FishGameover()
    {
        isGameActive = false;
        timer.timerOn = false;

        gameoverScreen.SetActive(true);
        fishGameoverText.gameObject.SetActive(true);
    }

    public void PteriGameover()
    {
        isGameActive = false;

        gameoverScreen.SetActive(true);
        pteriGameoverText.gameObject.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void menuReturn()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
