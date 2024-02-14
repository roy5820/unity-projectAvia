using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 부모 오브젝트의 로컬스케일 값이 -1인 경우 각도를 반전
        if (transform.parent.localScale.x < 0)
            angle += 180;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
