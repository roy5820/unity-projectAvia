using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSkill : MonoBehaviour
{

    //�� �׼Ǻ� ���°��� ������ ����
    PlayerMainController mainController = null;
    int getPlayerStatus;//�÷��̾� ���°�
    int getDashStatus;//�뽬 ���°�
    int getMoveStatus;//�̵� ���°�
    int getFireStatus;//�Ϲݰ��� ���°�
    int getReloadStatus;//�Ϲݰ��� ������ ���°�
    int getSkillStatus;//��ų ���°�

    private int maxSkillGauge = 20;//�ִ� ��ų ������
    [SerializeField]
    private int nowSkillGauge = 0;//���� ��ų ������
    [SerializeField]
    private int skillCoast = 10;//��ų ��� �� �ڽ�Ʈ
    [SerializeField]
    private float skillRange = 10f;//��ų ��Ÿ�

    [SerializeField]
    private GameObject bulletPrefab;//��ų ��� �� �߻��� �Ѿ� ������
    public LayerMask EnemyLayer;
    // Start is called before the first frame update
    void Awake()
    {
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        LoadStatus();
    }

    // Update is called once per frame
    void Update()
    {
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        LoadStatus();
    }

    //���� ��Ʈ�ѷ����� ���°��� ������ �ʱ�ȭ�ϴ� �Լ�
    public void LoadStatus()
    {
        mainController = PlayerMainController.getInstanc;//���� ��Ʈ�ѷ� ��������
        if (mainController != null)
        {
            getPlayerStatus = mainController.getSetPlayerStatus;//�÷��̾� ���°��� ����
            getMoveStatus = mainController.getSetMoveStatus;//�̵� ���°� �ʱ�ȭ
            getDashStatus = mainController.getSetDashStatus;//�뽬 ���°� �ʱ�ȭ
            getFireStatus = mainController.getSetFireStatus;//�Ϲݰ��� ���°� �ʱ�ȭ
            getReloadStatus = mainController.getSetReloadStatus;//�Ϲݰ��� ������ ���°� �ʱ�ȭ
            getSkillStatus = mainController.getSetSkillStatus;//��ų ���°� �ʱ�ȭ
        }
    }

    //��ų ��� �Է� ó�� �Լ�
    public void OnSkill()
    {

        if(getSkillStatus == 0 && nowSkillGauge >= skillCoast)
        {

            //�� ������Ʈ�� Ž���� �� Ž���� ��������Ʈ�� ������� ���� �߻�
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, skillRange, EnemyLayer);

            StartCoroutine(ShotEnemy(enemiesInRange));//Ž���� ���鿡�� ���� �߻�
        }
    }

    //�Ѿ� �߻� ���� �ڷ�ƾ
    IEnumerator ShotEnemy(Collider2D[] enemiesInRange)
    {
        nowSkillGauge -= skillCoast;//��ų �ڽ�Ʈ ����
        //���°� ����
        mainController.getSetSkillStatus = 1;
        mainController.getSetReloadStatus = 2;
        mainController.getSetFireStatus = 2;

        //
        foreach (Collider2D enemy in enemiesInRange)
        {
            Transform target = enemy.transform;//Ÿ���� Ʈ������ ����
            Vector2 direction = (target.position - transform.position).normalized;//�Ѿ� �߻� ���ⱸ�ϱ�

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);//������ ����
            bullet.GetComponent<BulletBase>().bulletVec = direction;//�Ѿ� �߻� ���� ����
            bullet.GetComponent<BulletBase>().isLaunch = true;//�Ѿ� �߻�

            yield return new WaitForSeconds(0.1f);
        }

        //���°� ����
        mainController.getSetSkillStatus = 0;
        mainController.getSetReloadStatus = 0;
        mainController.getSetFireStatus = 0;

    }
    void OnDrawGizmos() // ���� �׸���
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }
}
