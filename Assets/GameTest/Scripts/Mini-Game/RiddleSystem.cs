using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RiddleSystem : MonoBehaviour
{
    private List<string> _riddleIDs;
    public List<string> RiddleIDs { get { return _riddleIDs; } }

    public string TakeRiddleID()
    {
        if (_riddleIDs.Count == 0)
            return null;

        string riddleID = _riddleIDs[0];
        _riddleIDs.RemoveAt(0);
        return riddleID;
    }
    public void MakeRiddleIDs(Difficulty difficulty)
    {
        _riddleIDs = new List<string>();

        int[] riddles = new int[3];

        switch (difficulty)
        {
            case Difficulty.easy:
                riddles = Flyweight.ROOMS_CREATION_EASY;
                break;
            case Difficulty.normal:
                riddles = Flyweight.ROOMS_CREATION_NORMAL;
                break;
            case Difficulty.hard:
                riddles = Flyweight.ROOMS_CREATION_HARD;
                break;
        }

        if(riddles[0] != 0)
        {
            List<int> riddlesAvailables = Enumerable.Range(1, Flyweight.RIDDLE_AMOUNT).ToList();

            for (int i = 0; i < riddles[0]; i++)
            {
                int riddleID = Random.Range(0, riddlesAvailables.Count);
                _riddleIDs.Add("riddle" + riddlesAvailables[riddleID] + Difficulty.easy);
                riddlesAvailables.RemoveAt(riddleID);
            }
        }

        if (riddles[1] != 0)
        {
            List<int> riddlesAvailables = Enumerable.Range(1, Flyweight.RIDDLE_AMOUNT).ToList();

            for (int i = 0; i < riddles[1]; i++)
            {
                int riddleID = Random.Range(0, riddlesAvailables.Count);
                _riddleIDs.Add("riddle" + riddlesAvailables[riddleID] + Difficulty.normal);
                riddlesAvailables.RemoveAt(riddleID);
            }
        }


        if (riddles[2] != 0)
        {
            List<int> riddlesAvailables = Enumerable.Range(1, Flyweight.RIDDLE_AMOUNT).ToList();

            for (int i = 0; i < riddles[2]; i++)
            {
                int riddleID = Random.Range(0, riddlesAvailables.Count);
                _riddleIDs.Add("riddle" + riddlesAvailables[riddleID] + Difficulty.hard);
                riddlesAvailables.RemoveAt(riddleID);
            }
        }
    }
}