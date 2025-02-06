using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


public class LangManager : MonoBehaviour
{
    private static LangManager _instance;
    public static LangManager Instance { get { return _instance; } }

    [SerializeField] private string externalURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQsKNch9Cd2p8Wdk11Rys2_zF-W7swB6Mm7u71caFCdpjoAbu5GIponeG7Ohym4GBLkN12YhrnfcKsa/pub?gid=0&single=true&output=csv";
    [SerializeField] private TextAsset localCSV;

    public Language selectedLanguage;
    public Dictionary<Language, Dictionary<string, string>> LanguageManager;

    public event Action OnUpdate = delegate { };

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _instance = this;

        if (localCSV != null)
            LoadLocalCSV();
        else
            StartCoroutine(DownloadCSV(externalURL));
    }

    public void CallUpdate()
    {
        if (LanguageManager == null)
            return;

        OnUpdate();
    }
    public void ChangeLanguage(Language newLang)
    {
        selectedLanguage = newLang;
        OnUpdate();
    }

    public string GetTranslate(string _id)
    {
        if (!LanguageManager[selectedLanguage].ContainsKey(_id))
            return "Error 404: Not Found";
        else
            return LanguageManager[selectedLanguage][_id];
    }
    public void LoadLocalCSV()
    {
        LanguageManager = LanguageU.loadCodexFromString("local", localCSV.text);
        OnUpdate();
    }
    public IEnumerator DownloadCSV(string url)
    {
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        LanguageManager = LanguageU.loadCodexFromString("www", www.downloadHandler.text);

        OnUpdate();
    }
}
