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
    public float fireForce = 10f;//�Ѿ� �߻� �ӵ�

    Vector2 bulletVec;//�;� �߻� ����

    private void Awake()
    {
        nowBulletCnt = maxBulletCnt;//���� �Ѿ� ��� �ʱ�ȭ
        mainController = PlayerMainController.getInstanc;//�÷��̾� ��Ʈ�ѷ��� �ʱ�ȭ
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    private void Update()
    {
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);

        //���콺 ���⿡ ���� ���� ȸ��
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (new Vector2(transform.position.x, transform.position.y));
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
            mainController.getSetPlayerStatus = 2;//�Ϲ� ���� ���·� ����
            
            nowBulletCnt--;//���� ��ź ���� ����

            //�Ѿ� ����
            GameObject bulletPre = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);//�Ѿ� ����
            bulletPre.GetComponent<Rigidbody2D>().AddForce(bulletVec * fireForce, ForceMode2D.Impulse);

            StartCoroutine(FireDelayIementation());//������ ���� �ڷ�ƾ ȣ��
        }
    }

    //�÷��̾� �Ϲݰ��� ������ ����
    IEnumerator FireDelayIementation()
    {
        yield return new WaitForSeconds(fireCoolTime);
        
        if (getPlayerStatus == 2)
            mainController.getSetPlayerStatus = 0;
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
        mainController.getSetPlayerStatus = 3;

        //������ �ð� ���� ������ ����� �ൿ�� �� ��� ������ ���
        while (cntTime < reLoardTime)
        {
            cntTime += Time.deltaTime;//�ð� ī��Ʈ
            mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);//���°� ���� ��Ʈ�ѷ��� �ִ� ������ �ʱ�ȭ

            //���� ��� �� �ڷ�ƾ ���� ����
            if (getPlayerStatus != 3)
            {
                yield break;
            }

            yield return null;
        }

        mainController.getSetPlayerStatus = 0;//�÷��̾� ���°� �Ϲݻ��·� ����
        nowBulletCnt = maxBulletCnt;//����
    }
}
