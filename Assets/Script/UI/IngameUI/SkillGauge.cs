using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGauge : MonoBehaviour
{
    SkillStatus status = null;//��ų ����

    int maxSkillGauge = 0;
    int nowSkillGauge = 0;
    public int skillCost = 6;//��ų �ڽ�Ʈ

    public Image[] gaugeImgs = null;//��ų �ݿ� ������ �̹�����
    public Sprite offGaugeImgL = null;//������ �� ������ ���� �̹���
    public Sprite offGaugeImgM = null;//������ �� ������ ��� �̹���
    public Sprite offGaugeImgR = null;//������ �� ������ ������ �̹���
    public Sprite onGaugeImgL = null;//������ �� ������ ���� �̹���
    public Sprite onGaugeImgM = null;//������ �� ������ ��� �̹���
    public Sprite onGaugeImgR = null;//������ �� ������ ������ �̹���

    

    private void Start()
    {
        status = PlayerMainController.getInstanc.weapon.GetComponent<SkillStatus>();//��ų �������� ���� �������̽� ��������
        maxSkillGauge = status.maxGauge;//��ų �ִ� ������ �� ����
    }

    private void Update()
    {
        nowSkillGauge = status.nowGauge;//��ų ���� ������ �� ����

        for(int i = 0; i < gaugeImgs.Length; i++)
        {
            if (i+1 <= nowSkillGauge)
            {
                if ((i + 1) % skillCost == 0)
                    gaugeImgs[i].sprite = onGaugeImgR;
                else if((i + 1) % skillCost == 1)
                    gaugeImgs[i].sprite = onGaugeImgL;
                else
                    gaugeImgs[i].sprite = onGaugeImgM;
            }
            else
            {
                if ((i + 1) % skillCost == 0)
                    gaugeImgs[i].sprite = offGaugeImgR;
                else if ((i + 1) % skillCost == 1)
                    gaugeImgs[i].sprite = offGaugeImgL;
                else
                    gaugeImgs[i].sprite = offGaugeImgM;
            }
        }
    }
}
