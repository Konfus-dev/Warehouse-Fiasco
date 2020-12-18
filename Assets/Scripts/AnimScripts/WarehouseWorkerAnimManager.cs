using UnityEngine;

public class WarehouseWorkerAnimManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private ActorJump jump;

    public float conveyorMod = 1;

    private Animator animator;
    private PlayerGrab grab;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        grab = this.transform.parent.GetComponentInChildren<PlayerGrab>();
    }

    private void Update()
    {
        float speed = rb.velocity.magnitude;

        if (jump.isGrounded)
        {
            animator.SetFloat("Speed", speed * conveyorMod);
            animator.SetBool("Jump", false);
        }
        else if (!jump.isGrounded) 
            animator.SetBool("Jump", true);

        SetHoldingAnim(grab.interacting);
    }


    // use to set holding animation
    public void SetHoldingAnim(bool isHoldingObj)
    {
        if (isHoldingObj)
            animator.SetLayerWeight(1, 1);
        else
            animator.SetLayerWeight(1, 0);
    }
}
