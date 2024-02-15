using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    public static GameManeger instance = null;//���� �Ŵ��� �ν��Ͻ�ȭ�� ���� ���� ����

    private int killScore = 0;//ų ���ھ�

    private void Awake()
    {
        //�ν��Ͻ� �� �ʱ�ȭ
        if (instance == null)
        {
            instance = this;
        }

        else
            Destroy(this.gameObject);
    }

    //ų���ھ� ������Ƽ
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

    public void ReloardScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
