using System;
using System.Collections.Generic;
using UnityEngine;


class Lang
{
    public static string lang;
    public static Dictionary<string, string> Fields { get; private set; }
    
    static Lang()
    {
        LoadLanguage("EN");
    }
    
    public static void LoadLanguage(string langType)
    {
        if (Fields == null)
            Fields = new Dictionary<string, string>();

        Fields.Clear();
        var textAsset = Resources.Load(@"Language/" + langType); //no .txt needed
        string allTexts = "";
        if (textAsset == null)
            textAsset = Resources.Load(@"Language/EN") as TextAsset; //no .txt needed
        if (textAsset == null)
            Debug.LogError("File not found for Language: Assets/Resources/Language/" + langType + ".txt");
        allTexts = (textAsset as TextAsset).text;
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        string key, value;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                    lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                Fields.Add(key, value);
            }
        }
    }
    
    public static string GetLanguage()
    {
        return Get2LetterISOCodeFromSystemLanguage().ToLower();
    }
    public static string Get2LetterISOCodeFromSystemLanguage()
    {
        SystemLanguage lang = Application.systemLanguage;
        string res = "EN";
        switch (lang)
        {
            case SystemLanguage.English: res = "EN"; break;
            case SystemLanguage.French: res = "FR"; break;
            case SystemLanguage.Portuguese: res = "PT"; break;
        }
        Debug.Log ("Lang: " + res);
        return res;
    }
}
