using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillController : MonoBehaviour
{
    //��ų������ ���� ��ü
    [System.Serializable]
    public class SkillData
    {
        public EnemySkill skill;//����� ��ų
        public float skillRange;//��ų ��Ÿ�
    }
    public List<SkillData> enemySkills;
    public Transform AttackPoint;

    private void Update()
    {
        // ��ų ��� ����
        foreach (SkillData SkillData in enemySkills)
        {

        }
    }
}