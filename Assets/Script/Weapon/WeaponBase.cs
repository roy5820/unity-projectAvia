using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour
{
    //�� �׼Ǻ� ���°��� ������ ����
    PlayerMainController mainController = null;
    int getPlayerStatus;//�÷��̾� ���°�
    int getDashStatus;//�뽬 ���°�
    int getMoveStatus;//�̵� ���°�
    int getFireStatus;//�Ϲݰ��� ���°�
    int getReloadStatus;//�Ϲݰ��� ������ ���°�
    int getSkillStatus;//��ų ���°�

    public GameObject bulletPrefab;//�Ѿ� ������
    public Transform firePoint;//�Ѿ� �߻���ġ
    public int maxBulletCnt = 5; //�ִ� ��ź��
    public int nowBulletCnt;//���� ��ź��
    public float fireCoolTime = 0.3f; //�߻� ��Ÿ��
    public float reLoardTime = 2.0f;//������ �ð�

    Vector2 bulletVec;//�;� �߻� ����

    private void Awake()
    {
        nowBulletCnt = maxBulletCnt;//���� �Ѿ� ��� �ʱ�ȭ

        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        LoadStatus();
    }

    private void Update()
    {
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        LoadStatus();

        //���콺 ���⿡ ���� ���� ȸ��
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        //�Ѿ� �߻���� ����
        bulletVec = direction.normalized;
    }

    //���� �߻� ����
    public void OnFire()
    {
        if(getFireStatus == 0 && nowBulletCnt > 0)
        {
            mainController.getSetFireStatus = 1;//�Ϲݰ��� ���� �� ���·� ����
            //������ �� �Ϲ� ���� �� ������ ���
            if (getReloadStatus == 1)
                mainController.getSetReloadStatus = 0;

            nowBulletCnt--;//���� ��ź ���� ����
            StartCoroutine(FireDelayIementation());//������ ���� �ڷ�ƾ ȣ��

            //�Ѿ� ����
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            GameObject bulletPre = Instantiate(bulletPrefab);
            bulletPre.transform.position = firePoint.transform.position;//�Ѿ˻��� ��ġ ����
            bulletPre.GetComponent<BulletBase>().bulletVec = bulletVec;
            bulletPre.GetComponent<BulletBase>().isLaunch = true;
        }
    }

    //�÷��̾� �Ϲݰ��� ������ ����
    IEnumerator FireDelayIementation()
    {
        yield return new WaitForSeconds(fireCoolTime);

        if (getFireStatus == 1)
            mainController.getSetFireStatus = 0;
    }

    //�Ϲݰ��� ������ �Է� �� ó�� �Լ�
    void OnReload()
    {
        if(getReloadStatus == 0 && nowBulletCnt < maxBulletCnt)
        {
            StartCoroutine(ReloardImplementation());
        }
    }

    //������ ���� �ڷ�ƾ
    IEnumerator ReloardImplementation()
    {
        float cntTime = 0f;//�ð� ������� ���� ����
        //������ �� �׼� ���� ���°� ����
        mainController.getSetReloadStatus = 1;

        //������ �ð� ���� ������ ����� �ൿ�� �� ��� ������ ���
        while (cntTime < reLoardTime)
        {
            cntTime += Time.deltaTime;//�ð� ī��Ʈ
            LoadStatus();//���°� ���� ��Ʈ�ѷ��� �ִ� ������ �ʱ�ȭ

            //���� ��� �� �ڷ�ƾ ���� ����
            if (getReloadStatus != 1)
            {
                yield break;
            }

            yield return null;
        }
        mainController.getSetReloadStatus = 0;
        nowBulletCnt = maxBulletCnt;//����
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
}
