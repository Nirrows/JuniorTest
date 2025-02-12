using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Doublsb.Dialog;
using System;

public class SecondSceneScript : SceneScript
{
    private string _actualRiddle;
    private DoorPosition _currentDoor;

    bool _potsWarning, _lastDoorWarning;

    public void ReceiveRiddle(string ID)
    {
        _actualRiddle = ID;
    }
    public override void StartScene()
    {
        _potsWarning = false;
        _lastDoorWarning = false;

        var dialogTexts = new List<DialogData>();

        UnityAction callBack = () => Tuto();

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_HAPPY + LangManager.Instance.GetTranslate("tittle1") + LangManager.Instance.GetTranslate("ellipsis") + "/size:up/ " + LangManager.Instance.GetTranslate("tittle2"), Flyweight.DIALOG_CHAR_HALF, callBack));

        DialogManager.Show(dialogTexts);
    }

    private void Tuto()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_SAD + "/size:init/" + LangManager.Instance.GetTranslate("tuto1") + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HALF));

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_SCARED + LangManager.Instance.GetTranslate("tuto2") + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HALF));

        dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_THINKING + LangManager.Instance.GetTranslate("tuto3"), Flyweight.DIALOG_CHAR_HALF));

        var startGame = new DialogData(LangManager.Instance.GetTranslate("tutoAsk"));

        startGame.SelectList.Add("y", LangManager.Instance.GetTranslate("confirm"));
        startGame.SelectList.Add("n", LangManager.Instance.GetTranslate("cancel"));

        startGame.Callback = () => Intro();

        dialogTexts.Add(startGame);
        DialogManager.Show(dialogTexts);
    }
    private void Intro()
    {
        if (DialogManager.Result == "y")
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("/sound:Trap/" + LangManager.Instance.GetTranslate("ellipsis")));

            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_SCARED + "/size:up/" + LangManager.Instance.GetTranslate("fall"), Flyweight.DIALOG_CHAR_HEAD));

            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_THINK + "/size:init/" + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HEAD));

            UnityAction callBack = () => GameManager.Instance.myCam.FadeFromBlack(10);
            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_SHY + LangManager.Instance.GetTranslate("wakeUp"), Flyweight.DIALOG_CHAR_HEAD, callBack));

            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_SAD + LangManager.Instance.GetTranslate("lookTrap"), Flyweight.DIALOG_CHAR_HEAD));

            dialogTexts.Add(new DialogData("/sound:Bottles/" + Flyweight.EMOTE_HEAD_SCARED + LangManager.Instance.GetTranslate("checkPots"), Flyweight.DIALOG_CHAR_HEAD));

            switch (GameManager.Instance.DataLibrary.SessionInfo.actualDifficulty)
            {
                case Difficulty.easy:
                    dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_HAPPY + LangManager.Instance.GetTranslate("easyPots"), Flyweight.DIALOG_CHAR_HEAD));
                    break;
                case Difficulty.normal:
                    dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_SAD + LangManager.Instance.GetTranslate("easyPots"), Flyweight.DIALOG_CHAR_HEAD));
                    break;
                case Difficulty.hard:
                    dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_ANGRY + "/size:up/" + LangManager.Instance.GetTranslate("hardPots") + "/size:init/", Flyweight.DIALOG_CHAR_HEAD));
                    break;
                default:
                    dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_HAPPY + "Bien tengo las 6 intactas", Flyweight.DIALOG_CHAR_HEAD));
                    break;
            }

            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_THINK + LangManager.Instance.GetTranslate("getUp"), Flyweight.DIALOG_CHAR_HEAD));

            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_NORMAL + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HEAD));

            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_THINK + LangManager.Instance.GetTranslate("inspect"), Flyweight.DIALOG_CHAR_HEAD));

            DialogData options = new DialogData(LangManager.Instance.GetTranslate("whatDo"));

            options.SelectList.Add("1", LangManager.Instance.GetTranslate("goRock"));
            options.SelectList.Add("2", LangManager.Instance.GetTranslate("goDoorC"));
            options.SelectList.Add("3", LangManager.Instance.GetTranslate("goDoorL"));
            options.SelectList.Add("4", LangManager.Instance.GetTranslate("goDoorR"));

            dialogTexts.Add(options);

            dialogTexts.Add(new DialogData("/sound:Steps/" + Flyweight.EMOTE_HEAD_THINK + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HEAD, CheckLoop));

            DialogManager.Show(dialogTexts);
        }
        else
        {
            Tuto();
        }
    }
    private void CheckLoop()
    {
        if (DialogManager.Result == "1")
        {
            LoopToStone();
            GameManager.Instance.MovingToPoint(DoorPosition.stone);
            return;
        }
        else if (DialogManager.Result == "5")
        {
            EnterDoor();
            return;
        }
        else if (DialogManager.Result == "3")
            _currentDoor = DoorPosition.left;
        else if (DialogManager.Result == "4")
            _currentDoor = DoorPosition.right;
        else
            _currentDoor = DoorPosition.center;


        GameManager.Instance.MovingToPoint(_currentDoor);
        LoopToDoor(_currentDoor);
    }

    private void LoopToStone()
    {
        var dialogTexts = new List<DialogData>();

        if (!_lastDoorWarning)
        {
            if(GameManager.Instance.DataLibrary.SessionInfo.DoorsLeft() == 1)
            {
                dialogTexts.Add(new DialogData(Flyweight.EMOTE_HALF_HAPPY + LangManager.Instance.GetTranslate("lastDoor"), Flyweight.DIALOG_CHAR_HEAD));
                _lastDoorWarning = true;
            }
        }

        dialogTexts.Add(new DialogData(LangManager.Instance.GetTranslate("readStone")));

        dialogTexts.Add(new DialogData("/speed:init/" + LangManager.Instance.GetTranslate(_actualRiddle)));

        DialogData options = new DialogData(LangManager.Instance.GetTranslate("whatDo"));

        options.SelectList.Add("2", LangManager.Instance.GetTranslate("goDoorC"));
        options.SelectList.Add("3", LangManager.Instance.GetTranslate("goDoorL"));
        options.SelectList.Add("4", LangManager.Instance.GetTranslate("goDoorR"));
        options.SelectList.Add("1", LangManager.Instance.GetTranslate("read"));

        dialogTexts.Add(options);

        dialogTexts.Add(new DialogData("/sound:Steps/" + Flyweight.EMOTE_HEAD_NORMAL + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HEAD, CheckLoop));

        DialogManager.Show(dialogTexts);
    }
    private void LoopToDoor(DoorPosition position)
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData(LangManager.Instance.GetTranslate("readDoor")));

        dialogTexts.Add(new DialogData(LangManager.Instance.GetTranslate(GameManager.Instance.SelectedDoorInfo(position).AnswerID) + "/speed:init/"));

        DialogData options = new DialogData(LangManager.Instance.GetTranslate("whatDo"));

        switch (position)
        {
            case DoorPosition.center:
                options.SelectList.Add("5", LangManager.Instance.GetTranslate("pickDoor"));
                options.SelectList.Add("1", LangManager.Instance.GetTranslate("goRock"));
                options.SelectList.Add("3", LangManager.Instance.GetTranslate("goDoorL"));
                options.SelectList.Add("4", LangManager.Instance.GetTranslate("goDoorR"));
                options.SelectList.Add("2", LangManager.Instance.GetTranslate("read"));
                break;
            case DoorPosition.left:
                options.SelectList.Add("5", LangManager.Instance.GetTranslate("pickDoor"));
                options.SelectList.Add("1", LangManager.Instance.GetTranslate("goRock"));
                options.SelectList.Add("2", LangManager.Instance.GetTranslate("goDoorC"));
                options.SelectList.Add("4", LangManager.Instance.GetTranslate("goDoorR"));
                options.SelectList.Add("3", LangManager.Instance.GetTranslate("read"));
                break;
            case DoorPosition.right:
                options.SelectList.Add("5", LangManager.Instance.GetTranslate("pickDoor"));
                options.SelectList.Add("1", LangManager.Instance.GetTranslate("goRock"));
                options.SelectList.Add("2", LangManager.Instance.GetTranslate("goDoorC"));
                options.SelectList.Add("3", LangManager.Instance.GetTranslate("goDoorL"));
                options.SelectList.Add("4", LangManager.Instance.GetTranslate("read"));
                break;
        }

        dialogTexts.Add(options);

        dialogTexts.Add(new DialogData("/sound:Steps/" + Flyweight.EMOTE_HEAD_NORMAL + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HEAD, CheckLoop));

        DialogManager.Show(dialogTexts);
    }

    private void EnterDoor()
    {
        var dialogTexts = new List<DialogData>();

        GameManager.Instance.myCam.FadeToBlack();

        dialogTexts.Add(new DialogData("/sound:Steps/" + Flyweight.EMOTE_HEAD_NORMAL + LangManager.Instance.GetTranslate("ellipsis"), Flyweight.DIALOG_CHAR_HEAD, GoThroughDoor));

        DialogManager.Show(dialogTexts);
    }
    private void GoThroughDoor()
    {
        var dialogTexts = new List<DialogData>();

        if (GameManager.Instance.TrySelectedDoor(_currentDoor))
        {
            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_HAPPY + LangManager.Instance.GetTranslate("correctDoor"), Flyweight.DIALOG_CHAR_HEAD));
        }
        else
        {
            dialogTexts.Add(new DialogData("/sound:Spike/" + Flyweight.EMOTE_HEAD_SCARED + LangManager.Instance.GetTranslate("takeDmg"), Flyweight.DIALOG_CHAR_HEAD));
        }

        if(!GameManager.Instance.StillHavePots() & !_potsWarning)
        {
            dialogTexts.Add(new DialogData(Flyweight.EMOTE_HEAD_ANGRY + LangManager.Instance.GetTranslate("noLifes"), Flyweight.DIALOG_CHAR_HEAD));
            _potsWarning = true;
        }

        GameManager.Instance.myCam.FadeFromBlack();

        dialogTexts.Add(new DialogData("/sound:Steps/" + Flyweight.EMOTE_HEAD_NORMAL + LangManager.Instance.GetTranslate("newRoom"), Flyweight.DIALOG_CHAR_HEAD, LoopToStone));

        GameManager.Instance.MovingToPoint(DoorPosition.stone);

        DialogManager.Show(dialogTexts);
    }
}
