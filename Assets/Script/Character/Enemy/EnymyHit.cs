using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnymyHit : MonoBehaviour
{
    public EnemyMainController mainController;//메인 컨트롤러
    int enemyStatus = 0;//Enemy 상태 값

    //피격 시 호출 되는 함수 attacktype 0: 일반 공격, 1: 스킬, 2: 일반무적 무시


    public void HitFuntion(int attackType = 0)
    {
        enemyStatus = mainController.GetComponent<EnemyMainController>().EnemyStatus;//Enemy 상태값 가져오기
        
        if (enemyStatus == 0)
        {
            GameObject palyerweapon = PlayerMainController.getInstanc.weapon;
            if (palyerweapon != null)
                palyerweapon.SendMessage("SetNowSkillGauge", 1);
            Destroy(this.gameObject);
        }
    }
}
