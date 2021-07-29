using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropItem : MonoBehaviour
{
    abstract public void OnCollected(GameObject collectedBy);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.OnCollected(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
