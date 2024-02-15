using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGauge : MonoBehaviour
{
    Image gaugeImg = null;//������ �̹���
    WeaponStatus status = null;//���� ����
    float maxBullet = 0;//�Ѿ� �ִ� ����
    float nowBullet = 0;//�Ѿ� ���� ����

    private void Start()
    {
        gaugeImg = GetComponent<Image>();//������ �̹��� ��������
        status = PlayerMainController.getInstanc.weapon.GetComponent<WeaponStatus>();//���� �������� ���� �������̽� ��������
        maxBullet = status.maxBullet;//�ִ� �Ѿ� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        nowBullet = status.nowBullet;//���� �Ѿ� ���� ����

        gaugeImg.fillAmount = nowBullet / maxBullet;//���� �Ѿ� ���� ���� ������ ��ȭ
    }
}
