using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private SceneScript _actualScene;

    public ScriptObjLibrary DataLibrary;
    public LangManager LangManager;
    public MyCamera myCam;

    [SerializeField] private RiddleSystem _riddleSystem;
    [SerializeField] private DoorSystem _doorSystem;
    [SerializeField] private MapSystem _mapSystem;

    private void Awake()
    {
        DataLibrary.SessionInfo.SetGameStart();

        if (_instance == null)
            _instance = this;

        if (DataLibrary == null)
            DataLibrary = GetComponentInChildren<ScriptObjLibrary>();

        if(LangManager == null)
            LangManager = GetComponentInChildren<LangManager>();

        if (myCam == null)
            myCam = Camera.main.GetComponent<MyCamera>();

        if (DataLibrary.SessionInfo.isGameStarted)
            CreateRooms(DataLibrary.SessionInfo.actualDifficulty);
    }

    private void Start()
    {
        _actualScene?.StartScene();
    }

    private void CreateRooms(Difficulty difficulty)
    {
        _riddleSystem.MakeRiddleIDs(difficulty);
        StartRoom();
    }

    private void StartRoom()
    {
        string riddleID = _riddleSystem.TakeRiddleID();
        _doorSystem?.MakeRoom(riddleID);
        _actualScene.GetComponent<SecondSceneScript>()?.ReceiveRiddle(riddleID);
    }

    public DoorInfo SelectedDoorInfo(DoorPosition position)
    {
        return _doorSystem.GetDoorInfo(position);
    }
    public void SwitchScene(int index)
    {
        myCam.FadeToBlack();
        StartCoroutine(WaitFadeOut(index));
    }
    private IEnumerator WaitFadeOut(int index)
    {
        yield return new WaitUntil(myCam.IsFadeOut);
        DataLibrary.SessionInfo.SetGameStart();
        SceneManager.LoadScene(index);
    }
}
