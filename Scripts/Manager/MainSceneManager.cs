using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SocialPlatforms.Impl;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance;

    private GameObject[] players = new GameObject[2];
    private StageController stageController;
    private GameObject ScoreUI;

    public event Action OnCopyBallEvent;
    public event Action OnFinishStageEvent;
    public event Action<bool> OnChangeLifeEvent;
    public event Action OnChangeScoreEvent;


    private bool isMulti;
    private int highScore;
    public int HighScore
    {
        get
        {
            return highScore;
        }
        private set
        {
            highScore = value;
            CallChangeScoreEvent();
        }
    }
    private int currentScore;
    public int CurrentScore 
    {
        get
        {
            return currentScore;
        } 
        private set
        {
            currentScore = value;
            CallChangeScoreEvent();
        }
    }
    private int blockCount;
    private int ballCount;
    private int stageLevel = 1;
    public int StageLevel { get => stageLevel; }
    
    private int life;
    private bool isClear;

    public ObjectPool ObjectPool { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;                        
        }        
    }

    private void Start()
    {        
        ObjectPool = GetComponent<ObjectPool>();
        players[0] = GameObject.Find("Player").transform.GetChild(0).gameObject;
        players[1] = GameObject.Find("Player").transform.GetChild(1).gameObject;        
        stageController = GameObject.Find("Stage").gameObject.GetComponent<StageController>();
        UIManager.Instance.SetActiveUI("ScoreUI", true);
        isMulti = GameManager.Instance.IsMulti;

        InitGame();
    }        
    
    // 게임 씬 처음으로 넘어왔을때 실행할 함수
    public void InitGame()
    {        
        players[1].SetActive(isMulti);        
        StartCoroutine(StartStage(stageLevel));       
        life = 0;
        CurrentScore = 0;
        HighScore = 0;
        for (int i = 0; i < 2; i++) AddLife();
        HighScore = RankBoardManager.Instance.GetHighScore();        
    }

    private IEnumerator StartStage(int stageLevel)
    {
        
        Time.timeScale = 0f;
        if(stageLevel > 1)
        {
            UIManager.Instance.SetActiveUI("StageClearUI", true);
            yield return new WaitForSecondsRealtime(2f);
            UIManager.Instance.SetActiveUI("StageClearUI", false);
        }        
        UIManager.Instance.SetActiveUI("GameStartUI", true);
        CallFinishStageEvent();
        yield return new WaitForSecondsRealtime(2f);
        UIManager.Instance.SetActiveUI("GameStartUI", false);
        Time.timeScale = 1f;                
        
        ballCount = 0;        
        isClear = false;
        blockCount = stageController.StartStage(stageLevel);        

        ResetPlayerPos();        
    }

    public void CallFinishStageEvent()
    {
        OnFinishStageEvent?.Invoke();
    }

    private void ResetPlayerPos()
    {  
        Vector3 posX = isMulti ? Vector3.left * 2 : Vector3.zero;
        Vector3 posY = Vector3.down * 4.2f;
        players[0].GetComponent<PaddleMovement>().ResetState(posX + posY);
        if (isMulti) players[1].GetComponent<PaddleMovement>().ResetState(-posX + posY);

        GameObject obj = CreateBalls();
        obj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        players[0].GetComponent<PaddleMovement>().HoldBall(obj, posX.x);
    }

    // 블록 파괴 시 
    public void DestroyBlock(int score)
    {
        CurrentScore += score;
        if(CurrentScore > HighScore) HighScore = CurrentScore;

        if (--blockCount == 0)
        {
            isClear = true;

            StartCoroutine(StartStage(++stageLevel));
        }             
    }

    private void CallChangeScoreEvent()
    {        
        OnChangeScoreEvent?.Invoke();
    }

    public void CallChangeLifeEvent(bool isAdd)
    {
        OnChangeLifeEvent?.Invoke(isAdd);
    }

    // 목숨 추가 아이템
    public void AddLife()
    {
        if (life == 5) return;
        life++;
        CallChangeLifeEvent(true);
    }

    public void ReduceLife()
    {
        if (isClear) return;
        if (life-- > 0)
        {
            ResetPlayerPos();
            CallChangeLifeEvent(false);
            return;
        }
        StartCoroutine(GameOver());
    }

    // 볼 복사 아이템
    public GameObject CreateBalls()
    {
        GameObject obj = ObjectPool.SpawnFromPool("Ball");
        if (obj == null) return null;
        BallController ball = obj.GetComponent<BallController>();        
        ballCount++;        

        return obj;
    }

    private void CallCopyBallEvent()
    {
        OnCopyBallEvent?.Invoke();
    }
    public void Copyballs()
    {
        CallCopyBallEvent();        
    }

    public void DestroyBalls()
    {
        if (--ballCount == 0) 
            ReduceLife();
    }    

    private IEnumerator GameOver()
    {
        // 시작메뉴 씬으로 이동 및 랭킹진입시 이름입력구현 필요
        Time.timeScale = 0f;
        UIManager.Instance.SetActiveUI("GameOverUI", true);
        yield return new WaitForSecondsRealtime(2f);
        UIManager.Instance.SetActiveUI("GameOverUI", false);
        Time.timeScale = 1f;
        InitGame();
    }
}
