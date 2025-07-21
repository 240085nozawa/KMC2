using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    void Awake()
    {
        // 既に存在するなら新しいものを破棄
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // このオブジェクトをシーン遷移で破棄しない
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
