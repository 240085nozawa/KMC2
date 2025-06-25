using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("表示・非表示対象のUI要素")]
    [SerializeField] private List<GameObject> tipsElements = new List<GameObject>();

    private bool isVisible = false;

    // Optionボタンが押されたときに呼ばれる
    public void ToggleTips()
    {
        isVisible = !isVisible;

        foreach (GameObject element in tipsElements)
        {
            if (element != null)
                element.SetActive(isVisible);
        }
    }

    // 開始時はすべて非表示にする
    void Start()
    {
        foreach (GameObject element in tipsElements)
        {
            if (element != null)
                element.SetActive(false);
        }
    }
}
