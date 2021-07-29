using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontal;

    public float vertical;

    private KarapanMovement playerMovement;

    public void OnButtonUpDown()
    {
        vertical = 1;
        Debug.Log("maju");
    }

    public void OnHorizontalButtonRelease()
    {
        horizontal = 0;
    }

    public void OnButtonDownDown()
    {
        vertical = -1;
    }

    public void OnButtonRightDown()
    {
        horizontal = 1;
    }

    public void OnButtonLeftDown()
    {
        horizontal = -1;
    }

    public void OnVerticalButtonRelease()
    {
        vertical = 0;
    }

    private void Awake()
    {
        playerMovement = GetComponent<KarapanMovement>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
        float horizontalFromKey = Input.GetAxis("Horizontal");
        horizontal = horizontalFromKey == 0 ? horizontal : horizontalFromKey;
        vertical = Input.GetAxis("Vertical") == 0 ? vertical : Input.GetAxis("Vertical");

        if(GameManager.state == State.RACE)
        {
            inputVector.x = horizontal;
            inputVector.y = vertical;
            playerMovement.SetInputVector(inputVector);
        }
        
    }
}
