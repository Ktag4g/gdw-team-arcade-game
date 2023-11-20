using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    GameManager gameManager;

    public float timeLeft;
    public bool timerOn = false;

    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                timerOn = false;
                gameManager.PteriGameover();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float time = Mathf.FloorToInt(currentTime);


        timeText.text = "Time: " + time;
    }
}
