using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill: MonoBehaviour
{
    public float cooldown;//쿨타임
    public float castTime; // 선딜레이
    public float afterCastTime; // 후딜레이
    public float lastUsedTime;//마지막 사용 시간

    private Coroutine skillCoroutine = null;//스킬 사용시 코루틴

    //스킬 사용 여부체크
    public virtual bool CanUse()
    {
        return (Time.time - lastUsedTime >= cooldown) && skillCoroutine == null;//스킬 쿨타임과 스킬 사용중인지 여부에 따라 
    }

    //스킬 사용 부분 creationLocation: 스킬 생성 방향, attackVec: 스킬 방향
    public virtual void Use(Transform creationLocation, Vector2 attackVec = new Vector2())
    {
        skillCoroutine = StartCoroutine(PerformSkill(creationLocation, attackVec));
    }

    //선딜 후딜레이 구현 부분
    private IEnumerator PerformSkill(Transform creationLocation, Vector2 attackVec)
    {
        if (castTime > 0f)
        {
            yield return new WaitForSeconds(castTime); // 선딜레이 후에 스킬 수행
        }

        lastUsedTime = Time.time;//스킬 마지막 사용 시간 갱신

        Skill(creationLocation, attackVec);//스킬 사용

        if (afterCastTime > 0f)
        {
            yield return new WaitForSeconds(afterCastTime); // 후딜레이 후에 스킬 종료
        }

        skillCoroutine = null; // 코루틴 종료 후 초기화
    }

    //스킬 구현 부분
    public abstract void Skill(Transform creationLocation, Vector2 attackVec);
}
