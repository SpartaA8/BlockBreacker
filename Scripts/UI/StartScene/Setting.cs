using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject settingLetter;

    public GameObject soundBar1;
    public GameObject soundBar2;
    public GameObject soundBar3;
    public GameObject backBtn;

    int count = 0;


    private void Start()
    {
        StartCoroutine(MakeSetting());
    }
    public IEnumerator MakeSetting()
    {
        settingLetter.SetActive(false);
        soundBar1.SetActive(false);
        soundBar2.SetActive(false);
        soundBar3.SetActive(false);
        backBtn.SetActive(false);

        settingLetter.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        soundBar1.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        soundBar2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        soundBar3.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        backBtn.SetActive(true);

        yield break;
    }
}
