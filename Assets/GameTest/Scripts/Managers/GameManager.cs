using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public ScriptObjLibrary DataLibrary;
    public LangManager LangManager;
    public MyCamera myCam;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        if (DataLibrary == null)
            DataLibrary = GetComponentInChildren<ScriptObjLibrary>();

        if(LangManager == null)
            LangManager = GetComponentInChildren<LangManager>();

        if (myCam == null)
            myCam = Camera.main.GetComponent<MyCamera>();
    }



    public void SwitchScene(int index)
    {
        myCam.FadeToBlack();
        StartCoroutine(WaitFadeOut(index));
    }
    private IEnumerator WaitFadeOut(int index)
    {
        yield return new WaitUntil(myCam.IsFadeOut);
        SceneManager.LoadScene(index);
    }
}
