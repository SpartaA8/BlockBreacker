using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isMulti;
    public bool IsMulti
    {
        get
        {
            return isMulti;
        }
        set
        {
            isMulti = value;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);            
            IsMulti = false;
        }
    }
}
