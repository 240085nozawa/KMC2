using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadBegin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Button restartButton; // 初めからボタン

    void Start()
    {
        // 初めからボタンのクリックイベントを設定
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 「初めから」ボタンを押したときの処理
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
