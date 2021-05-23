using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    [SerializeField] Text HighScoreText;
    [SerializeField] Text CurrentScoreText;
    [SerializeField] IntVariable score;
    const string highScore = "HighScore";

    void Awake()
    {
        if (!PlayerPrefs.HasKey(highScore))
            PlayerPrefs.SetInt(highScore, 0);
    }

    void OnEnable()
    {
        PhysicCheck.OnBallLandedOutsideHole += showScore;
    }

    void showScore()
    {
        if(score.value > PlayerPrefs.GetInt(highScore))
            PlayerPrefs.SetInt(highScore, score.value);

        HighScoreText.text = PlayerPrefs.GetInt(highScore).ToString();
        CurrentScoreText.text = score.value.ToString();
    }

    void OnDisable()
    {
        PhysicCheck.OnBallLandedOutsideHole -= showScore;
    }
}
