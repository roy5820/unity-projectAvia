using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour
{
    public GameObject bulletPrefab;//�Ѿ� ������
    public Transform firePoint;//�Ѿ� �߻���ġ
    public int maxBulletCnt = 5; //�ִ� ��ź��
    public int nowBulletCnt;//���� ��ź��

    Vector2 bulletVec;

    private void Start()
    {
        nowBulletCnt = maxBulletCnt;
    }

    private void Update()
    {
        //���콺 ���⿡ ���� ���� ȸ��
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        //�Ѿ� �߻���� ����
        bulletVec = direction.normalized;
    }

    //���� �߻� ����
    public void Fire()
    {
        //nowBulletCnt--;
        //�Ѿ� ����
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        GameObject bulletPre = Instantiate(bulletPrefab);
        bulletPre.transform.position = firePoint.transform.position;//�Ѿ˻��� ��ġ ����
        bulletPre.GetComponent<BulletBase>().bulletVec = bulletVec;
        bulletPre.GetComponent<BulletBase>().isLaunch = true;
    }

    //���� ��
}
