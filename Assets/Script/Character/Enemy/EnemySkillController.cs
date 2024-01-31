using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillController : MonoBehaviour
{

    public List<EnemySkill> enemySkills;

    private int getBehavioralStatus = 0;//적 오브젝트 행동 상태값

    private void Update()
    {
        //오브젝트 행동 상태값 가져오기
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus;
        else
            return;

        //정지, 이동 상태에서만 공격 구현
        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            // 스킬 사용 로직
            foreach (EnemySkill SkillData in enemySkills)
            {
                //스킬 사용 여부 체크
                if (SkillData.CanUse())
                {
                    SkillData.Use(this.GetComponent<EnemySkillController>());//스킬 사용
                    break;
                }
            }
        }
    }

    //스테이터스 설정하는 함수
    public void SetStatus(int mode)
    {
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus = mode;
    }
}