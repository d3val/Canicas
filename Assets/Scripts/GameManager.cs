using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject scoreText;
    [SerializeField] ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {

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
        GameOverScreen.SetActive(true);
        GameOverScreen.transform.GetChild(0).GetComponent<Text>().text = "Final Score\n" + score;
        Time.timeScale = 0;
    }
}
