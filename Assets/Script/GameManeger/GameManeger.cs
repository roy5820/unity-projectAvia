using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    public static GameManeger instance = null;//게임 매니저 인스턴스화를 위한 변수 선언

    private int killScore = 0;//킬 스코어

    public GameObject deadPoup;

    private void Awake()
    {
        //인스턴스 값 초기화
        if (instance == null)
        {
            instance = this;
        }

        else
            Destroy(this.gameObject);
    }

    //킬스코어 프로퍼티
    public int KillScore
    {
        get
        {
            return killScore;
        }
        set
        {
            
            killScore = value;
        }
    }

    public void PlayerDead()
    {
        deadPoup.SetActive(true);
    }
}
