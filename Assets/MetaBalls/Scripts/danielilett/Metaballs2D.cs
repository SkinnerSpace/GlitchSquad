using System;
using UnityEngine;

public class Metaballs2D : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Color color = Color.white;

    public float Radius => radius;
    public Color Color => color;

    private void OnEnable()
    {
        MetaballSystem2D.Add(this);
    }

    private void OnDisable()
    {
        MetaballSystem2D.Remove(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
