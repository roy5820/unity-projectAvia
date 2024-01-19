using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAttackBase : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject buletPrefeb;
    public Transform firePoint;

    public EnemyMainController mainController;//���� ��Ʈ�ѷ�

    Transform playerT;//�÷��̾ ����Ǵ� ����
    Vector2 direction;//�÷��̾� ����

    public float attackRange = 6.0f;//���� ��Ÿ�
    public float attackDelay = 0.5f;//���� ����
    public float attackCoolTime = 2.0f;//���� ��Ÿ��
    public float fireForce = 30f;//�߻� �Ŀ�
    private bool isRedy = true;//���� �غ� ����

    private int getBehavioralStatus = 0;//�� ������Ʈ �ൿ ���°�

    public LayerMask wallLayer;//

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.FindWithTag("Player").transform;//�÷��̾� ������Ʈ ã��
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus;//�÷��̾� ���°� ��������

        float targetDistance = Vector2.Distance(playerT.position, transform.position);
        if (targetDistance < attackRange && isRedy && (getBehavioralStatus == 0 || getBehavioralStatus == 1))
        {
            rb.velocity = Vector2.zero;//���� �� �̵� �� 0���� �ʱ�ȭ
            direction = playerT.position - transform.position;//�÷��̾� ��ġ ��������
            //Raycast�� ��ֹ� üũ
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, wallLayer);
            
            //�÷��̾ ���� ���� �ȿ� ���� �� ����
            if(hitInfo.collider == null)
            {
                StartCoroutine(Fire());
            }
        }
    }

    //�߻� ���� �ڷ�ƾ
    IEnumerator Fire()
    {
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 2;//���� �� ���·� ��ȯ
        isRedy = false;//���� ���� ���� ��Ȱ��ȭ

        yield return new WaitForSeconds(attackDelay);//���� ����
        GameObject bulletPre = Instantiate(buletPrefeb);
        bulletPre.transform.position = firePoint.transform.position;//�Ѿ˻��� ��ġ ����
        direction = playerT.position - transform.position;//�÷��̾� ��ġ ��������
        bulletPre.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);

        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 0;//�⺻ ���·� ��ȯ

        StartCoroutine(FireCooltime());//���� ��Ÿ�� ����
    }

    //�߻� ��Ÿ�� ����
    IEnumerator FireCooltime() {
        yield return new WaitForSeconds(attackCoolTime);
        isRedy = true;
    }
}
