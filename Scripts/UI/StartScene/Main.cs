using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject UI;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeMain());
    }

    public IEnumerator MakeMain()
    {
        yield return new WaitForSeconds(2.8f);
        UI.SetActive(true);
        Instantiate(ball);
        yield break;
    }
}
