using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public float boundX = 1.5f;
    public float boundY = 1.2f;

    public Vector2 minMapPosition;
    public Vector2 maxMapPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float dx = target.position.x - transform.position.x;
        if(dx > boundX || dx < -boundX)
        {
            if(transform.position.x < target.position.x)
            {
                delta.x = dx - boundX;
            }
            else
            {
                delta.x = dx + boundX;
            }
        }

        float dy = target.position.y - transform.position.y;
        if (dy > boundY || dy < -boundY)
        {
            if (transform.position.y < target.position.y)
            {
                delta.y = dy - boundY;
            }
            else
            {
                delta.y = dy + boundY;
            }
        }

        Vector3 desiredPostion = transform.position += delta;


        desiredPostion.x = Mathf.Clamp(desiredPostion.x, minMapPosition.x, maxMapPosition.x);
        desiredPostion.y = Mathf.Clamp(desiredPostion.y, minMapPosition.y, maxMapPosition.y);

        //Debug.Log("clamp" + desiredPostion);

        transform.position = Vector3.Lerp(transform.position, desiredPostion, 1);
        //Debug.Log("final camera pos" + transform.position);
        //transform.position = desiredPostion;
    }
}
