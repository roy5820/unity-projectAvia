using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : BulletBase
{
    public GameObject boomPre;//气惯 橇府崎

    //颇鲍矫 气惯
    private void OnDestroy()
    {
        Instantiate(boomPre, this.gameObject.transform.position, Quaternion.identity);//气惯 橇府崎 积己
    }
}
