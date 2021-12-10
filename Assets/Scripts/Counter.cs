using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private ScoreManager scoreManager;
    [SerializeField] TextMesh scoreLabel;
    [SerializeField] float scoreValue = 100;

    private void Start()
    {
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        scoreLabel.text = scoreValue.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        scoreManager.addScore(scoreValue);
    }

    private void OnTriggerExit(Collider other)
    {
        scoreManager.addScore(-scoreValue);
    }
}
