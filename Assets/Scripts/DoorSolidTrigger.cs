using Bubbles;
using UnityEngine;

public class DoorSolidTrigger : MonoBehaviour
{
    public Transform pivotPoint;
    public Transform thrustPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Bubble bubble) && bubble.IsConnected)
        {
            bubble.Disconnect();
        }
    }
}
