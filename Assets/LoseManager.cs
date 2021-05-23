using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    [SerializeField] GameObject LosePanel;
    [SerializeField] [Range(0, 10)] float DelayBeforeShowGameOver = 1;
    
    void Awake()
    {
        LosePanel.SetActive(false);
    }

    void OnEnable()
    {
        PhysicCheck.OnBallLandedOutsideHole += ShowGameOverPanel;
    }


    void ShowGameOverPanel()
    {
        Invoke("ShowGameOverPanelWithDelay", DelayBeforeShowGameOver);
    }

    void ShowGameOverPanelWithDelay()
    {
        LosePanel.SetActive(true);
    }

    void OnDisable()
    {
        PhysicCheck.OnBallLandedOutsideHole -= ShowGameOverPanel;
    }

}
