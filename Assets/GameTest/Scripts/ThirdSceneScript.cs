using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class ThirdSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] SessionData _data;
    [SerializeField] LangManager _lang;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData(_lang.GetTranslate(_data.actualDifficulty.ToString() + "Result") + " " + _data.TotalPoints.ToString(), "Li"));

        dialogTexts.Add(new DialogData("/emote:Happy/" + _lang.GetTranslate("end1"), "Li"));
        dialogTexts.Add(new DialogData("/emote:Sad/" + _lang.GetTranslate("end2"), "Li"));
        dialogTexts.Add(new DialogData("/emote:Happy/" + _lang.GetTranslate("end3"), "Li"));

        dialogTexts.Add(new DialogData("/speed:down/" + "The End..."));

        DialogManager.Show(dialogTexts);
    }
}
