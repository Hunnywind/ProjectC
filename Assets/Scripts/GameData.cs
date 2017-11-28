using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public enum DataKind
{
    NORMALSTAGE,
    RULESCORE,
    RULETEXT,
}

public class GameData : Singleton<GameData> {

    private Dictionary<string, Dictionary<int, Dictionary<string, string>>> gameDataDictionary = new Dictionary<string, Dictionary<int, Dictionary<string, string>>>();

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        LoadData(DataKind.NORMALSTAGE);
        LoadData(DataKind.RULESCORE);
        LoadData(DataKind.RULETEXT);
    }
    private void LoadData(DataKind dataKind)
    {
        string[] line = null;
        string[] keys = null;
        string[] values = null;
        string directory = null;

        switch (dataKind)
        {
            case DataKind.NORMALSTAGE:
                directory = "NormalStage";
                break;
            case DataKind.RULESCORE:
                directory = "RuleScore";
                break;
            case DataKind.RULETEXT:
                directory = "RuleText";
                break;
            default:
                break;
        }

        TextAsset loadText = Resources.Load<TextAsset>("Data\\" + directory);
        if(loadText == null)
        {
            Debug.Log("Can't load data");
        }
        line = loadText.text.Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            line[i] = line[i].Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
        }
        keys = line[0].Split(',');

        Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>();

        for (int i = 1; i < line.Length; i++)
        {
            values = line[i].Split(',');
            Dictionary<string, string> valueD = new Dictionary<string, string>();
            for (int j = 0; j < values.Length; j++)
            {
                values[j] = values[j].Replace("@", ",");
                valueD.Add(keys[j], values[j]);
            }
            data.Add(i-1, valueD);
        }
        gameDataDictionary.Add(directory, data);
    }

    public string GetGameData(DataKind dataKind, int id, string key)
    {
        string dataName = null;
        switch (dataKind)
        {
            case DataKind.NORMALSTAGE:
                dataName = "NormalStage";
                break;
            case DataKind.RULESCORE:
                dataName = "RuleScore";
                break;
            case DataKind.RULETEXT:
                dataName = "RuleText";
                break;
            default:
                break;
        }

        return gameDataDictionary[dataName][id][key];
    }
}