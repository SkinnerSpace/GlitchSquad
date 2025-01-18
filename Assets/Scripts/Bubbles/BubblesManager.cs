using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bubbles;
using UnityEngine;

public class BubblesManager : MonoBehaviour
{
    public float spawnTimeInterval;

    public List<BubbleSpawner> spawners;

    public List<Bubble> bubbles;

    public List<Vector2> positions;

    private float time;

    public static BubblesManager Manager;

    private void OnEnable()
    {
        Manager = this;

        bubbles = new List<Bubble>();
    }

    private void Update()
    {
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }

        time = spawnTimeInterval;

        List<Bubble> inactiveBubbles = bubbles.Where(bubble => !bubble.gameObject.activeSelf).ToList();

        if (!inactiveBubbles.Any() || !positions.Any())
        {
            return;
        }

        Bubble randomBubble = inactiveBubbles[UnityEngine.Random.Range(0, inactiveBubbles.Count)];

        /*BubbleSpawner randomSpawner = spawners[UnityEngine.Random.Range(0, spawners.Count)];*/

        var randomPosition = positions[UnityEngine.Random.Range(0, positions.Count)];

        randomBubble.Respawn(randomPosition);
    }
}
