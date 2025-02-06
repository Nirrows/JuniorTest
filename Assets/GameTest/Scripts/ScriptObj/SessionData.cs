using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionData", menuName = "Info/SessionData")]
public class SessionData : ScriptableObject
{
    [SerializeField] private Language _actualLanguage;
    [SerializeField] private Difficulty _actualDifficulty;

    public void Set_Language(Language selectedLanguage)
    {
        _actualLanguage = selectedLanguage;
    }
    public void Set_Difficulty(Difficulty selectedDifficulty)
    {
        _actualDifficulty = selectedDifficulty;
    }
}
