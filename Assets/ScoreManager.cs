using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] IntVariable score;
    Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.value.ToString();
    }

    void OnEnable()
    {
        EnterHole.OnBallEnterHole += UpdateScore;
    }

    void UpdateScore()
    {
        score.value++;
        scoreText.text = score.value.ToString();
    }

    void OnDisable()
    {
        EnterHole.OnBallEnterHole -= UpdateScore;
    }
}
