
using UnityEngine;
using UnityEngine.UI;

public class LangTranslator : MonoBehaviour
{
    public string TextId;

    // Use this for initialization
    void Start()
    {
        var text = GetComponent<Text>();
        if (text != null)
            if(TextId == "ISOCode")
                text.text = Lang.GetLanguage();
            else
                text.text = Lang.Fields[TextId];
    }
}
