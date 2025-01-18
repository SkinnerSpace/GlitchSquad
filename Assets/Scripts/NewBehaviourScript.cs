using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 1.5 closing
    // 2 closed
    // 4 opened

    public enum States
    {
        Opened,
        Closing,
        ClosedDangerous,
        CLosedIdle

    }

    public Transform pivotPoint;
    public Transform thrustPoint;

    public GameObject closedDoors;
    public GameObject openedDoors;

    public GameObject yellowDoors;
    public GameObject redDoors;

    public CapsuleCollider2D doorsCollider;

    public float thrustPower;

    public float closedDangerousTime;
    public float closedIdleTime;
    public float openedTime;

    public int flicksCount;
    public float flickTime;

    public States state;

    private float time;
    private int flicksLeft;

    private bool isRed = true;

    private void Update()
    {
        if (time > 0f)
        {
            time -= Time.deltaTime;

            return;
        }

        switch (state)
        {
            case States.Opened:
                state = States.Closing;
                time = flickTime;
                flicksLeft = flicksCount;

                isRed = true;

                redDoors.SetActive(true);
                yellowDoors.SetActive(false);

                break;
            case States.Closing:

                flicksLeft -= 1;

                if (flicksLeft <= 0)
                {
                    state = States.ClosedDangerous;
                    time = closedDangerousTime;

                    doorsCollider.gameObject.SetActive(true);

                    openedDoors.SetActive(false);
                    closedDoors.SetActive(true);
                }
                else
                {
                    time = flickTime;
                }

                isRed = !isRed;

                yellowDoors.SetActive(!isRed);
                redDoors.SetActive(isRed);

                break;
            case States.ClosedDangerous:
                state = States.CLosedIdle;
                time = closedIdleTime;

                doorsCollider.gameObject.SetActive(false);

                break;
            case States.CLosedIdle:
                state = States.Opened;
                time = openedTime;

                openedDoors.SetActive(true);
                closedDoors.SetActive(false);

                isRed = true;

                redDoors.SetActive(true);
                yellowDoors.SetActive(false);
                break;
        }
    }

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
        }
    }
}