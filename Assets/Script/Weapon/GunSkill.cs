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
    private float shotDelay = 0.1f;//��ų ���� �Ѿ� �߻� ����
    [SerializeField]
    private float skillDelay = 0.5f;//��ų �ּ� ��� �ð�
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
        float takenTime = 0;//���� �ҿ��� �ð�

        //���°� ����
        mainController.getSetSkillStatus = 1;
        mainController.getSetReloadStatus = 2;
        mainController.getSetFireStatus = 2;
        
        //Ÿ�� ���� �����̸� �־� ����
        foreach (Collider2D enemy in enemiesInRange)
        {
            
            if (enemy != null)
            {
                Transform target = enemy.transform;//Ÿ���� Ʈ������ ����
                Vector2 direction = (target.position - transform.position).normalized;//�Ѿ� �߻� ���ⱸ�ϱ�

                int _layerMask = 1 << LayerMask.NameToLayer("Player");
                _layerMask = ~_layerMask;
                RaycastHit2D rayTarget = Physics2D.Raycast(transform.position, direction, skillRange, _layerMask);

                //�����ɽ�Ʈ�� �÷��̾�� Ÿ�� ���̿� ���� �ִ��� üũ
                if(rayTarget.transform.gameObject == target.gameObject)
                {
                    target.gameObject.SendMessage("HitFuntion");
                    yield return new WaitForSeconds(shotDelay);//���� �߻� ���� ������
                }
                
            }
        }

        //��ų ������ ����
        if (skillDelay > takenTime)
        {
            yield return new WaitForSeconds(skillDelay - shotDelay);
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
