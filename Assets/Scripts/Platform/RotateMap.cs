using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour
{

    public bool isBossDefeated = false;
    private int rotateAmount =-90;
    public float rotationDuration = 2f; // Thời gian để hoàn thành xoay từ 0 đến 90 độ

    // Start được gọi trước khi khung hình đầu tiên được cập nhật
    void Start()
    {
        if (!isBossDefeated)
        {
            StartCoroutine(RotateRepeatedly(15f));
        }
        else
        {
            return;
        }
    }

    // Update được gọi mỗi khung hình

    IEnumerator Rotate(float duration)
    {
        rotateAmount -= 90;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotateAmount);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            
            float t = elapsedTime / duration; // Tính toán tỷ lệ thời gian hoàn thành của quá trình xoay
            ShakeCamera.Instance.Shake(1.5f, t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo đối tượng được xoay chính xác về góc 90 độ
        transform.rotation = targetRotation;
    }

    IEnumerator RotateRepeatedly(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            yield return StartCoroutine(Rotate(rotationDuration));
            
        }
    }
}
