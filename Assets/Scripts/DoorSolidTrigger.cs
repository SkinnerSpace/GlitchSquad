using System.Collections;
using System.Collections.Generic;
using Bubbles;
using Unity.VisualScripting;
using UnityEngine;

public class DoorSolidTrigger : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.CompareTag("Bubble"))
        {
            other.gameObject.GetComponent<Bubble>().Pop();
        }
    }
}
