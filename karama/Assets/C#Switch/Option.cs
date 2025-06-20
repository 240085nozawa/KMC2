using UnityEngine;

public class Option : MonoBehaviour
{
    [Header("�I�v�V����UI")]
    [SerializeField] private GameObject optionPanel; // �w�i�p�l���i���{�������j
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;

    private bool isVisible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ToggleOptions()
    {
        isVisible = !isVisible;

        optionPanel.SetActive(isVisible);
        button1.SetActive(isVisible);
        button2.SetActive(isVisible);
        button3.SetActive(isVisible);
    }

    // �J�n���͔�\���ɂ���
    void Start()
    {
        optionPanel.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
}
