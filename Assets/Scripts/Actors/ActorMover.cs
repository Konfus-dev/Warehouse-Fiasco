using UnityEngine;

public abstract class ActorMove : MonoBehaviour
{
    [HideInInspector]
    public bool jump = false;

    [SerializeField]
    protected Transform actorMesh;
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected float movementMod;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    protected float getToSpeed;
    [SerializeField]
    protected float boostForce;
    [SerializeField]
    protected Camera camera;

    protected Rigidbody rb;
    protected float targetSpeed;


    protected void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        targetSpeed = movementSpeed;
    }

    protected Vector3 MoveActor(Vector3 axisInput)
    {
        //get target speed
        targetSpeed = Mathf.Lerp(targetSpeed, movementSpeed, Time.fixedDeltaTime * getToSpeed);

        //take cam rotation into consideration when applying movement
        if (this.CompareTag("Player")) axisInput = Quaternion.Euler(0, camera.gameObject.transform.rotation.eulerAngles.y, 0) * axisInput;

        //rotate actor toward movement direction
        RotateTowardMovementDir(axisInput);

        //apply speed to actor
        Vector3 targetVelocity = transform.TransformDirection(axisInput) * targetSpeed * movementMod * Time.fixedDeltaTime;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * getToSpeed);

        return rb.velocity;
    }

    private void RotateTowardMovementDir(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        actorMesh.rotation = Quaternion.RotateTowards(actorMesh.rotation, rotation, rotationSpeed);
    }

    protected void Boost(Vector3 boostDirection)
    {
        rb.AddForce((rb.transform.forward + boostDirection) * boostForce , ForceMode.Impulse);
    }

    /*protected void Crouch()
    {
        if (!isCrouching)
        {
            actorMesh.localScale = new Vector3(actorMesh.localScale.x, actorMesh.localScale.y / 2, actorMesh.localScale.z);
            movementMod /= 2;
            isCrouching = true;
        }
        else
        {
            actorMesh.localScale = new Vector3(actorMesh.localScale.x, actorMesh.localScale.y * 2, actorMesh.localScale.z); 
            movementMod *= 2;
            isCrouching = false;
        }
    }*/
}
