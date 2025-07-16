using System.Collections.Generic;
using UnityEngine;

public class SELECT : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("�\���E��\���Ώۂ�UI�v�f")]
    [SerializeField] private List<GameObject> SelectElements = new List<GameObject>();

    private bool isVisible = false;

    // Option�{�^���������ꂽ�Ƃ��ɌĂ΂��
    public void ToggleOptions()
    {
        isVisible = !isVisible;

        foreach (GameObject element in SelectElements)
        {
            if (element != null)
                element.SetActive(isVisible);
        }
    }

    // �J�n���͂��ׂĔ�\���ɂ���
    void Start()
    {
        foreach (GameObject element in SelectElements)
        {
            if (element != null)
                element.SetActive(false);
        }
    }
}
