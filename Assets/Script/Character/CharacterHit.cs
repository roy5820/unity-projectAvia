using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterHit
{
    //피격시 호출되는 함수 attackType 0: 일반 1: 스킬
    void HitAction(int attackType);
}
    