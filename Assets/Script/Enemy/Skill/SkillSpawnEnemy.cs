using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnEnemy : EnemySkill
{
    public GameObject enemyPrefeb;//������ ���� ������Ʈ

    //��ų ���� �κ�
    public override void Skill(Transform creationLocation)
    {
        GameObject enemyPre = Instantiate(enemyPrefeb, creationLocation.position, Quaternion.identity);
    }
}
