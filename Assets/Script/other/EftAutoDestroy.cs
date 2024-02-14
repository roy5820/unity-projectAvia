using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EftAutoDestroy : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    void Start()
    {
        // 애니메이터 컴포넌트를 가져옵니다.
        animator = GetComponent<Animator>();

        // 애니메이션의 길이를 가져옵니다.
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // 애니메이션의 길이만큼 대기한 후에 오브젝트를 파괴합니다.
        StartCoroutine(DestroyAfter(animationLength));
    }

    IEnumerator DestroyAfter(float seconds)
    {
        // 지정된 시간만큼 대기합니다.
        yield return new WaitForSeconds(seconds);

        // 이 오브젝트를 파괴합니다.
        Destroy(gameObject);
    }
}
