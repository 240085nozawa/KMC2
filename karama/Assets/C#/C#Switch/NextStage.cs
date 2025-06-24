using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class NextStage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // "次のステージへ" ボタンを押したときに次のステージへ進む

    public Button nextStageButton;

    void Start()
    {
        if (nextStageButton != null)
        {
            nextStageButton.onClick.AddListener(LoadNextStage);
        }
        else
        {
            Debug.LogError("NextStageボタンが設定されていません。Inspectorで設定してください。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadNextStage()
    {
        string currentScene = StageTracker.LastPlayedStage;

        Match match = Regex.Match(currentScene, @"TGS(\d+)-(\d+)"); // ← ここを修正

        if (!match.Success || match.Groups.Count < 3)
        {
            Debug.LogError("シーン名が TGSX-Y の形式ではありません: " + currentScene);
            return;
        }

        int group = int.Parse(match.Groups[1].Value); // TGSのグループ番号
        int stage = int.Parse(match.Groups[2].Value); // ステージ番号

        // 次の候補を生成
        string nextStageName = $"TGS{group}-{stage + 1}";

        // 存在するかチェック（Build Settingsに登録されている必要あり）
        if (Application.CanStreamedLevelBeLoaded(nextStageName))
        {
            SceneManager.LoadScene(nextStageName);
        }
        else
        {
            // 存在しない場合は次のグループの1ステージ目に進む
            string nextGroupName = $"TGS{group + 1}-1";
            if (Application.CanStreamedLevelBeLoaded(nextGroupName))
            {
                SceneManager.LoadScene(nextGroupName);
            }
            else
            {
                Debug.Log("これ以上ステージが存在しません。ゲームクリア？");
            }
        }
    }
}
