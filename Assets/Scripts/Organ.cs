using Bubbles;
using UnityEngine;

public class Organ : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private float consumeTimeStep;

    private float lastConsumeTime;

    private void OnTriggerStay2D(Collider2D other)
    {
        bool timeHasPassed = Time.time > lastConsumeTime + consumeTimeStep;
        bool hasBubbles = other.TryGetComponent(out BubblesRoot bubblesRoot);
        if (timeHasPassed && hasBubbles && bubblesRoot.TryConsumeLastBubble())
        {
            currentHealth++;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            lastConsumeTime = Time.time;
        }
    }
}
