using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAttackBase : MonoBehaviour
{
    public GameObject buletPrefeb;
    public Transform firePoint;

    EnemyControllerBase MainController;//���� ��Ʈ�ѷ�
    bool thisFireAvailability;//���� �߻� ����

    Transform playerT;//�÷��̾ ����Ǵ� ����
    Vector2 direction;//�÷��̾� ����

    public float attackRange = 6.0f;//���� ��Ÿ�
    public float attackDelay = 0.8f;//���� ����
    public float attackCoolTime = 1.0f;//���� ��Ÿ��

    // Start is called before the first frame update
    void Start()
    {
        MainController = this.GetComponent<EnemyControllerBase>();//���� ��Ʈ�ѷ� ����
        playerT = GameObject.FindWithTag("Player").transform;//�÷��̾� ������Ʈ ã��
    }

    // Update is called once per frame
    void Update()
    {
        thisFireAvailability = MainController.GetComponent<EnemyControllerBase>().isFireAvailability;//���� �߻� ���� ���� ��������

        //�÷��̾� ����
        direction = playerT.position - transform.position;

        if (thisFireAvailability)
        {
            //Raycast�� ��ֹ� üũ
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, attackRange, LayerMask.GetMask("Player"));
            
            //�÷��̾ ���� ���� �ȿ� ���� �� ����
            if(hitInfo.collider != null)
            {
                StartCoroutine(Fire());
            }
        }
    }

    //�߻� ���� �ڷ�ƾ
    IEnumerator Fire()
    {
        MainController.GetComponent<EnemyControllerBase>().isFireAvailability = false;//�߻� ���ɻ��� ��Ȱ��ȭ
        MainController.GetComponent<EnemyControllerBase>().isMoveAvailability = false;//�̵� ���ɻ��� ��Ȱ��ȭ
        
        yield return new WaitForSeconds(attackDelay);//���� ����
        GameObject buletPre = Instantiate(buletPrefeb);
        buletPre.transform.position = firePoint.transform.position;//�Ѿ˻��� ��ġ ����
        buletPre.GetComponent<BulletBase>().bulletVec = direction;
        buletPre.GetComponent<BulletBase>().isLaunch = true;

        MainController.GetComponent<EnemyControllerBase>().isMoveAvailability = true;//�̵� ���ɻ��� Ȱ��ȭ
        StartCoroutine(FireCooltime());//���� ��Ÿ�� ����
    }

    //�߻� ��Ÿ�� ����
    IEnumerator FireCooltime() {
        yield return new WaitForSeconds(attackCoolTime);
        MainController.GetComponent<EnemyControllerBase>().isFireAvailability = true;
    }
}
