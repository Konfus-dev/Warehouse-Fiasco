using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltChanger : MonoBehaviour
{
    public Color changeColor;
    public GameObject objectToAdd;
    OnTriggers trig;
    Collider prevObject;

    void Start()
    {
        trig = GetComponent<OnTriggers>();
    }

    void Update()
    {
        if (trig.isTriggered)
        {
            Collider col = trig.selectedObject;
            if (col != null && col != prevObject)
            {
                prevObject = col;
                EditInteractable(col);
            }
        }
        else
        {
            //prevObject = null;
        }
    }

    void EditInteractable(Collider col)
    {
        //change crate color
        Renderer rend = col.GetComponent<Renderer>();
        if (rend != null && changeColor.a > 0)
        {
            Color c;
            if (changeColor.r == 0 && changeColor.g == 0 && changeColor.b == 0)
            {
                c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 255f);
            }
            else
            {
                c = changeColor;
            }
            rend.material.shader = Shader.Find("Universal Render Pipeline/Simple Lit");
            rend.material.SetColor("_BaseColor", c);
        }
        //add item to crate
        if (objectToAdd != null)
        {
            Crate crate = col.GetComponent<Crate>();
            if (crate != null)
            {
                GameObject newItem = Instantiate(objectToAdd);
                newItem.transform.position = transform.position;
                Item item = newItem.GetComponent<Item>();
                col.transform.rotation = Quaternion.LookRotation(transform.forward);
                if (crate.AddItem(item))
                {
                    //box has added our item, yay! Happy effect :D
                }
                else
                {
                    Debug.Log("No Space!");
                    Destroy(newItem);
                    //box is overflowing, make a mess of items. Sad effect :(
                }
            }
        }
    }

}
