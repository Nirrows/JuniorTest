using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private Sprite _topSectionOpen, _lowSectionOpen;
    [SerializeField] private Sprite _topSectionClose, _lowSectionClose;

    [SerializeField] private Image[] _leftDoor, _rigthDoor, _centerDoor;

    [SerializeField, Range(1, 10)] private float _time;

    DoorInfo[] _allDoors;

    public void MakeRoom(string riddleID)
    {
        _allDoors = new DoorInfo[3];
        List<(int Index, DoorPosition Position)> doorsAvailables = Enumerable.Range(0, 3).Select(i => (i, (DoorPosition)i)).ToList();

        string[] riddleIDs = { riddleID + "True", riddleID + "False1", riddleID + "False2" };
        bool[] isCorrect = { true, false, false };

        for (int i = 0; i < 3; i++)
        {
            int index = UnityEngine.Random.Range(0, doorsAvailables.Count);
            var (doorIndex, doorPosition) = doorsAvailables[index];
            _allDoors[doorIndex] = new DoorInfo(riddleIDs[i], isCorrect[i], doorPosition);
            doorsAvailables.RemoveAt(index);
        }
    }
    public void TryOpenDoor(int index)
    {
        StartCoroutine(CR_OpenDoor(index));
    }

    public DoorInfo GetDoorInfo(DoorPosition position)
    {
        foreach (var door in _allDoors)
        {
            if (door.MyPos == position)
                return door;
        }

        return default;
    }

    private void CheckDoor(int index)
    {

    }
    private IEnumerator CR_OpenDoor(int index)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _centerDoor[0].sprite = _lowSectionOpen;

        elapsedTime = 0f;

        while (elapsedTime < _time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _centerDoor[1].sprite = _topSectionOpen;

        CheckDoor(index);
    }
}

public struct DoorInfo
{
    private string _answerID;
    private bool _correctAnswer;
    private DoorPosition _myPos;

    public DoorInfo(string myAnswer, bool isCorrect, DoorPosition position)
    {
        _answerID = myAnswer;
        _correctAnswer = isCorrect;
        _myPos = position;
    }

    public DoorPosition MyPos => _myPos;
    public string AnswerID => _answerID;
    public bool IsCorrect => _correctAnswer;
}
