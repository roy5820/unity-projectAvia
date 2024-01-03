using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSkill : MonoBehaviour
{

    //각 액션별 상태값을 저장할 변수
    PlayerMainController mainController = null;
    int getPlayerStatus;//플레이어 상태값
    int getDashStatus;//대쉬 상태값
    int getMoveStatus;//이동 상태값
    int getFireStatus;//일반공격 상태값
    int getReloadStatus;//일반공격 재장전 상태값
    int getSkillStatus;//스킬 상태값

    private int maxSkillGauge = 20;//최대 스킬 게이지
    [SerializeField]
    private int nowSkillGauge = 0;//현재 스킬 게이지
    [SerializeField]
    private int skillCoast = 10;//스킬 상용 시 코스트
    [SerializeField]
    private float skillRange = 10f;//스킬 사거리

    [SerializeField]
    private GameObject bulletPrefab;//스킬 사용 시 발사할 총알 프리펩
    public LayerMask EnemyLayer;
    // Start is called before the first frame update
    void Awake()
    {
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        LoadStatus();
    }

    // Update is called once per frame
    void Update()
    {
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        LoadStatus();
    }

    //메인 컨트롤러에서 상태값을 가져와 초기화하는 함수
    public void LoadStatus()
    {
        mainController = PlayerMainController.getInstanc;//메인 컨트롤러 가져오기
        if (mainController != null)
        {
            getPlayerStatus = mainController.getSetPlayerStatus;//플레이어 상태값을 메인
            getMoveStatus = mainController.getSetMoveStatus;//이동 상태값 초기화
            getDashStatus = mainController.getSetDashStatus;//대쉬 상태값 초기화
            getFireStatus = mainController.getSetFireStatus;//일반공격 상태값 초기화
            getReloadStatus = mainController.getSetReloadStatus;//일반공격 재장전 상태값 초기화
            getSkillStatus = mainController.getSetSkillStatus;//스킬 상태값 초기화
        }
    }

    //스킬 사용 입력 처리 함수
    public void OnSkill()
    {

        if(getSkillStatus == 0 && nowSkillGauge >= skillCoast)
        {

            //적 오브젝트를 탐색한 뒤 탐색한 적오브젝트를 순서대로 공격 발사
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, skillRange, EnemyLayer);

            StartCoroutine(ShotEnemy(enemiesInRange));//탐지한 적들에게 총할 발사
        }
    }

    //총알 발사 구현 코루틴
    IEnumerator ShotEnemy(Collider2D[] enemiesInRange)
    {
        nowSkillGauge -= skillCoast;//스킬 코스트 감소
        //상태값 변경
        mainController.getSetSkillStatus = 1;
        mainController.getSetReloadStatus = 2;
        mainController.getSetFireStatus = 2;

        //
        foreach (Collider2D enemy in enemiesInRange)
        {
            Transform target = enemy.transform;//타겟의 트렛스폼 저장
            Vector2 direction = (target.position - transform.position).normalized;//총알 발사 방향구하기

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);//프리펩 생성
            bullet.GetComponent<BulletBase>().bulletVec = direction;//총알 발사 방향 설정
            bullet.GetComponent<BulletBase>().isLaunch = true;//총알 발사

            yield return new WaitForSeconds(0.1f);
        }

        //상태값 변경
        mainController.getSetSkillStatus = 0;
        mainController.getSetReloadStatus = 0;
        mainController.getSetFireStatus = 0;

    }
    void OnDrawGizmos() // 범위 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }
}
