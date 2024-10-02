using UnityEngine;
public class Jump : State
{
    private PlayerController controller;
    private bool hasJumped;
    private float cooldown;

    public Jump(PlayerController controller) : base("Jump")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        hasJumped = false;
        cooldown = 0.5f;

        // Controlar o animator
        controller.thisAnimator.SetBool("bJump", true);
        //Debug.Log("Entrou no Idle");
    }
    public override void Exit()
    {
        base.Exit();
        controller.thisAnimator.SetBool("bJump", false);
        //Debug.Log("Saiu do Idle");
    }

    public override void Update()
    {
        base.Update();
        // Troca pro idle
        cooldown -= Time.deltaTime;
        if (hasJumped && controller.isGround && cooldown <= 0)
        {
            controller.stateMachine.ChangeState(controller.idleState);
        }
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!hasJumped)
        {
            hasJumped = true;
            ApllyJump();
            GameManager.Instance.jumpSfx.Play();
        }


        Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        //Girar o vector em relação a camera
        walkVector = controller.GetForward() * walkVector;
        walkVector *= controller.movementSpeed * controller.jumpMovementFactor;

        //Debug.Log(walkVector);

        controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);

        // Rotate character
        controller.RotateBodyToFaceInput();


    }

    private void ApllyJump()
    {
        // Aplica o impulso
        Vector3 forceVector = Vector3.up * controller.jumpPower;
        controller.thisRigidbody.AddForce(forceVector, ForceMode.Impulse);
    }


}
