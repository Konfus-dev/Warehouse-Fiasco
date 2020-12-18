using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed = 2;
    public float ejectMod = 5;
    public float visualSpeedScalar = 2;
    public bool power = true;

    private Vector3 direction;
    private float currentScroll;
    float texSizeX = 1f/5f;
    float texSizeZ = 1;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(texSizeX * gameObject.transform.lossyScale.x, texSizeZ * gameObject.transform.lossyScale.z);
        
    }

    private void Update()
    {
        if (power)
        {
            // Scroll texture to fake it moving
            currentScroll += Time.deltaTime * speed * visualSpeedScalar;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, currentScroll);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (power)
        {
            Rigidbody rgb = other.rigidbody;
            if (rgb != null)
            {
                if (other.gameObject.layer.Equals(11))
                {
                    //Debug.Log(other.gameObject);
                    other.transform.GetChild(0).GetChild(0).GetComponent<WarehouseWorkerAnimManager>().conveyorMod = 1;
                }
                // Get the direction of the conveyor belt 
                direction = transform.forward * speed;

                // Add a WORLD force to the other objects
                // Ignore the mass of the other objects so they all go the same speed (ForceMode.Acceleration)
                Vector3 movement = transform.forward * speed * Time.deltaTime;

                rgb.MovePosition(other.transform.position + movement);

            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        Rigidbody rgb = other.rigidbody;
        if (rgb != null)
        {
            if (other.gameObject.layer.Equals(11))
            {
                //Debug.Log(other.gameObject);
                other.transform.GetChild(0).GetChild(0).GetComponent<WarehouseWorkerAnimManager>().conveyorMod = 0.5f;
            }
            rgb.AddForce(transform.forward * speed * ejectMod);
        }
    }
}
