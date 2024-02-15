using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScore : MonoBehaviour
{
    PlayerMainController playerStatus = null;//�÷��̾� ���°��� ������ ��ü
    int nowPlayerLife = 0;

    public Image[] lifeScoreImgs = null;//���� ��� ǥ���� �̹��� ������Ʈ��

    public Sprite offLifeImg = null;
    public Sprite onLifeImg = null;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = PlayerMainController.getInstanc;//�÷��̾� �ν��Ͻ� ��������
        
    }

    // Update is called once per frame
    void Update()
    {
        nowPlayerLife = playerStatus.playerLife;//���� �÷��̾� ���� �ʱ�ȭ

        //���� ����� ���� �̹��� ����
        for(int i = 0; i < lifeScoreImgs.Length; i++)
        {
            if (i + 1 <= nowPlayerLife)
                lifeScoreImgs[i].sprite = onLifeImg;
            else
                lifeScoreImgs[i].sprite = offLifeImg;
                
        }
    }
}
