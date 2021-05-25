using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    #region Fields

    [SerializeField] IntVariable scoreRef;
    [SerializeField] Text scoreTextUI;

    #endregion

    void Start()
    {
        UpdateScoreTextInUI();
    }

    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += UpdateStats;
    }

    void UpdateStats()
    {
        scoreRef.value++;
        UpdateScoreTextInUI();
    }

    private void UpdateScoreTextInUI()
    {
        scoreTextUI.text = scoreRef.value.ToString();
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= UpdateStats;
    }
}
