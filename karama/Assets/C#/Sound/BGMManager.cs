using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    void Awake()
    {
        // ���ɑ��݂���Ȃ�V�������̂�j��
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ���̃I�u�W�F�N�g���V�[���J�ڂŔj�����Ȃ�
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
