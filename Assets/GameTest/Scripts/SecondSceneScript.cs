using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System;

public class SecondSceneScript : MonoBehaviour
{

    public DialogManager DialogManager;
    private Action Test;

    private void Start()
    {
        Tuto();
    }

    private void Tuto()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData(LangManager.Instance.GetTranslate("riddle2easy")));

        dialogTexts.Add(new DialogData(LangManager.Instance.GetTranslate("tittle1") + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HALF));

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_HAPPY + "/size:up/" + LangManager.Instance.GetTranslate("tittle2"), Flyweight.DIALOG_CHAR_HALF));

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_SAD + "/size:init/" + LangManager.Instance.GetTranslate("tuto1") + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HALF));

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_SCARED + LangManager.Instance.GetTranslate("tuto2") + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HALF));

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_THINKING + LangManager.Instance.GetTranslate("tuto3"), Flyweight.DIALOG_CHAR_HALF));

        var startGame = new DialogData("Ready?");
        startGame.SelectList.Add("yes", "Continue...");
        startGame.Callback = () => Intro();

        dialogTexts.Add(startGame);
        DialogManager.Show(dialogTexts);
    }

    private void Intro()
    {
        GameManager.Instance.myCam.FadeFromBlack();
    }
    private void ReadRiddle()
    {

    }

    private void ChooseDoor()
    {

    }

    private void GrabRiddle(string ID, Difficulty value)
    {
        var dialogTexts = new List<DialogData>();

        var difChoose = new DialogData("/size:init/" + "Que puerta sera la correcta?");

        difChoose.SelectList.Add(DoorPosition.center, "Centro");
        difChoose.SelectList.Add(DoorPosition.right, "Derecha");
        difChoose.SelectList.Add(DoorPosition.left, "Izquierda");

        difChoose.Callback = () => Test();

        dialogTexts.Add(difChoose);
    }
}
