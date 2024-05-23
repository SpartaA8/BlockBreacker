using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private List<GameObject> UIPrefabs;
    private List<GameObject> UIList;
    private GameObject parent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;            
            if (SceneManager.GetActiveScene().name != "MainScene") return;
            UIList = new List<GameObject>();
            parent = GameObject.Find("UI");
            foreach (GameObject go in UIPrefabs)
            {
                GameObject obj = Instantiate(go, parent.transform);
                obj.name = go.name;
                UIList.Add(obj);
            }
        }
    }

    public void Start()
    {
        
    }

    public void SetActiveUI(string name, bool active)
    {
        UIList.Find(x => x.name == name).SetActive(active);
    }
}
