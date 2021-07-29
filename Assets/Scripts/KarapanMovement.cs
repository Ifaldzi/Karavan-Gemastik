using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarapanMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject circuit;

    Vector2 minMapPosition;
    Vector2 maxMapPosition;

    private float objectWidth;
    private float objectHeight;

    public float timeElapsed;

    private int coinCollected;

    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        minMapPosition = circuit.transform.position - circuit.GetComponent<SpriteRenderer>().bounds.size / 2;
        maxMapPosition = circuit.transform.position + circuit.GetComponent<SpriteRenderer>().bounds.size / 2;

        Debug.Log(maxMapPosition);

        objectWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        timeElapsed = 0;

        coinCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();

        timeElapsed += Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, minMapPosition.x + objectWidth, maxMapPosition.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, minMapPosition.y + objectHeight, maxMapPosition.y - objectHeight);

        transform.position = viewPos;
    }

    public void addCoin(int addValue)
    {
        coinCollected += addValue;
        Debug.Log("Coin collected: " + coinCollected);
    }

    void ApplyEngineForce()
    {
        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        else rb.drag = 0;

        //Caculate how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster than the 50% of max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0 && velocityVsUp > 0)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            animator.SetFloat("Speed", engineForceVector.sqrMagnitude);
        }

        //Apply force and pushes the car forward
        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (rb.velocity.magnitude / 2);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //Apply steering by rotating the car object
        rb.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        //Returns how how fast the car is moving sideways. 
        return Vector2.Dot(transform.right, rb.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        //Check if we are moving forward and if the player is hitting the brakes. In that case the tires should screech.
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        //If we have a lot of side movement then the tires should be screeching
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        //Clamp input to -1 and 1. 
        inputVector.x = Mathf.Clamp(inputVector.x, -1.0f, 1.0f);
        inputVector.y = Mathf.Clamp(inputVector.y, -1.0f, 1.0f);

        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return rb.velocity.magnitude;
    }

    public float GetVelocityVsUp()
    {
        return velocityVsUp;
    }

    public int getCoinCollected()
    {
        return this.coinCollected;
    }
}
