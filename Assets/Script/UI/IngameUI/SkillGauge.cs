using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGauge : MonoBehaviour
{
    SkillStatus status = null;//스킬 상태

    int maxSkillGauge = 0;
    int nowSkillGauge = 0;
    public int skillCost = 6;//스킬 코스트

    public Image[] gaugeImgs = null;//스킬 반영 게이지 이미지들
    public Sprite offGaugeImgL = null;//꺼졌을 때 게이지 왼쪽 이미지
    public Sprite offGaugeImgM = null;//꺼졌을 때 게이지 가운데 이미지
    public Sprite offGaugeImgR = null;//꺼졌을 때 게이지 오른쪽 이미지
    public Sprite onGaugeImgL = null;//켜졌을 때 게이지 왼쪽 이미지
    public Sprite onGaugeImgM = null;//켜졌을 때 게이지 가운데 이미지
    public Sprite onGaugeImgR = null;//켜졌을 때 게이지 오른쪽 이미지

    

    private void Start()
    {
        status = PlayerMainController.getInstanc.weapon.GetComponent<SkillStatus>();//스킬 설정값을 가진 인터페이스 가져오기
        maxSkillGauge = status.maxGauge;//스킬 최대 게이지 값 설정
    }

    private void Update()
    {
        nowSkillGauge = status.nowGauge;//스킬 현재 게이지 값 설정

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
