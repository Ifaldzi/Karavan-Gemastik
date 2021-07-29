using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : DropItem
{
    public override void OnCollected(GameObject collectedBy)
    {
        // add point to player
        Debug.Log("pungut berhasil");
        collectedBy.GetComponent<KarapanMovement>().addCoin(1);
        AudioClip sfx = GetComponent<AudioSource>().clip;
        AudioSource.PlayClipAtPoint(sfx, transform.position);
    }
}
