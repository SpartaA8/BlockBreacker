using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public GameObject lifeSpriteObject;

    private Stack<GameObject> lifeStack;

    private void Start()
    {
        lifeStack = new Stack<GameObject>();
        MainSceneManager.Instance.OnChangeLifeEvent += ChangeLifeCount;               
    }

    private void ChangeLifeCount(bool isAdd)
    {
        if (isAdd) drawLife();
        else eraseLife();
    }   

    private void drawLife()
    {
        GameObject obj = Instantiate(lifeSpriteObject,transform);
        obj.transform.position += Vector3.up * lifeStack.Count * 0.6f;
        lifeStack.Push(obj);        
    }

    private void eraseLife()
    {
        GameObject obj = lifeStack.Pop();
        Destroy(obj);
    }
}
