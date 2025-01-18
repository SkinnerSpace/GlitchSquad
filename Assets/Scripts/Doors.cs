using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Doors : MonoBehaviour
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

    public GameObject closedDoors;
    public GameObject openedDoors;

    public GameObject yellowDoors;
    public GameObject redDoors;

    public CapsuleCollider2D doorsCollider;
    public GameObject solidCollider;

    public float closedDangerousTime;
    public float closedIdleTime;
    public float openedTime;

    public int flicksCount;
    public float flickTime;

    public States state;

    private float time;
    private int flicksLeft;

    private bool isRed = true;

    private string[] fxs;

    public static List<Doors> DoorsList;

    public static bool isPlating;


    private void OnEnable()
    {
        fxs = new[] { "Close1", "Close2", "Close3" };

        DoorsList ??= new List<Doors>();

        DoorsList.Add(this);
    }

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

                isPlating = false;

                break;
            case States.Closing:

                flicksLeft -= 1;

                if (flicksLeft <= 0)
                {
                    state = States.ClosedDangerous;
                    time = closedDangerousTime;

                    doorsCollider.gameObject.SetActive(true);

                    solidCollider.gameObject.SetActive(true);

                    openedDoors.SetActive(false);
                    closedDoors.SetActive(true);

                    if (!isPlating)
                    {
                        isPlating = true;

                        DoorsList = DoorsList.Where(door => door != null).ToList();

                        var closestDoor = DoorsList.OrderByDescending(door =>
                            Vector2.Distance(GetFlatPos(door.transform.position),
                                GetFlatPos(Camera.main.transform.position))).LastOrDefault();

                        float distance = Vector2.Distance(GetFlatPos(closestDoor.transform.position),
                            GetFlatPos(Camera.main.transform.position));

                        float volume = 1f - Mathf.InverseLerp(0f, 5f, distance) * 0.8f;

                        SoundManager.Instance.PlaySfx(fxs[Random.Range(0, fxs.Length)], volume);
                    }
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

                solidCollider.gameObject.SetActive(false);

                isRed = true;

                redDoors.SetActive(true);
                yellowDoors.SetActive(false);
                break;
        }
    }

    private Vector3 GetFlatPos(Vector3 pos)
    {
        return new Vector3(pos.x, pos.x, 0f);
    }
}
