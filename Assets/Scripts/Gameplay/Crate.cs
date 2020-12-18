using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable
{

    public float fallMod = 2.5f;
    public float jumpMod = 2f;
    public List<Item> heldItems;

    private void Start()
    {
        rgb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        parent = transform.parent;
        heldItems = new List<Item>();
        heldItems.Capacity = transform.childCount;
    }


    public override void Interact<T>(T component)
    {
        isEnabled = false;
        Destroy(col.attachedRigidbody);
        gameObject.layer = LayerMask.NameToLayer("HeldObject");
        transform.parent = component.transform;

    }

    private void FixedUpdate()
    {
        if (isEnabled)
        {
            if (rgb.velocity.y < 0)
            {
                rgb.velocity += Vector3.up * Physics.gravity.y * (fallMod - 1) * Time.fixedDeltaTime;
            }
            else if (rgb.velocity.y > 0)
            {
                rgb.velocity += Vector3.up * Physics.gravity.y * (jumpMod - 1) * Time.fixedDeltaTime;
            }
        }
    }


    public bool AddItem(Item item)
    {
        if (item == null) return false;

        if (heldItems.Count < heldItems.Capacity)
        {
            Transform p = FindEmptyChild();
            if (p != null)
            {
                item.transform.SetPositionAndRotation(p.position, p.rotation);
                item.transform.SetParent(p);
                heldItems.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private Transform FindEmptyChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.childCount == 0)
            {
                return child;
            }
        }
        return null;
    }


}
