using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject forceBar;
    [SerializeField] ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Stop the game and show the final score in screen
    public void GameOver()
    {
        string score = scoreManager.currentScore.ToString();
        scoreText.SetActive(false);
        forceBar.SetActive(false);
        GameOverScreen.SetActive(true);
        GameOverScreen.transform.GetChild(0).GetComponent<Text>().text = "Final Score\n" + score;
        Time.timeScale = 0;
    }

    public void RestartScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
