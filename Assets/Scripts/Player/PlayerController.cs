using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public properties
    public float movementSpeed = 10f;
    public float jumpMovementFactor = 1f;
    public float jumpPower = 5f;
    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Animator thisAnimator;
    [HideInInspector] public Collider thisCollider;
    // State Machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpState;
    [HideInInspector] public Dead deadState;
    // Internal Properties
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool hasJumpInput;
    [HideInInspector] public bool isGround;

    // Start is called before the first frame update
    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisAnimator = GetComponent<Animator>();
        thisCollider = GetComponent<Collider>();
    }

    void Start()
    {
        // StateMachine and its states
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        // Check game over
        if(GameManager.Instance.isGameOver) {
            if (stateMachine.currentStateName != deadState.name) {
                stateMachine.ChangeState(deadState);
            }
        }
        // Create input vector
        // Read Input (1)
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        //float movementZ = isUp ? +1 : isDown ? -1 : 0;
        //float movementX = isLeft ? -1 : isRight ? +1 : 0;
        movementVector = new Vector2(inputX, inputY);
        hasJumpInput = Input.GetKey(KeyCode.Space);

        //Passar a valocidade de 0 a 1 pro Animator
        float velocity = thisRigidbody.velocity.magnitude;
        float velocityRate = velocity / movementSpeed;
        thisAnimator.SetFloat("fVelocity", velocityRate);

        // Detect ground
        DetectGround();
        // State Machine (2)
        stateMachine.Update();

    }

    void LateUpdate()
    {
        //StateMachine
        stateMachine.LateUpdate();
    }

    void FixedUpdate()
    {
        //StateMachine
        stateMachine.FixedUpdateUpdate();
    }

    public Quaternion GetForward()
    {//Girar o vector em relação a camera
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput()
    {
        if (movementVector.IsZero())
        {
            return;
        }
        //Calcula a rotação
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion toRotation = q1 * q2;
        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);
        //Aplica a rotação
        thisRigidbody.MoveRotation(newRotation);
    }

    // void OnGUI()
    // {
    //     Rect rect = new Rect(5, 5, 200, 50);
    //     string text = "State: " + stateMachine.currentStateName;
    //     GUIStyle style = new GUIStyle();
    //     style.fontSize = (int)(60 * (Screen.width / 1920f));
    //     GUI.Label(rect, text, style);
    // }

    private void DetectGround()
    {
        //Physics.SphereCast(origin, radius,direction);
        isGround = false;
        // Detect ground
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.10f;
        if (Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                isGround = true;
            }
        }
    }
    // void OnDrawGizmos()
    // {
    //     Vector3 origin = transform.position;
    //     Vector3 direction = Vector3.down;
    //     Bounds bounds = thisCollider.bounds;
    //     float radius = bounds.size.x * 0.33f;
    //     float maxDistance = bounds.size.y * 0.10f;


    //     // Draw ray
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawRay(new Ray(origin, direction * maxDistance));

    //     //Draw origin
    //     Gizmos.color = Color.gray;

    //     // Draw Sphere
    //     Vector3 spherePosition = direction * maxDistance + origin;
    //     Gizmos.color = isGround ? Color.magenta : Color.blue;
    //     Gizmos.DrawSphere(spherePosition, radius);
    // }
}
