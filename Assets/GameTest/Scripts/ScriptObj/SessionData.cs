using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionData", menuName = "Info/SessionData")]
public class SessionData : ScriptableObject
{
    [SerializeField] private Difficulty _actualDifficulty;
    [SerializeField] private int _points;

    public void SetDifficulty(Difficulty selectedDifficulty)
    {
        _actualDifficulty = selectedDifficulty;
    }
    public void AddPoints(int value)
    {
        _points += value;
    }
    public void RestartPoints()
    {
        _points = 0;
    }
}
