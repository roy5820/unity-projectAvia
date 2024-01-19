using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAttackBase : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject buletPrefeb;
    public Transform firePoint;

    public EnemyMainController mainController;//메인 컨트롤러

    Transform playerT;//플레이어가 저장되는 변수
    Vector2 direction;//플레이어 방향

    public float attackRange = 6.0f;//공격 사거리
    public float attackDelay = 0.5f;//공격 선딜
    public float attackCoolTime = 2.0f;//공격 쿨타임
    public float fireForce = 30f;//발사 파워
    private bool isRedy = true;//공격 준비 상태

    private int getBehavioralStatus = 0;//적 오브젝트 행동 상태값

    public LayerMask wallLayer;//

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.FindWithTag("Player").transform;//플레이어 오브젝트 찾기
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus;//플레이어 상태값 가져오기

        float targetDistance = Vector2.Distance(playerT.position, transform.position);
        if (targetDistance < attackRange && isRedy && (getBehavioralStatus == 0 || getBehavioralStatus == 1))
        {
            rb.velocity = Vector2.zero;//공격 시 이동 값 0으로 초기화
            direction = playerT.position - transform.position;//플레이어 위치 가져오기
            //Raycast로 장애물 체크
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, wallLayer);
            
            //플레이어가 공격 범위 안에 있을 시 공격
            if(hitInfo.collider == null)
            {
                StartCoroutine(Fire());
            }
        }
    }

    //발사 구현 코루틴
    IEnumerator Fire()
    {
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 2;//공격 중 상태로 전환
        isRedy = false;//공격 가능 상태 비활성화

        yield return new WaitForSeconds(attackDelay);//공격 선딜
        GameObject bulletPre = Instantiate(buletPrefeb);
        bulletPre.transform.position = firePoint.transform.position;//총알생성 위치 설정
        direction = playerT.position - transform.position;//플레이어 위치 가져오기
        bulletPre.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);

        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 0;//기본 상태로 전환

        StartCoroutine(FireCooltime());//공격 쿨타임 구현
    }

    //발사 쿨타임 구현
    IEnumerator FireCooltime() {
        yield return new WaitForSeconds(attackCoolTime);
        isRedy = true;
    }
}
