using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //public void LoadScene(string sceneName)
    //{
    //    SceneManager.LoadScene(sceneName);
    //}
    public void LoadScene(string sceneName)
    {
        FadeManager.Instance.LoadScene(sceneName, 1.0f); // �t�F�[�h�t��
    }
}
