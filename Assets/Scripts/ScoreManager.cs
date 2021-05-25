using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Fields

    [Header("Game Current Score References")]
    [SerializeField] Text scoreTextUI;
    
    [Header("Game Over Scores References")]
    [SerializeField] Text HighScoreText;
    [SerializeField] Text CurrentScoreText;
    
    [Header("Score File Reference")]
    [SerializeField] IntVariable scoreRef;
    
    const string highScore = "HighScore";

    #endregion

    void Start()
    {
        InitializeHighScoreSavedData();
        UpdateInGameScoreUI();
    }

    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += UpdateInGameScore;
        LandingGroundCheck.OnBallLandedOutsideHole += UpdateGameOverScores;
    }

    void UpdateInGameScore()
    {
        scoreRef.value++;
        UpdateInGameScoreUI();
    }
    
    void UpdateGameOverScores()
    {
        SetHighScoreValue();

        HighScoreText.text = PlayerPrefs.GetInt(highScore).ToString();
        CurrentScoreText.text = scoreRef.value.ToString();
    }

    private void UpdateInGameScoreUI()
    {
        scoreTextUI.text = scoreRef.value.ToString();
    }
    
    private static void InitializeHighScoreSavedData()
    {
        if (!PlayerPrefs.HasKey(highScore))
            PlayerPrefs.SetInt(highScore, 0);
    }

    private void SetHighScoreValue()
    {
        if (scoreRef.value > PlayerPrefs.GetInt(highScore))
            PlayerPrefs.SetInt(highScore, scoreRef.value);
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= UpdateInGameScore;
        LandingGroundCheck.OnBallLandedOutsideHole -= UpdateGameOverScores;
    }
}
