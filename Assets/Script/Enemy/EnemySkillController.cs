using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillController : MonoBehaviour
{
    //스킬정보를 갖는 객체
    [System.Serializable]
    public class SkillData
    {
        public EnemySkill skill;//사용할 스킬
        public float skillRange;//스킬 사거리
    }
    public List<SkillData> enemySkills;
    public Transform AttackPoint;

    private void Update()
    {
        // 스킬 사용 로직
        foreach (SkillData SkillData in enemySkills)
        {

        }
    }
}