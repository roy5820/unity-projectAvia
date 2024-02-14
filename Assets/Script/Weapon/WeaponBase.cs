using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour, WeaponStatus
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
    public Transform eftPoint;//����Ʈ ���� ��ġ
    public Transform firePoint;//�Ѿ� �߻���ġ
    public int maxBulletCnt = 5; //�ִ� ��ź��
    public int nowBulletCnt;//���� ��ź��
    public float fireCoolTime = 0.3f; //�߻� ��Ÿ��
    public float reLoardTime = 2.0f;//������ �ð�
    public float fireForce = 10f;//�Ѿ� �߻� �ӵ�

    Vector2 bulletVec;//�;� �߻� ����

    public GameObject fireEft = null;//�Ѿ� ����Ʈ
    public GameObject reloardEft = null;//������ �� ����Ʈ

    private void Start()
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

        // �θ� ������Ʈ�� ���ý����� ���� -1�� ��� ������ ����
        if (transform.parent.localScale.x < 0)
            angle += 180;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //rotation = gameObject.transform.localScale.x > 0 ? rotation * Quaternion.Euler(0, 0, 180f) : rotation;
        transform.rotation = rotation;

        //�Ѿ� �߻���� ����
        bulletVec = direction.normalized;
    }

    //���� �߻� ����
    public void OnFire()
    {
        if(getFireStatus == 0 && nowBulletCnt > 0)
        {
            mainController.OnSetStatus(-1, -1, -1, 2, -1, -1);//�Ϲ� ���� ���·� ����
            //���� ���⿡ ���� ĳ���� ���� ��ȯ
            if(transform.parent.TryGetComponent<Transform>(out Transform playerTransform))
            {
                if (bulletVec.x > 0)
                    playerTransform.transform.localScale = new Vector3(1, 1, 1);
                else if (bulletVec.x < 0)
                    playerTransform.transform.localScale = new Vector3(-1, 1, 1);
            }
            
                
            nowBulletCnt--;//���� ��ź ���� ����

            //�Ѿ� ����
            GameObject bulletPre = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);//�Ѿ� ����
            bulletPre.GetComponent<Rigidbody2D>().AddForce(bulletVec * fireForce, ForceMode2D.Impulse);

            //�߻� ����Ʈ ����
            GameObject fireEftPre = Instantiate(fireEft, eftPoint.transform.position, Quaternion.identity);//�߻� ����Ʈ
            fireEftPre.transform.localScale = transform.parent.transform.localScale;
            StartCoroutine(FireDelayIementation());//������ ���� �ڷ�ƾ ȣ��
        }
    }

    //�÷��̾� �Ϲݰ��� ������ ����
    IEnumerator FireDelayIementation()
    {
        yield return new WaitForSeconds(fireCoolTime);

        if (getFireStatus == 2)
            mainController.OnSetStatus(-1, -1, -1, 0, -1, -1);
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
        //������ ����Ʈ ����
        GameObject reloardEftPre = Instantiate(reloardEft, transform.position, Quaternion.identity, this.transform);//�߻� ����Ʈ

        float cntTime = 0f;//�ð� ������� ���� ����
        //������ �� �׼� ���� ���°� ����
        mainController.OnSetStatus(-1, -1, -1, -1, 2, -1);

        //������ �ð� ���� ������ ����� �ൿ�� �� ��� ������ ���
        while (cntTime < reLoardTime)
        {
            cntTime += Time.deltaTime;//�ð� ī��Ʈ

            //���� ��� �� �ڷ�ƾ ���� ����
            if (mainController.getSetReloadStatus != 2)
            {
                yield break;
            }

            yield return null;
        }

        mainController.OnSetStatus(-1, -1, -1, -1, 0, -1);//������ ���� ���°� ����
        nowBulletCnt = maxBulletCnt;//����
    }

    //���� ������ �Լ�
    public void OnForcedReload()
    {
        nowBulletCnt = maxBulletCnt;//����
    }

    public int maxBullet
    {
        get
        {
            return maxBulletCnt;
        }
        set
        {
            maxBulletCnt = value;
        }
    }

    public int nowBullet
    {
        get
        {
            return nowBulletCnt;
        }
        set
        {
            nowBulletCnt = value;
        }
    }
}
