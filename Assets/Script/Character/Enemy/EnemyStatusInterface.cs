using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyStatusInterface
{
    int EnemyStatus { get; set; }//Enemy 상태값 프로퍼티
    int BehavioralStatus { get; set; }//Enemy 행동값 프로퍼티
}
