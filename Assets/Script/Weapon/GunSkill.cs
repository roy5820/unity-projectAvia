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

    [SerializeField]
    private int maxSkillGauge = 20;//최대 스킬 게이지
    [SerializeField]
    private int nowSkillGauge = 0;//현재 스킬 게이지
    [SerializeField]
    private int skillCoast = 10;//스킬 상용 시 코스트
    [SerializeField]
    private float skillHorizontalRange = 16f;//스킬 가로 사거리
    [SerializeField]
    private float skillVerticalRange = 10f;//스킬 세로 사거리
    [SerializeField]
    private float shotDelay = 0.1f;//스킬 사용시 총알 발사 간격
    [SerializeField]
    private float skillDelay = 0.5f;//스킬 최소 사용 시간
    [SerializeField]
    private GameObject bulletPrefab;//스킬 사용 시 발사할 총알 프리펩
    public LayerMask EnemyLayer;

    // Start is called before the first frame update
    void Awake()
    {
        mainController = PlayerMainController.getInstanc;//플레이어 컨트롤러값 초기화
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    //스킬 사용 입력 처리 함수
    public void OnSkill()
    {

        if(getSkillStatus == 0 && nowSkillGauge >= skillCoast)
        {

            //적 오브젝트를 탐색한 뒤 탐색한 적오브젝트를 순서대로 공격 발사
            List<Collider2D> enemiesInRange = new List<Collider2D>(Physics2D.OverlapBoxAll(transform.position, new Vector2(skillHorizontalRange, skillVerticalRange), 0, EnemyLayer));


            //타겟 사이에 벽이 있으면 타켓 목록에서 제거
            for(int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i] != null)
                {
                    Transform target = enemiesInRange[i].transform;//타겟의 트렛스폼 저장
                    Vector2 direction = (target.position - transform.position).normalized;//총알 발사 방향구하기
                    float Dist = Vector2.Distance(target.position, this.transform.position);//물제 거리 계산
                    RaycastHit2D rayTarget = Physics2D.Raycast(transform.position, direction, Dist, LayerMask.GetMask("Wall"));

                    //레이케스트로 플레이어와 타겟 사이에 벽이 있는지 체크
                    if(rayTarget)
                    {
                        if (rayTarget.transform.gameObject.layer != LayerMask.NameToLayer("Wall"))
                            enemiesInRange.RemoveAt(i);//벽이 있으면 리스트에서 해당 타겟 제거
                    }                }
            }

            StartCoroutine(ShotEnemy(enemiesInRange));//탐지한 적들에게 총할 발사
        }
    }

    //스킬 구현 코루틴
    IEnumerator ShotEnemy(List<Collider2D> enemiesInRange)
    {
        nowSkillGauge -= skillCoast;//스킬 코스트 감소
        float takenTime = 0;//현제 소요한 시간

        //상태값 변경
        mainController.OnSetStatus(0, 0, 0, 1, 1, 2);

        //타겟 별로 딜레이를 주어 공격
        foreach (Collider2D enemy in enemiesInRange)
        {
            
            if (enemy != null)
            {
                enemy.gameObject.SendMessage("HitFuntion");
                yield return new WaitForSeconds(shotDelay);//다음 발사 까지 딜레이
            }
        }

        //스킬 딜레이 구현
        if (skillDelay > takenTime)
        {
            yield return new WaitForSeconds(skillDelay - shotDelay);
        }

        //상태값 변경
        mainController.OnSetStatus(0, 0, 0, 0, 0, 0);
    }

    void OnDrawGizmos() // 범위 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(skillHorizontalRange, skillVerticalRange));
    }

    //현재 스킬 게이지 정보를 가져오는 함수
    public int GetNowSkillGauge()
    {
        return nowSkillGauge;
    }

    //현재 스킬 게이지 값을 설정하는 함수
    public void SetNowSkillGauge(int value)
    {
        nowSkillGauge += value;
    }

}
