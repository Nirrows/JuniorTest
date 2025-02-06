using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public ScriptObjLibrary DataLibrary;
    public LangManager LangManager;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        if (DataLibrary == null)
            DataLibrary = GetComponentInChildren<ScriptObjLibrary>();

        if(LangManager == null)
            LangManager = GetComponentInChildren<LangManager>();
    }
}
