using UnityEngine;
using System.Collections.Generic;


public class StageGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ExcelDataLoader dataLoader;

    public GameObject whiteTilePrefab;
    public GameObject redTilePrefab;
    public GameObject blueTilePrefab;
    public GameObject blackTilePrefab;
    public GameObject yellowTilePrefab;
    public GameObject greenTilePrefab;
    public GameObject purpleTilePrefab;

    public GameObject playerPrefab;
    public GameObject goalPrefab;

    void Start()
    {
        var stage = dataLoader.LoadStageData();
        GenerateStage(stage);
    }

    void GenerateStage(List<string[]> stageData)
    {
        for (int z = 0; z < stageData.Count; z++)
        {
            string[] row = stageData[z];
            for (int x = 0; x < row.Length; x++)
            {
                string cell = row[x].Trim();
                Vector3 pos = new Vector3(x, 0, z);

                // 完全一致の場合（タイル単体）
                if (IsTileOnly(cell))
                {
                    Instantiate(GetTilePrefab(cell), pos, Quaternion.identity);
                }
                // 複合（Player色 / Goal色）対応
                else if (cell.StartsWith("Player"))
                {
                    string color = cell.Substring("Player".Length);
                    Instantiate(GetTilePrefab(color), pos, Quaternion.identity);
                    Instantiate(playerPrefab, pos + Vector3.up, Quaternion.identity);
                }
                else if (cell.StartsWith("Goal"))
                {
                    string color = cell.Substring("Goal".Length);
                    Instantiate(GetTilePrefab(color), pos, Quaternion.identity);
                    Instantiate(goalPrefab, pos + Vector3.up, Quaternion.identity);
                }
            }
        }
    }

    bool IsTileOnly(string keyword)
    {
        switch (keyword)
        {
            case "White":
            case "Red":
            case "Blue":
            case "Black":
            case "Yellow":
            case "Purple":
            case "Green":
                return true;
            default:
                return false;
        }
    }

    GameObject GetTilePrefab(string color)
    {
        switch (color)
        {
            case "White": return whiteTilePrefab;
            case "Red": return redTilePrefab;
            case "Blue": return blueTilePrefab;
            case "Black": return blackTilePrefab;
            case "Yellow": return yellowTilePrefab;
            case "Purple": return purpleTilePrefab;
            case "Green": return greenTilePrefab;
            default:
                Debug.LogError("未対応の色です: " + color);
                return null;
        }
    }
}
