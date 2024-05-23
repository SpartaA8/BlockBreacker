using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameStart : MonoBehaviour
{
    private TextMeshProUGUI stageNumText;
    

    private void Awake()
    {
        stageNumText = GetComponentsInChildren<TextMeshProUGUI>()[0];        
    }

    private void OnEnable()
    {
        MainSceneManager.Instance.OnFinishStageEvent += ChangeScoreUI;
    }

    private void OnDisable()
    {
        MainSceneManager.Instance.OnFinishStageEvent -= ChangeScoreUI;
    }

    private void ChangeScoreUI()
    {
        int num = MainSceneManager.Instance.StageLevel;
        if (num == 5) stageNumText.text = "Boss Stage" + "\nGame Start";
        else stageNumText.text = "Stage " + num.ToString() + "\nGame Start";
    }
}
