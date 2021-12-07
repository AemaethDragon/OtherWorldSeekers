using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;

public class SerializationManager
{
    //nome do save e o objeto em que queremos guardar as informaçao que retorna um boleano se o save foi feito com sucesso
    public static bool Save(string saveName, object saveData)
    {
        //BinaryFormatter é a forma mais segura de nao se conseguir ler facilmente os ficheiros de save no Unity
        BinaryFormatter formatter = GetBinaryFormatter();

        //defnir o caminho do ficheiro de save
        //cria o caminho caso nao exista e armazena multiplas saves
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        //caminho da pasta para os save files (o nome do ficheiro pode ser o que for necesario ex: .dat .bat .data...)
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    //vai ler o caminho onde se encontra as saves e carregar o gameobject "save"
    public static object Load(string path)
    {
        //verificar se o caminho existe
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        //tenta ler o ficheiro de save e caso nao o consiga abrir ou nao existe save retorna null
        try
        {
            object save = formatter.Deserialize(file);

            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat(" Failed to Load Save File at {0}", path);
            file.Close();
            return null;
        }
    }

    //transforma a save em dados binarios
    private static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();

        Vector3SerializableSurrogate vector3Surrogate = new Vector3SerializableSurrogate();
        Vector2SerializableSurrogate vector2Surrogate = new Vector2SerializableSurrogate();
        QuaternionSerializableSurrogate quaternionSurrogate = new QuaternionSerializableSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
