using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public Rigidbody rgb;
    public Collider col;
    public Transform parent;
    public bool isEnabled;
    public abstract void Interact<T>(T component) where T : Component;

}
