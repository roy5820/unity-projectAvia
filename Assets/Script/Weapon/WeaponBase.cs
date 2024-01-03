using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour
{
    //각 액션별 상태값을 저장할 변수
    PlayerMainController mainController = null;
    int getPlayerStatus;//플레이어 상태값
    int getDashStatus;//대쉬 상태값
    int getMoveStatus;//이동 상태값
    int getFireStatus;//일반공격 상태값
    int getReloadStatus;//일반공격 재장전 상태값
    int getSkillStatus;//스킬 상태값

    public GameObject bulletPrefab;//총알 프리펩
    public Transform firePoint;//총알 발사위치
    public int maxBulletCnt = 5; //최대 장탄수
    public int nowBulletCnt;//현제 장탄수
    public float fireCoolTime = 0.3f; //발사 쿨타임
    public float reLoardTime = 2.0f;//재장전 시간

    Vector2 bulletVec;//촐알 발사 방향

    private void Awake()
    {
        nowBulletCnt = maxBulletCnt;//현제 총알 계수 초기화

        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        LoadStatus();
    }

    private void Update()
    {
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        LoadStatus();

        //마우스 방향에 따른 무기 회전
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        //총알 발사방향 설정
        bulletVec = direction.normalized;
    }

    //무기 발사 구현
    public void OnFire()
    {
        if(getFireStatus == 0 && nowBulletCnt > 0)
        {
            mainController.getSetFireStatus = 1;//일반공격 공격 중 상태로 변경
            //재장전 중 일반 공격 시 재장전 취소
            if (getReloadStatus == 1)
                mainController.getSetReloadStatus = 0;

            nowBulletCnt--;//현제 장탄 개수 감소
            StartCoroutine(FireDelayIementation());//딜레이 구현 코루틴 호출

            //총알 생성
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            GameObject bulletPre = Instantiate(bulletPrefab);
            bulletPre.transform.position = firePoint.transform.position;//총알생성 위치 설정
            bulletPre.GetComponent<BulletBase>().bulletVec = bulletVec;
            bulletPre.GetComponent<BulletBase>().isLaunch = true;
        }
    }

    //플레이어 일반공격 딜레이 구현
    IEnumerator FireDelayIementation()
    {
        yield return new WaitForSeconds(fireCoolTime);

        if (getFireStatus == 1)
            mainController.getSetFireStatus = 0;
    }

    //일반공격 재장전 입력 시 처리 함수
    void OnReload()
    {
        if(getReloadStatus == 0 && nowBulletCnt < maxBulletCnt)
        {
            StartCoroutine(ReloardImplementation());
        }
    }

    //재장전 구현 코루틴
    IEnumerator ReloardImplementation()
    {
        float cntTime = 0f;//시간 경과값을 담을 변수
        //재장전 시 액션 재한 상태값 설정
        mainController.getSetReloadStatus = 1;

        //재장전 시간 동안 장전을 취소할 행동을 할 경우 재장전 취소
        while (cntTime < reLoardTime)
        {
            cntTime += Time.deltaTime;//시간 카운트
            LoadStatus();//상태값 메인 컨트롤러에 있는 값으로 초기화

            //장전 취소 시 코루틴 강제 종료
            if (getReloadStatus != 1)
            {
                yield break;
            }

            yield return null;
        }
        mainController.getSetReloadStatus = 0;
        nowBulletCnt = maxBulletCnt;//장전
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
}
