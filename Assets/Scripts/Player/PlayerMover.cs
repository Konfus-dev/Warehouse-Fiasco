using UnityEngine;

public class PlayerMover : ActorMove
{
    private ActorJump jumper;
    private Vector3 lastAxisInput;
    public float jumpControl;

    private void Start()
    {
        jumper = this.GetComponent<ActorJump>();
        lastAxisInput = new Vector3();
        base.Start();
    }

    void FixedUpdate()
    {

        jump = InputHandler.Instance.jump;

        //get input dir
        Vector3 axisInput = new Vector3(InputHandler.Instance.axisInput.x, 0, InputHandler.Instance.axisInput.y);

        if (!jumper.isGrounded && (Mathf.Abs(lastAxisInput.x) < 0.1) && (Mathf.Abs(lastAxisInput.z) < 0.1))
        {
            axisInput = new Vector3(axisInput.x * 0.4f, 0, axisInput.z * 0.4f);
        }
        else if (!jumper.isGrounded && (Mathf.Abs(lastAxisInput.x) > Mathf.Abs(lastAxisInput.z)) && (Mathf.Abs(axisInput.x) > 0))
        {
            axisInput = new Vector3(axisInput.x, 0, axisInput.x * .1f);
        }
        else if (!jumper.isGrounded && (Mathf.Abs(lastAxisInput.x) < Mathf.Abs(lastAxisInput.z)) && (Mathf.Abs(axisInput.z) > 0))
        {
            axisInput = new Vector3(axisInput.x * .1f, 0, axisInput.z);
        }
        
        if (jumper.isGrounded) lastAxisInput = axisInput;

        axisInput = MoveActor(axisInput);

        //if boost
        if (InputHandler.Instance.boost.Value) Boost(axisInput);
        //if (InputHandler.Instance.crouch) Crouch();
    }

}
