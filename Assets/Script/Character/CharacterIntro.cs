using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIntro : MonoBehaviour
{
    CharacterAnimationManager aniManager;
    public int setAniNum = 0;//������ �ִϸ��̼�

    public float introTime = 1f;//��

    private void Awake()
    {
        aniManager = GetComponent<CharacterAnimationManager>();//�ִϸ��̼� �޴��� ��������
    }

    // Start is called before the first frame update
    void Start()
    {
        aniManager.SetAniParameter(setAniNum);//��Ʈ�� �ִϸ��̼����� ����
        GetComponent<EnemyStatusInterface>().BehavioralStatus = 2;//�ൿ �Ұ� ���·� ����
        Invoke("OffIntro", introTime);//��Ʈ�� �ִϸ��̼��� �����ð��� ��
    }

    //��Ʈ�� �ִϸ��̼��� ���� �Լ�
    private void OffIntro()
    {
        aniManager.SetAniParameter(0);
        GetComponent<EnemyStatusInterface>().BehavioralStatus = 0;//�ൿ ���� ���·� ����
    }

}
