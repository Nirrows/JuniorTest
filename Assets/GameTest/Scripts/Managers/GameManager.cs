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

    [SerializeField] private MyCamera _myCamera;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        if (DataLibrary == null)
            DataLibrary = GetComponentInChildren<ScriptObjLibrary>();

        if(LangManager == null)
            LangManager = GetComponentInChildren<LangManager>();

        if (_myCamera == null)
            _myCamera = Camera.main.GetComponent<MyCamera>();
    }

    public void SwitchScene(int index)
    {
        _myCamera.FadeToBlack();
        StartCoroutine(WaitFadeOut(index));
    }

    private IEnumerator WaitFadeOut(int index)
    {
        yield return new WaitUntil(_myCamera.IsFadeOut);
        SceneManager.LoadScene(index);
    }
}
