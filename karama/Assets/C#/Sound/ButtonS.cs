using UnityEngine;

public class ButtonS : MonoBehaviour
{
    public AudioSource audioSource;     // SE�pAudioSource
    public AudioClip clickSound;        // �N���b�N��

    // �{�^������Ăяo���p�̊֐�
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
