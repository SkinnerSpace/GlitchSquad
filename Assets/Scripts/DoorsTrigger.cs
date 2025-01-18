using System.Collections;
using System.Collections.Generic;
using Bubbles;
using UnityEngine;



public class DoorsTrigger : MonoBehaviour
{
    public Transform pivotPoint;
    public Transform thrustPoint;

    public float thrustPower;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 directionToB = (pivotPoint.position - other.transform.position).normalized; // Direction from A to B
            float angle = Vector2.SignedAngle(Vector2.up, directionToB); // Get the angle relative to the up direction

            Vector2 thrustDir = thrustPoint.position - pivotPoint.position;

            if (angle > 0)
            {
                other.gameObject.GetComponent<Player>().Thrust(thrustDir * thrustPower);
            }
            else
            {
                other.gameObject.GetComponent<Player>().Thrust(thrustDir * -1 * thrustPower);
            }

            return;
        }
    }
}
