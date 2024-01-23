using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillController : MonoBehaviour
{

    public List<EnemySkill> enemySkills;
    public Transform AttackPoint;//���� ���� ��ġ
    public EnemyMainController mainController;//���� ��Ʈ�ѷ�

    private int getBehavioralStatus = 0;//�� ������Ʈ �ൿ ���°�

    private void Update()
    {
        getBehavioralStatus = mainController.BehavioralStatus;//������Ʈ �ൿ ���°� ��������

        //����, �̵� ���¿����� ���� ����
        if(getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            // ��ų ��� ����
            foreach (EnemySkill SkillData in enemySkills)
            {
                //��ų ��� ���� üũ
                if (SkillData.CanUse())
                {
                    SkillData.Use(AttackPoint, this.GetComponent<EnemySkillController>());
                    break;
                }
            }
        }
    }

    //�������ͽ� �����ϴ� �Լ�
    public void SetStatus(int mode)
    {
        mainController.BehavioralStatus = mode;
    }
}