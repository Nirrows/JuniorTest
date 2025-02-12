using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionData", menuName = "Info/SessionData")]
public class SessionData : ScriptableObject
{
    public Difficulty actualDifficulty { get; private set; }
    public bool isGameStarted { get; private set; }
    [SerializeField] private float _points;

    public float TotalPoints { get { return _points; } }

    private int _doorsAmount;

    public void SetGameStart()
    {
        isGameStarted = true;
        _doorsAmount = GetTotalRiddles();
    }
    public void SetDifficulty(Difficulty selectedDifficulty)
    {
        actualDifficulty = selectedDifficulty;
    }

    public void AddPoints(float value)
    {
        switch (actualDifficulty)
        {
            case Difficulty.easy:
                _points += value;
                break;
            case Difficulty.normal:
                _points += value * 1.5f;
                break;
            case Difficulty.hard:
                _points += value * 3;
                break;
            default:
                _points += value;
                break;
        }
    }
    public void RestartPoints()
    {
        _points = 0;
    }
    public void DoorClear()
    {
        _doorsAmount -= 1;
    }
    public int DoorsLeft()
    {
        return _doorsAmount;
    }

    private int GetTotalRiddles()
    {
        switch (actualDifficulty)
        {
            case Difficulty.easy:
                return 6;
            case Difficulty.normal:
                return 8;
            case Difficulty.hard:
                return 12;
            default:
                return 6;
        }
    }
    public int GetTotalLife()
    {
        switch (actualDifficulty)
        {
            case Difficulty.easy:
                return 6;
            case Difficulty.normal:
                return 3;
            case Difficulty.hard:
                return 1;
            default:
                return 6;
        }
    }
}
