using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doublsb.Dialog;

public class GameOverDialog : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] SessionData _data;
    [SerializeField] LangManager _lang;

    private void Start()
    {
        var dialogTexts = new List<DialogData>();

        DialogData again = new DialogData(_lang.GetTranslate("over"));

        again.SelectList.Add("y", _lang.GetTranslate("confirm"));
        again.SelectList.Add("n", _lang.GetTranslate("cancel"));

        again.Callback = () => Choose();

        dialogTexts.Add(again);

        DialogManager.Show(dialogTexts);
    }

    public void Choose()
    {
        if(DialogManager.Result == "y")
        {
            _data.RestartPoints();
            SceneManager.LoadScene(1);
        }
        else
        {
            _data.RestartPoints();
            SceneManager.LoadScene(0);
        }
    }
}
