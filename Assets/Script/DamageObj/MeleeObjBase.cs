using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeObjBase : DamageObj
{
    public float deletionTime = 0.1f;//삭제 시간
    float cntTime = 0;//흐른 시간 저장

    public virtual void Update()
    {
        cntTime += Time.deltaTime;//경과 시간 저장

        //경과 시간이 삭제 시간 보다 크면 삭제
        if (deletionTime <= cntTime)
            Destroy(this.gameObject);
    }
}
