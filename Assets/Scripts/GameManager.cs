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

    PteriMovement pteriMechanics;
    FishMechanics fishMechanics;

    private void Update()
    {
        if (isGameActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuReturn();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                restartGame();
            }
        }
    }

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<timer>();

        pteriMechanics = GameObject.Find("Pteri").GetComponent<PteriMovement>();
        fishMechanics = GameObject.Find("Fish").GetComponent<FishMechanics>();

        isGameActive = true;
    }

    public void FishGameover()
    {
        isGameActive = false;
        timer.timerOn = false;

        pteriMechanics.enabled = false;
        fishMechanics.enabled = false;

        gameoverScreen.SetActive(true);
        fishGameoverText.gameObject.SetActive(true);
    }

    public void PteriGameover()
    {
        isGameActive = false;

        pteriMechanics.enabled = false;
        fishMechanics.enabled = false;

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
