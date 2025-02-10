using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionData", menuName = "Info/SessionData")]
public class SessionData : ScriptableObject
{
    public Difficulty actualDifficulty { get; private set; }
    public bool isGameStarted { get; private set; }
    [SerializeField] private int _points;

    private int _doorsAmount;
    private int _lifePoints;

    public void SetGameStart()
    {
        isGameStarted = true;
    }
    public void SetDifficulty(Difficulty selectedDifficulty)
    {
        actualDifficulty = selectedDifficulty;
    }
    public void AddPoints(int value)
    {
        _points += value;
    }
    public void RestartPoints()
    {
        _points = 0;
    }

    public int GetTotalRiddles()
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
