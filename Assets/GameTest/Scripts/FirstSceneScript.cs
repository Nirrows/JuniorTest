using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System;
using UnityEngine.Events;

public class FirstSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;
    private bool _firstTime;

    private void Start()
    {
        _firstTime = true;
        AskLang();
    }

    private void AskLang()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData(_firstTime ? "/size:up/" + LangManager.Instance.GetTranslate("greeting" + UnityEngine.Random.Range(1, 4)) + ", " + "/size:init/" + LangManager.Instance.GetTranslate("langAsk") :
                                                     "/emote:Sad/" + LangManager.Instance.GetTranslate("reLangAsk"), "Li"));

        var langChoose = new DialogData("...");

        langChoose.SelectList.Add(Language.spa, LangManager.Instance.GetTranslate(Language.spa.ToString()) + ".");
        langChoose.SelectList.Add(Language.eng, LangManager.Instance.GetTranslate(Language.eng.ToString()) + ".");
        langChoose.Callback = () => SetLang();

        dialogTexts.Add(langChoose);

        DialogManager.Show(dialogTexts);
    }
    private void SetLang()
    {
        ConfirmAction<Language>(selected => LangManager.Instance.selectedLanguage = selected,
                                "selectionAsk", _ => "/emote:Normal/" + LangManager.Instance.GetTranslate("selectionAsk"), 
                                ContinueScene);
    }
    private void ContinueScene()
    {
        Debug.Log("Inicio Continue");

        if (DialogManager.Result == "confirm")
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("/emote:Happy/" + LangManager.Instance.GetTranslate("langSave")));

            dialogTexts.Add(new DialogData("/size:up/" + LangManager.Instance.GetTranslate("difficultyIntro")));

            var difChoose = new DialogData("/size:init/" + LangManager.Instance.GetTranslate("difficultyAsk"));

            difChoose.SelectList.Add(Difficulty.easy, LangManager.Instance.GetTranslate(Difficulty.easy.ToString()));
            difChoose.SelectList.Add(Difficulty.normal, LangManager.Instance.GetTranslate(Difficulty.normal.ToString()));
            difChoose.SelectList.Add(Difficulty.hard, LangManager.Instance.GetTranslate(Difficulty.hard.ToString()));

            difChoose.Callback = () => ConfirmDifficulty();

            dialogTexts.Add(difChoose);

            DialogManager.Show(dialogTexts);
        }
        else
        {
            _firstTime = false;
            AskLang();
        }
    }
    private void ReAskDifficulty()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/emote:Sad/" + "..."));

        var difChoose = new DialogData(LangManager.Instance.GetTranslate("difficultyAskAgain"));

        difChoose.SelectList.Add(Difficulty.easy, LangManager.Instance.GetTranslate(Difficulty.easy.ToString()));
        difChoose.SelectList.Add(Difficulty.normal, LangManager.Instance.GetTranslate(Difficulty.normal.ToString()));
        difChoose.SelectList.Add(Difficulty.hard, LangManager.Instance.GetTranslate(Difficulty.hard.ToString()));

        difChoose.Callback = () => ConfirmDifficulty();

        dialogTexts.Add(difChoose);

        DialogManager.Show(dialogTexts);
    }
    private void ConfirmDifficulty()
    {
        ConfirmAction<Difficulty>(selected => GameManager.Instance.DataLibrary.SessionInfo.SetDifficulty(selected),
                                  "difficultyConfirm", selected => LangManager.Instance.GetTranslate("difficultyConfirm") + " (" + selected + ").", 
                                  ChangeScene);
    }
    private void ChangeScene()
    {
        if (DialogManager.Result == "confirm")
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("/emote:Happy/" + LangManager.Instance.GetTranslate("introFinish")));

            DialogManager.Show(dialogTexts);

            GameManager.Instance.SwitchScene(1);
        }
        else
        {
            ReAskDifficulty();
        }
    }

    private void ConfirmAction<T>(Action<T> onSelectionConfirmed, string messageKey, Func<T, string> messageFormatter, UnityAction onConfirmCallback) where T : struct, Enum
    {
        if (Enum.TryParse(DialogManager.Result, out T selected))
        {
            onSelectionConfirmed(selected);

            var dialogTexts = new List<DialogData>();

            var confirmChoose = new DialogData(messageFormatter(selected));

            confirmChoose.SelectList.Add("confirm", LangManager.Instance.GetTranslate("confirm") + ".");
            confirmChoose.SelectList.Add("cancel", LangManager.Instance.GetTranslate("cancel") + ".");

            confirmChoose.Callback = onConfirmCallback;

            dialogTexts.Add(confirmChoose);

            DialogManager.Show(dialogTexts);
        }
    }
}
