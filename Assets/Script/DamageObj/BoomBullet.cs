using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : BulletBase
{
    public GameObject boomPre;//Æø¹ß ÇÁ¸®Æé
    
    //ÃÑ¾Ë ÆÄ±«½Ã È£ÃâµÇ´Â ÄÚ·çÆ¾
    public override IEnumerator BulletDestroy()
    {
        Instantiate(boomPre, this.gameObject.transform.position, Quaternion.identity);//Æø¹ß ÇÁ¸®Æé »ý¼º
        yield return base.BulletDestroy();
    }
}
