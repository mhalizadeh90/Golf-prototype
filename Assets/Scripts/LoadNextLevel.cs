using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField][Range(0,10)] float DelayBeforeNextLevelLoading = 1;

    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += LoadNewLevelWithDelay;
    }

    void LoadNewLevelWithDelay()
    {
        StartCoroutine(RestartLevel(DelayBeforeNextLevelLoading));
    }
   
    public void LoadWithDelay(float delay)
    {
        StartCoroutine(RestartLevel(delay));
    }

    IEnumerator RestartLevel(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        SceneManager.LoadScene(0);
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= LoadNewLevelWithDelay;
    }

}
