using UnityEngine;

public class ActorJump : MonoBehaviour
{
    public OnTriggers groundedTrigger;
    public float jumpForce = 2f;
    public float jumpMod = 2f;
    public float fallMod = 2.5f;
    public bool isGrounded;

    private Rigidbody rb;
    private ActorMove mover;

    private void Start()
    {
        mover = this.GetComponent<ActorMove>();
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        isGrounded = groundedTrigger.isTriggered;
        bool jumpInput = mover.jump;

        if (isGrounded && jumpInput)
        {
            rb.velocity = Vector3.up * jumpForce;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMod - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !jumpInput)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (jumpMod - 1) * Time.fixedDeltaTime;
        }
    }
}
