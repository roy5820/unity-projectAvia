using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnEnemy : EnemySkill
{
    public GameObject enemyPrefeb;//������ ���� ������Ʈ

    //��ų ���� �κ�
    public override IEnumerator Skill()
    {
        GameObject enemyPre = Instantiate(enemyPrefeb, creationLocation.position, Quaternion.identity);
        yield return base.Skill();
    }
}
