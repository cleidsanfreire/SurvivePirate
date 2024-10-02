using UnityEngine;

public class Walking : State
{
    private PlayerController controller;
    public Walking(PlayerController controller) : base("Walking")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entrou no Walking");
    }
    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Saiu do Walking");
    }

    public override void Update()
    {
        base.Update();

        if(controller.hasJumpInput) {
            controller.stateMachine.ChangeState(controller.jumpState);
            return;
        }

        if (controller.movementVector.IsZero())
        {
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }
        
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        // Create movement vector
        //controller.movementVector; // X (esquerda  NEGATIVO e direita POSITIVO) e Y (para cima POSITIVO e para baixo NEGATIVO )

        Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        //Girar o vector em relação a camera
        walkVector = controller.GetForward() * walkVector;
        
        walkVector *= controller.movementSpeed;
        //Debug.Log(walkVector);

        controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);

        // Rotate character
        controller.RotateBodyToFaceInput();
    }    
}

