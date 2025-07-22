using UnityEngine;
using System.Collections;

public class GoalJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;           // アニメーター
    public string triggerName = "Play"; // Animatorに設定したTrigger名

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Arrow"))  // Arrowタグのオブジェクトが離れたら
        {
            animator.SetTrigger(triggerName);
        }
    }
}
