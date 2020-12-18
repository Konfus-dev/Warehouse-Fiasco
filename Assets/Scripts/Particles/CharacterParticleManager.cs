using UnityEngine;

public class CharacterParticleManager : MonoBehaviour
{
    public ParticleSystem walkParticles;
    public ParticleSystem jumpParticles;

    private Rigidbody rb;
    private ActorJump jumper;
    private bool inAir = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        jumper = this.GetComponent<ActorJump>();
    }

    // Update is called once per frame
    void Update()
    {

        if (jumper.isGrounded && Mathf.Abs(rb.velocity.magnitude) > 0.1)
        {
            walkParticles.Play();
        }
        else
        {
            walkParticles.Stop();
        }

        if (!jumper.isGrounded)
        {
            inAir = true;
        }
        else if (inAir && jumper.isGrounded)
        {
            jumpParticles.Play();
            inAir = false;
        }
    }
}
