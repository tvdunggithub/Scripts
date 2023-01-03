using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Hiển thị text khi game over
public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();
    }
}

