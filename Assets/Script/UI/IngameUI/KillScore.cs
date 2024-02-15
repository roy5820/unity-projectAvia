using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillScore : MonoBehaviour
{
    Text ScoreTxt = null;//킬스코어 표시할 텍스트 오브젝트

    private void Start()
    {
        ScoreTxt = GetComponent<Text>();
    }

    private void Update()
    {
        ScoreTxt.text = (GameManeger.instance.KillScore).ToString();
    }
}
