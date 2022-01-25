using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameScreen;
    public GameObject pauseScreen;
    public GameObject deadScreen;

    public GameObject m_char;

    public float increasedSpd;

    public TMP_Text scoreTxt;
    public TMP_Text finalScoreTxt;
    public double score = 0;
    

    private void Start()
    {
        Time.timeScale = 1;

        pauseScreen.SetActive(false);
        deadScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    private void Update()
    {
        if (m_char == null)
        {
            deadScreen.SetActive(true);
            gameScreen.SetActive(false);
            finalScoreTxt.text = "Score: " + score;
        }

        CheckScore();
    }

    private void FixedUpdate()
    {
        IncreaseScore();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        Time.timeScale = 0;

        pauseScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        if (m_char != null)
        {
            score++;
            scoreTxt.text = score.ToString();
        }
    }

    public void CheckScore()
    {
        if (score == 2653 || score == 6543 ||
            score == 10453 || score == 14832 ||
            score == 19327 || score == 24216 ||
            score == 29127 || score == 39123 ||
            score == 51372 || score == 64325 ||
            score == 79547 || score == 95237 ||
            score == 129877 || score == 169564 ||
            score == 238546 || score == 276765)
        {
            NewCC.fwdSpeed += increasedSpd;
        }
    }
}
