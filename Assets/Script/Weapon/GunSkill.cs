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

    [SerializeField]
    private int maxSkillGauge = 20;//�ִ� ��ų ������
    [SerializeField]
    private int nowSkillGauge = 0;//���� ��ų ������
    [SerializeField]
    private int skillCoast = 10;//��ų ��� �� �ڽ�Ʈ
    [SerializeField]
    private float skillHorizontalRange = 16f;//��ų ���� ��Ÿ�
    [SerializeField]
    private float skillVerticalRange = 10f;//��ų ���� ��Ÿ�
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
        mainController = PlayerMainController.getInstanc;//�÷��̾� ��Ʈ�ѷ��� �ʱ�ȭ
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    //��ų ��� �Է� ó�� �Լ�
    public void OnSkill()
    {

        if(getSkillStatus == 0 && nowSkillGauge >= skillCoast)
        {

            //�� ������Ʈ�� Ž���� �� Ž���� ��������Ʈ�� ������� ���� �߻�
            List<Collider2D> enemiesInRange = new List<Collider2D>(Physics2D.OverlapBoxAll(transform.position, new Vector2(skillHorizontalRange, skillVerticalRange), 0, EnemyLayer));


            //Ÿ�� ���̿� ���� ������ Ÿ�� ��Ͽ��� ����
            for(int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i] != null)
                {
                    Transform target = enemiesInRange[i].transform;//Ÿ���� Ʈ������ ����
                    Vector2 direction = (target.position - transform.position).normalized;//�Ѿ� �߻� ���ⱸ�ϱ�
                    float Dist = Vector2.Distance(target.position, this.transform.position);//���� �Ÿ� ���
                    RaycastHit2D rayTarget = Physics2D.Raycast(transform.position, direction, Dist, LayerMask.GetMask("Wall"));

                    //�����ɽ�Ʈ�� �÷��̾�� Ÿ�� ���̿� ���� �ִ��� üũ
                    if(rayTarget)
                    {
                        if (rayTarget.transform.gameObject.layer != LayerMask.NameToLayer("Wall"))
                            enemiesInRange.RemoveAt(i);//���� ������ ����Ʈ���� �ش� Ÿ�� ����
                    }                }
            }

            StartCoroutine(ShotEnemy(enemiesInRange));//Ž���� ���鿡�� ���� �߻�
        }
    }

    //��ų ���� �ڷ�ƾ
    IEnumerator ShotEnemy(List<Collider2D> enemiesInRange)
    {
        nowSkillGauge -= skillCoast;//��ų �ڽ�Ʈ ����
        float takenTime = 0;//���� �ҿ��� �ð�

        //���°� ����
        mainController.OnSetStatus(0, 0, 0, 1, 1, 2);

        //Ÿ�� ���� �����̸� �־� ����
        foreach (Collider2D enemy in enemiesInRange)
        {
            
            if (enemy != null)
            {
                enemy.gameObject.SendMessage("HitFuntion");
                yield return new WaitForSeconds(shotDelay);//���� �߻� ���� ������
            }
        }

        //��ų ������ ����
        if (skillDelay > takenTime)
        {
            yield return new WaitForSeconds(skillDelay - shotDelay);
        }

        //���°� ����
        mainController.OnSetStatus(0, 0, 0, 0, 0, 0);
    }

    void OnDrawGizmos() // ���� �׸���
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(skillHorizontalRange, skillVerticalRange));
    }

    //���� ��ų ������ ������ �������� �Լ�
    public int GetNowSkillGauge()
    {
        return nowSkillGauge;
    }

    //���� ��ų ������ ���� �����ϴ� �Լ�
    public void SetNowSkillGauge(int value)
    {
        nowSkillGauge += value;
    }

}
