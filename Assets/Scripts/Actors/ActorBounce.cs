using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBounce : MonoBehaviour
{
    public Rigidbody rb;
    public string bounceColliderName = "Bounce";

    private Vector3 lastFrameVelocity;

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint c in collision.contacts)
        {
            //Debug.Log(c.thisCollider.name);
            if(c.thisCollider.name == bounceColliderName) BounceOnCollision(collision.contacts[0].normal);
        }
    }

    private void BounceOnCollision(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        //Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * speed/2;
    }
}
