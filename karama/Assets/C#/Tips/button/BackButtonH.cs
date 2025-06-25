using System.Collections.Generic;
using UnityEngine;

public class BackButtonH : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // バツボタンなどを押したときに、指定した UI 要素をまとめて非表示にするスクリプト

    [Header("非表示にしたいUI要素")]
    [SerializeField] private List<GameObject> targetsToHide = new List<GameObject>();

    // バツボタンが押されたときに呼び出す用（Button の OnClick に登録）
    public void HideUI()
    {
        foreach (GameObject element in targetsToHide)
        {
            if (element != null)
                element.SetActive(false);
        }
    }
}
