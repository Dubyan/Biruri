using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // персонаж, за которым следит камера
    public float smoothSpeed = 0.125f; // плавность следования камеры

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + new Vector3(0, 0, -10); // задаем позицию камеры с учетом смещения по z
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // плавно перемещаем камеру к желаемой позиции
            transform.position = smoothedPosition;
        }
    }
}