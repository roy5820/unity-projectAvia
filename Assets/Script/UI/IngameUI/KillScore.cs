using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillScore : MonoBehaviour
{
    Text ScoreTxt = null;//ų���ھ� ǥ���� �ؽ�Ʈ ������Ʈ

    private void Start()
    {
        ScoreTxt = GetComponent<Text>();
    }

    private void Update()
    {
        ScoreTxt.text = (GameManeger.instance.KillScore).ToString();
    }
}
