using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rotateCon : MonoBehaviour
{
    public float interval = 1.5f;        // 回転の間隔（秒）
    public float anglePerStep = 90f;     // 1回あたりの回転角度
    public float rotateDuration = 0.3f;  // 回転にかける時間（滑らかにする）

    private void Start()
    {
        StartCoroutine(AutoRotateLoop());
    }

    IEnumerator AutoRotateLoop()
    {
        while (true)
        {
            yield return RotateSmoothly(anglePerStep);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator RotateSmoothly(float angle)
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, angle, 0);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }
}
