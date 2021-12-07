using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//classes que queremos gravar sao marcados como serializable
[System.Serializable]
public class SaveData
{
    //criar uma nova referencia caso nao exista
    private static SaveData _currentSave;
    public static SaveData currentSave
    {
        get
        {
            if (_currentSave == null)
            {
                _currentSave = new SaveData();
            }
            return _currentSave;
        }
    }

    public List<TeamCharacter> savedTeamCharacter;


}
