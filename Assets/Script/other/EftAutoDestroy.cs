using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EftAutoDestroy : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;

    void Start()
    {
        // �ִϸ����� ������Ʈ�� �����ɴϴ�.
        animator = GetComponent<Animator>();

        // �ִϸ��̼��� ���̸� �����ɴϴ�.
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // �ִϸ��̼��� ���̸�ŭ ����� �Ŀ� ������Ʈ�� �ı��մϴ�.
        StartCoroutine(DestroyAfter(animationLength));
    }

    IEnumerator DestroyAfter(float seconds)
    {
        // ������ �ð���ŭ ����մϴ�.
        yield return new WaitForSeconds(seconds);

        // �� ������Ʈ�� �ı��մϴ�.
        Destroy(gameObject);
    }
}
