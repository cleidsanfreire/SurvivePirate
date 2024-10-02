using UnityEngine;
public class Dead : State
{
    private PlayerController controller;
    public Dead(PlayerController controller) : base("Idle")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        controller.thisAnimator.SetTrigger("tGameOver");
        //Debug.Log("Entrou no Idle");
    }
    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Saiu do Idle");
    }

    public override void Update()
    {
        base.Update();
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

  
}
