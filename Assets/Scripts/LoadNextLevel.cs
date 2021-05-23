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
        Invoke("LoadLevel", DelayBeforeNextLevelLoading);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            LoadLevel();
    }

    void OnDisable()
    {
        EnterHole.OnBallEnterHole -= LoadWithDelay;
    }

}
