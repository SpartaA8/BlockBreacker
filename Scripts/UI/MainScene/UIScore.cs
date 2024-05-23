using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;   

public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI currentScoreText;

    private void Awake()
    {
        highScoreText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        currentScoreText = GetComponentsInChildren<TextMeshProUGUI>()[3];        
    }

    private void OnEnable()
    {
        MainSceneManager.Instance.OnChangeScoreEvent += ChangeScoreUI;        
    }

    private void OnDisable()
    {
        MainSceneManager.Instance.OnChangeScoreEvent -= ChangeScoreUI;        
    }

    private void ChangeScoreUI()
    {
        highScoreText.text = MainSceneManager.Instance.HighScore.ToString();
        currentScoreText.text = MainSceneManager.Instance.CurrentScore.ToString();        
    }
}
