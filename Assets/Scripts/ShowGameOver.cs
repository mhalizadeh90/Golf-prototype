using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOver : MonoBehaviour
{
    #region Fields
    
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] [Range(0, 10)] float DelayBeforeGameOver = 1;

    #endregion

    void Awake()
    {
        GameOverPanel.SetActive(false);
    }

    void OnEnable()
    {
        LandingGroundCheck.OnBallLandedOutsideHole += ShowGameOverPanel;
    }
    void ShowGameOverPanel()
    {
        StartCoroutine(ShowGameOverPanelWithDelay());
    }

    IEnumerator ShowGameOverPanelWithDelay()
    {
        yield return new WaitForSeconds(DelayBeforeGameOver);
        GameOverPanel.SetActive(true);
    }

    void OnDisable()
    {
        LandingGroundCheck.OnBallLandedOutsideHole -= ShowGameOverPanel;
    }

}
