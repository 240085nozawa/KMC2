using UnityEngine;
using System.Collections;

public class GoalJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;           // �A�j���[�^�[
    public string triggerName = "Play"; // Animator�ɐݒ肵��Trigger��

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Arrow"))  // Arrow�^�O�̃I�u�W�F�N�g�����ꂽ��
        {
            animator.SetTrigger(triggerName);
        }
    }
}
