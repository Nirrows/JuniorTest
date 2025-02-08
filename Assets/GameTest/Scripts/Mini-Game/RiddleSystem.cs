using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class RiddleSystem : MonoBehaviour
{
    Difficulty _difficulty;

    private void Awake()
    {
        _difficulty = GameManager.Instance.DataLibrary.SessionInfo.actualDifficulty;
    }

    public DialogData GetRiddle()
    {
        string ID = "riddle" + Random.Range(1, Flyweight.RIDDLE_AMOUNT + 1) + _difficulty.ToString();

        return null;
    }

    //Se genera un ID de manera random para el acertijo
    //Al ID se suma el ID de Respuestas true, false1, false2
    //El acertijo se manda a la "Roca"
    //Cada respuesta se manda a una puerta
    //Podria ser un String[] de 4 el GetRiddle y la Scena maneja lo demas.
}
