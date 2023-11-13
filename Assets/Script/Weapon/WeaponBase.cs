using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour
{
    public GameObject bulletPrefab;//총알 프리펩
    public Transform firePoint;//총알 발사위치
    public int maxBulletCnt = 5; //최대 장탄수
    public int nowBulletCnt;//현제 장탄수

    Vector2 bulletVec;

    private void Start()
    {
        nowBulletCnt = maxBulletCnt;
    }

    private void Update()
    {
        //마우스 방향에 따른 무기 회전
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        //총알 발사방향 설정
        bulletVec = direction.normalized;
    }

    //무기 발사 구현
    public void Fire()
    {
        //nowBulletCnt--;
        //총알 생성
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        GameObject bulletPre = Instantiate(bulletPrefab);
        bulletPre.transform.position = firePoint.transform.position;//총알생성 위치 설정
        bulletPre.GetComponent<BulletBase>().bulletVec = bulletVec;
        bulletPre.GetComponent<BulletBase>().isLaunch = true;
    }

    //장전 시
}
