using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour, WeaponStatus
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
    public Transform eftPoint;//이펙트 생성 위치
    public Transform firePoint;//총알 발사위치
    public int maxBulletCnt = 5; //최대 장탄수
    public int nowBulletCnt;//현제 장탄수
    public float fireCoolTime = 0.3f; //발사 쿨타임
    public float reLoardTime = 2.0f;//재장전 시간
    public float fireForce = 10f;//총알 발사 속도

    Vector2 bulletVec;//촐알 발사 방향

    public GameObject fireEft = null;//총알 이펙트
    public GameObject reloardEft = null;//재장전 시 이펙트

    private void Start()
    {
        nowBulletCnt = maxBulletCnt;//현제 총알 계수 초기화
        mainController = PlayerMainController.getInstanc;//플레이어 컨트롤러값 초기화
        
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    private void Update()
    {
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);

        //마우스 방향에 따른 무기 회전
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (new Vector2(transform.position.x, transform.position.y));
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 부모 오브젝트의 로컬스케일 값이 -1인 경우 각도를 반전
        if (transform.parent.localScale.x < 0)
            angle += 180;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //rotation = gameObject.transform.localScale.x > 0 ? rotation * Quaternion.Euler(0, 0, 180f) : rotation;
        transform.rotation = rotation;

        //총알 발사방향 설정
        bulletVec = direction.normalized;
    }

    //무기 발사 구현
    public void OnFire()
    {
        if(getFireStatus == 0 && nowBulletCnt > 0)
        {
            mainController.OnSetStatus(-1, -1, -1, 2, -1, -1);//일반 공격 상태로 변경
            //공격 방향에 따라 캐릭터 방향 전환
            if(transform.parent.TryGetComponent<Transform>(out Transform playerTransform))
            {
                if (bulletVec.x > 0)
                    playerTransform.transform.localScale = new Vector3(1, 1, 1);
                else if (bulletVec.x < 0)
                    playerTransform.transform.localScale = new Vector3(-1, 1, 1);
            }
            
                
            nowBulletCnt--;//현제 장탄 개수 감소

            //총알 생성
            GameObject bulletPre = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);//총알 생성
            bulletPre.GetComponent<Rigidbody2D>().AddForce(bulletVec * fireForce, ForceMode2D.Impulse);

            //발사 이펙트 생성
            GameObject fireEftPre = Instantiate(fireEft, eftPoint.transform.position, Quaternion.identity);//발사 이펙트
            fireEftPre.transform.localScale = transform.parent.transform.localScale;
            StartCoroutine(FireDelayIementation());//딜레이 구현 코루틴 호출
        }
    }

    //플레이어 일반공격 딜레이 구현
    IEnumerator FireDelayIementation()
    {
        yield return new WaitForSeconds(fireCoolTime);

        if (getFireStatus == 2)
            mainController.OnSetStatus(-1, -1, -1, 0, -1, -1);
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
        //재장전 이펙트 생성
        GameObject reloardEftPre = Instantiate(reloardEft, transform.position, Quaternion.identity, this.transform);//발사 이펙트

        float cntTime = 0f;//시간 경과값을 담을 변수
        //재장전 시 액션 재한 상태값 설정
        mainController.OnSetStatus(-1, -1, -1, -1, 2, -1);

        //재장전 시간 동안 장전을 취소할 행동을 할 경우 재장전 취소
        while (cntTime < reLoardTime)
        {
            cntTime += Time.deltaTime;//시간 카운트

            //장전 취소 시 코루틴 강제 종료
            if (mainController.getSetReloadStatus != 2)
            {
                yield break;
            }

            yield return null;
        }

        mainController.OnSetStatus(-1, -1, -1, -1, 0, -1);//재장전 이후 상태값 변경
        nowBulletCnt = maxBulletCnt;//장전
    }

    //강제 재장전 함수
    public void OnForcedReload()
    {
        nowBulletCnt = maxBulletCnt;//장전
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
