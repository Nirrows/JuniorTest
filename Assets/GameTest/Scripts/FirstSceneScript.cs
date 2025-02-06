using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System;

public class FirstSceneScript : MonoBehaviour, ILangListener
{
    public DialogManager DialogManager;

    private void Start()
    {
        GameManager.Instance.SubscribeLangListener(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.LangManager.CallUpdate();
        }
    }
    public void OnUpdateLang()
    {
        AskLang();
    }

    private void AskLang()
    {
        var dialogTextsA = new List<DialogData>();

        dialogTextsA.Add(new DialogData("/size:up/" + LangManager.Instance.GetTranslate("greeting" + UnityEngine.Random.Range(1, 4)) + ", " + "/size:init/" + LangManager.Instance.GetTranslate("langAsk"), "Li"));

        var langChoose = new DialogData("...");

        langChoose.SelectList.Add("spa", LangManager.Instance.GetTranslate("spa") + ".");
        langChoose.SelectList.Add("eng", LangManager.Instance.GetTranslate("eng") + ".");
        langChoose.Callback = () => { Debug.Log("AskLang callback"); SetLang(); };

        dialogTextsA.Add(langChoose);

        DialogManager.Show(dialogTextsA);
    }

    public void SetLang()
    {
        if (Enum.TryParse(DialogManager.Result, out Language selected))
        {
            LangManager.Instance.selectedLanguage = selected;

            var dialogTextsB = new List<DialogData>();

            var confirmChoose = new DialogData(LangManager.Instance.GetTranslate("selectionAsk"));

            confirmChoose.SelectList.Add("confirm", LangManager.Instance.GetTranslate("confirm") + ".");
            confirmChoose.SelectList.Add("cancel", LangManager.Instance.GetTranslate("cancel") + ".");

            confirmChoose.Callback = () => { Debug.Log("SetLang callback"); ContinueScene(); };

            dialogTextsB.Add(confirmChoose);

            DialogManager.Show(dialogTextsB);
        }
    }

    public void ContinueScene()
    {
        Debug.Log("Inicio Continue");

        if (DialogManager.Result == "confirm")
        {
            var dialogTextsC = new List<DialogData>();

            dialogTextsC.Add(new DialogData(LangManager.Instance.GetTranslate("langSave")));

            DialogManager.Show(dialogTextsC);
        }
        else
        {
            AskLang();
        }
    }
}
