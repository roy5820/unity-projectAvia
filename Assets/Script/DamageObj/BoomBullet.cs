using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : BulletBase
{
    public GameObject boomPre;//Æø¹ß ÇÁ¸®Æé

    //ÆÄ±«½Ã Æø¹ß
    private void OnDestroy()
    {
        Instantiate(boomPre, this.gameObject.transform.position, Quaternion.identity);//Æø¹ß ÇÁ¸®Æé »ý¼º
    }

    //Ãæµ¹ ½Ã ÃÑ¾Ë ÆÄ±«
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Destroy(this.gameObject);
    }
}
