using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameCameraMove : MonoBehaviour
{
    public Transform target; // �÷��̾��� Transform
    public Vector2 minPosition; // ī�޶� �̵��� �� �ִ� �ּ� ��ǥ
    public Vector2 maxPosition; // ī�޶� �̵��� �� �ִ� �ִ� ��ǥ


    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // ī�޶� ������ ���� �������� �����̵��� ��
            targetPosition.x = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);

            transform.position = targetPosition;
        }
    }
}
