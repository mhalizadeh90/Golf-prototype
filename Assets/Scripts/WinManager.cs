using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    [SerializeField] IntVariable score;
    [SerializeField] FloatVariable drawSpeed;
    [SerializeField] [Range(0,2)] float SpeedIncreaseStep;
    [SerializeField] Text scoreText;

    void Start()
    {
        scoreText.text = score.value.ToString();
    }

    void OnEnable()
    {
        EnterHole.OnBallEnterHole += UpdateLevelStats;
    }

    void UpdateLevelStats()
    {
        score.value++;
        scoreText.text = score.value.ToString();
        drawSpeed.value += SpeedIncreaseStep;
    }

    void OnDisable()
    {
        EnterHole.OnBallEnterHole -= UpdateLevelStats;
    }
}
