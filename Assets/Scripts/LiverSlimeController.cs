using System.Collections.Generic;
using System.Linq;
using Obstacles;
using UnityEngine;

[RequireComponent(typeof(Organ))]
public class LiverSlimeController : MonoBehaviour
{
    private Organ organ;
    private List<Slime> slimes;

    private void Start()
    {
        organ = GetComponent<Organ>();
        slimes = FindObjectsOfType<Slime>().ToList();
    }

    private void Update()
    {
        slimes.ForEach(slime => slime.SetHideProgress(organ.HealthRatio));
    }
}
