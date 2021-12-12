using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float currentScore;
    [SerializeField] Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add score and update the scoreText
    public void addScore(float addingScore)
    {
        currentScore += addingScore;
        scoreText.text = "Score: " + currentScore; 
    }


}
