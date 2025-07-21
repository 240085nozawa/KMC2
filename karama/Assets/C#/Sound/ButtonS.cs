using UnityEngine;

public class ButtonS : MonoBehaviour
{
    public AudioSource audioSource;     // SE用AudioSource
    public AudioClip clickSound;        // クリック音

    // ボタンから呼び出す用の関数
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
