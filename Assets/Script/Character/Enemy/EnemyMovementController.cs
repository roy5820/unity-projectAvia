using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour//�� ������Ʈ �̵� ���� ������Ʈ
{
    public float speed = 5f;
    private Transform playerP;
    public LayerMask wallLayer;
    public LayerMask playerLayer;
    public float minLimitDistance = 4f;
    public float castDistance = 1f;

    private Rigidbody2D rb;

    private int getBehavioralStatus = 0;//�� ������Ʈ �ൿ ���°�

    float turnMoveTime = 0;//�� Ž���� 

    Vector2 directionMovement;//�̵� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //������Ʈ �ൿ ���°� ��������
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus;
        else
            return;
        
        //�̵� ���� �����̸� Ÿ���� ������ �÷��̾� ����
        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            playerP = TargetTransform;//Ÿ�� ��ġ ��������
            if (playerP != null)
            {
                getBehavioralStatus = enemyStatusInterface.BehavioralStatus = 1;//�ص� ������ 1(�ȱ�)�� ����

                directionMovement = (playerP.position - transform.position);//�÷��̾� ���� ��������
                float playerDistance = directionMovement.magnitude;//�÷��̾���� �Ÿ� ���
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), directionMovement, minLimitDistance, wallLayer);
                RaycastHit2D hitM = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), directionMovement, castDistance, wallLayer);
                RaycastHit2D hitL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.Euler(0, 0, -25) * directionMovement, castDistance, wallLayer);
                RaycastHit2D hitR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.Euler(0, 0, 25) * directionMovement, castDistance, wallLayer);

                //��ο� ���� ���� �� ��ȸ�Ͽ� �÷��̾� ����
                if (hitM.collider != null || hitL.collider != null || hitR.collider != null)
                {
                    //ĳ���� �𼭸� ���� ���� üũ
                    if (hitL.collider != null && hitM.collider == null)
                    {
                        directionMovement = Vector2.Perpendicular(hitL.normal).normalized;
                    }
                    else if (hitR.collider != null && hit.collider == null)
                    {
                        directionMovement = Vector2.Perpendicular(hitR.normal).normalized;
                    }
                    else
                    {
                        directionMovement = Vector2.Perpendicular(hitM.normal).normalized;
                    }
                }
                //�ּ� �̵��Ÿ� üũ
                else if (minLimitDistance > directionMovement.magnitude && hit.collider == null)
                {
                    getBehavioralStatus = 0;
                    directionMovement = Vector2.zero;
                }
            }
            else
            {
                getBehavioralStatus = 0;
                directionMovement = Vector2.zero;
            }

            //�̵� ����
            directionMovement.Normalize();
            rb.velocity = directionMovement * speed;

            //�̵� ��ġ�� ���� ĳ���� ����
            this.gameObject.GetComponent<Transform>().localScale = new Vector2((directionMovement.x > 0 ? 1 : -1), 1);
        }

        //�ȱ�, IDLE ���� �ִϸ��̼� ����

        if(TryGetComponent<CharacterAnimationManager>(out CharacterAnimationManager aniManager))
        {

        }
        if (getBehavioralStatus == 0)
            aniManager.SetAniParameter(0);
        else if (getBehavioralStatus == 1)
            aniManager.SetAniParameter(1);
    }


    //Ÿ�� ������Ʈ�� �������� get ������Ƽ
    public Transform TargetTransform
    {
        get
        {
            GameObject targetObj = GameObject.FindWithTag("Player");//Ÿ�� ������Ʈ ����
            //Ÿ���� ���̾� ���� Player�� �ƴϸ� null�� ����
            if (targetObj == null || (((1 << targetObj.gameObject.layer & playerLayer) == 0)))
                return null;

            return targetObj.transform;
        }
    }
}