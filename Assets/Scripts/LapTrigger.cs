using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public LapTrigger next;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LapCounter lapCounter = collision.gameObject.GetComponent<LapCounter>();
        if(lapCounter)
        {
            Debug.Log(gameObject.name);
            lapCounter.OnLapTrigger(this);
        }
    }
}
