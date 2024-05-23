using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private Setting setting;
    public GameObject closeGameObject;
    public GameObject openGameObject;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //setting = openGameObject.GetComponent<Setting>();
    }

    public void GameOffBtn()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }
    public void CloseOpenBtn()
    {
        DestroyObject(closeGameObject);
        closeGameObject.SetActive(false);
        if (openGameObject == null) return;
        Instantiate(openGameObject);
        openGameObject.SetActive(true);
    }    

    public void OpenBtn()
    {
        Instantiate(openGameObject);
        openGameObject.SetActive(true);
    }

    public void GameStartBtn(bool isMulti)
    {
        GameManager.Instance.IsMulti = isMulti;
        SceneManager.LoadScene("MainScene");        
    }
}
