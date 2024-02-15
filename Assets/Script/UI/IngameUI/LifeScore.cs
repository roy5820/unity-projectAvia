using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScore : MonoBehaviour
{
    PlayerMainController playerStatus = null;//플레이어 상태값을 가져올 객체
    int nowPlayerLife = 0;

    public Image[] lifeScoreImgs = null;//현재 목숨 표시할 이미지 오브젝트들

    public Sprite offLifeImg = null;
    public Sprite onLifeImg = null;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = PlayerMainController.getInstanc;//플레이어 인스턴스 가져오기
        
    }

    // Update is called once per frame
    void Update()
    {
        nowPlayerLife = playerStatus.playerLife;//현재 플레이어 생명 초기화

        //현재 목숨에 따른 이미지 변경
        for(int i = 0; i < lifeScoreImgs.Length; i++)
        {
            if (i + 1 <= nowPlayerLife)
                lifeScoreImgs[i].sprite = onLifeImg;
            else
                lifeScoreImgs[i].sprite = offLifeImg;
                
        }
    }
}
