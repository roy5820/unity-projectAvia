using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameCameraMove : MonoBehaviour
{
    public Transform target; // 플레이어의 Transform
    public Vector2 minPosition; // 카메라가 이동할 수 있는 최소 좌표
    public Vector2 maxPosition; // 카메라가 이동할 수 있는 최대 좌표


    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // 카메라가 지정된 범위 내에서만 움직이도록 함
            targetPosition.x = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);

            transform.position = targetPosition;
        }
    }
}
