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

    Action WrongDoorEvent;
    Action CorrectDoorEvent;

    private void Awake()
    {
        DataLibrary.SessionInfo.SetDifficulty(Difficulty.hard);
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
        {
            _mapSystem.SetLifePoints(DataLibrary.SessionInfo.GetTotalLife());

            CorrectDoorEvent += StartRoom;
            CorrectDoorEvent += () => DataLibrary.SessionInfo.AddPoints(Flyweight.BASE_POINTS);

            WrongDoorEvent += _mapSystem.RemoveLife;
            CreateRooms(DataLibrary.SessionInfo.actualDifficulty);
        }
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

    public void MovingToPoint(DoorPosition position)
    {
        _mapSystem.Moving(position);
    }
    public bool StillHavePots()
    {
        return _mapSystem.isAlive;
    }
    public bool TrySelectedDoor(DoorPosition curretDoor)
    {
        DataLibrary.SessionInfo.DoorClear();

        if(DataLibrary.SessionInfo.DoorsLeft() == 1)
        {
            SceneManager.LoadScene(2);
        }


        if (_doorSystem.GetDoorInfo(curretDoor).IsCorrect)
        {
            CorrectDoorEvent?.Invoke();
            _mapSystem.ModifyPoints(DataLibrary.SessionInfo.TotalPoints);
            return true;
        }
        else
        {
            WrongDoorEvent?.Invoke();
            return false;
        }
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
