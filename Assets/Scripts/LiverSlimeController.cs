using System.Collections.Generic;
using System.Linq;
using Obstacles;
using UnityEngine;

[RequireComponent(typeof(Organ))]
public class LiverSlimeController : MonoBehaviour
{
    private Organ _organ;
    private List<Slime> _slimes;

    private void Start()
    {
        _organ = GetComponent<Organ>();
        _slimes = FindObjectsOfType<Slime>().ToList();
    }

    private void Update()
    {
        _slimes.ForEach(slime => slime.SetHideProgress(_organ.HealthRatio));
    }
}
