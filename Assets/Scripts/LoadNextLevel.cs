using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField][Range(0,10)] float DelayBeforeNextLevelLoading = 1;

    void OnEnable()
    {
        EnterHole.OnBallEnterHole += LoadWithDelay;
    }

    void LoadWithDelay()
    {
        Invoke("RestartLevel", DelayBeforeNextLevelLoading);
    }
    public void LoadWithDelay(float delay)
    {
        Invoke("RestartLevel", delay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();
    }

    void OnDisable()
    {
        EnterHole.OnBallEnterHole -= LoadWithDelay;
    }

}
