using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataPlinkoManager : MonoBehaviour
{
    [SerializeField] private bool removeInitialSettings;
    private static string savePathFile => Application.persistentDataPath + "/SavesPathFile.json";
    public static PlinkoSavesDTO PlinkoSaves;

    private void Awake()
    {
        if (!removeInitialSettings)
        {
            LoadSaveFile();
        }
        else
        {
            LoadDefaultSaveFile();
        }
    }

    public static void Save()
    {
        if (File.Exists(savePathFile))
        {
            WriteDataFileValue();

        }
        else
        {
            NewDataFileValue();
        }
    }

    private void LoadSaveFile()
    {
        if (File.Exists(savePathFile))
        {
            LoadFile();

        }
        else
        {
            NewDataFileValue();
        }
    }

    private void LoadDefaultSaveFile()
    {
        NewDataFileValue();
    }

    private static void NewDataFileValue()
    {
        PlinkoSaves = (PlinkoSavesDTO)new PlinkoSavesDTO().Clone();
        File.WriteAllText(savePathFile, JsonUtility.ToJson(PlinkoSaves, prettyPrint: true));
    }

    private static void WriteDataFileValue()
    {
        File.WriteAllText(savePathFile, JsonUtility.ToJson(PlinkoSaves, prettyPrint: true));
    }

    private static void LoadFile()
    {
        string jsonFile = File.ReadAllText(savePathFile);
        PlinkoSaves = JsonUtility.FromJson<PlinkoSavesDTO>(jsonFile);
    }
}
