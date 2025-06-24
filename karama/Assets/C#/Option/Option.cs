using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    [Header("表示・非表示対象のUI要素")]
    [SerializeField] private List<GameObject> optionElements = new List<GameObject>();

    private bool isVisible = false;

    // Optionボタンが押されたときに呼ばれる
    public void ToggleOptions()
    {
        isVisible = !isVisible;

        foreach (GameObject element in optionElements)
        {
            if (element != null)
                element.SetActive(isVisible);
        }
    }

    // 開始時はすべて非表示にする
    void Start()
    {
        foreach (GameObject element in optionElements)
        {
            if (element != null)
                element.SetActive(false);
        }
    }
}
