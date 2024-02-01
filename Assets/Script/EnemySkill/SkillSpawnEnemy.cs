using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnEnemy : EnemySkill
{
    public GameObject enemyPrefeb;//생성할 몬스터 오브젝트

    //스킬 구현 부분
    public override IEnumerator Skill()
    {
        GameObject enemyPre = Instantiate(enemyPrefeb, creationLocation.position, Quaternion.identity);
        yield return base.Skill();
    }
}
