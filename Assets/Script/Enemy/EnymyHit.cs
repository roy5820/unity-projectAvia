using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnymyHit : MonoBehaviour
{
    public EnemyMainController mainController;//���� ��Ʈ�ѷ�
    int enemyStatus = 0;//Enemy ���� ��

    public void HitFuntion()
    {
        enemyStatus = mainController.GetComponent<EnemyMainController>().EnemyStatus;//Enemy ���°� ��������
        
        if (enemyStatus == 0)
        {
            GameObject palyerweapon = PlayerMainController.getInstanc.weapon;
            if (palyerweapon != null)
                palyerweapon.SendMessage("SetNowSkillGauge", 1);
            Destroy(this.gameObject);
        }
    }
}
