using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGauge : MonoBehaviour
{
    Image gaugeImg = null;//게이지 이미지
    WeaponStatus status = null;//무기 상태
    float maxBullet = 0;//총알 최대 갯수
    float nowBullet = 0;//총알 현재 갯수

    private void Start()
    {
        gaugeImg = GetComponent<Image>();//게이지 이미지 가져오기
        status = PlayerMainController.getInstanc.weapon.GetComponent<WeaponStatus>();//무기 설정값을 가진 인터페이스 가져오기
        maxBullet = status.maxBullet;//최대 총알 개수 갱신
    }

    // Update is called once per frame
    void Update()
    {
        nowBullet = status.nowBullet;//현재 총알 개수 갱신

        gaugeImg.fillAmount = nowBullet / maxBullet;//현제 총알 량에 따른 게이지 변화
    }
}
