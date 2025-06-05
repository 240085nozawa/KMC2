using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MoveCounter : MonoBehaviour
{
    public int maxMoves = 10; // ステージごとに設定可能な手数
    public int currentMoves { get; private set; }

    public TextMeshProUGUI moveText; // TextMeshPro の UI テキスト
    public GameObject restartPanel; // 「初めから」ボタンを表示するパネル
    public Button restartButton; // 初めからボタン

    void Start()
    {
        currentMoves = maxMoves;
        UpdateMoveUI();

        // 初めからパネルを非表示
        if (restartPanel != null)
            restartPanel.SetActive(false);

        // 初めからボタンのクリックイベントを設定
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);

        
    }

    public void UseMove()
    {
        if (currentMoves > 0)
        {
            currentMoves--;
            UpdateMoveUI();

            if (currentMoves == 0)
            {
                ShowRestartUI();
            }
        }
    }

    // UI更新
    private void UpdateMoveUI()
    {
        if (moveText != null)
            moveText.text = "× " + currentMoves;
    }

    // 手数が0になったらリスタートボタンを表示
    private void ShowRestartUI()
    {
        if (restartPanel != null)
            restartPanel.SetActive(true);
    }

    // 「初めから」ボタンを押したときの処理
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
