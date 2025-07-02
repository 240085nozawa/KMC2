using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExcelDataLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string csvFileName = "Stage1_Template.csv";

    public List<string[]> LoadStageData()
    {
        List<string[]> stageData = new List<string[]>();
        string filePath = Path.Combine(Application.streamingAssetsPath, csvFileName);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] cells = line.Split(',');
                stageData.Add(cells);
            }
        }
        else
        {
            Debug.LogError("CSVƒtƒ@ƒCƒ‹‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ: " + filePath);
        }

        return stageData;
    }
}
