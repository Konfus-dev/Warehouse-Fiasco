using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Transform interactObject;
    public float interactObjectMass;
    public Vector2 throwForce = new Vector2(300, 500);
    public bool interacting;

    private void OnTriggerStay(Collider other)
    {
        if (interacting)
        {
            return;
        }
        Interactable act = other.GetComponent<Interactable>();
        if (act == null) return;

        if (InputHandler.Instance.interact)
        {
            InputHandler.Instance.interact = false;
            interacting = true;
            interactObject = other.transform;
            interactObjectMass = act.rgb.mass;
            act.Interact(this);
        }
    }

    private void Update()
    {
        if (interacting && interactObject != null)
        {

            //remembered why we need this in update. Without it, the boxes do not stay with player when carried onto a conveyor belt
            interactObject.position = transform.position;
            interactObject.rotation = transform.rotation;
            //maybe could find a way around this by editting conveyor belt, but this is an easy temporary fix

            if (InputHandler.Instance.interact)
            {
                if (ThrowItem(throwForce))
                {
                    interacting = false;
                }
            }
            else if (InputHandler.Instance.drop)
            {
                if (ThrowItem(Vector2.zero))
                {
                    interacting = false;
                }
            }
        }
    }

    private bool ThrowItem(Vector2 force)
    {
        if (interactObject != null)
        {
            Interactable act = interactObject.GetComponent<Interactable>();
            if (act != null)
            {
                act.transform.parent = act.parent;
                act.gameObject.layer = LayerMask.NameToLayer("Interactable");

                //resetting object rigidbody
                act.rgb = act.gameObject.AddComponent<Rigidbody>();
                act.rgb.mass = interactObjectMass;

                act.rgb.AddForce(force.x * transform.forward + force.y * Vector3.up);
                act.isEnabled = true;
                interactObject = null;
                return true;
            }
            else
            {
                interactObject.parent = null;
            }

        }
        interactObject = null;
        return false;
    }

}
