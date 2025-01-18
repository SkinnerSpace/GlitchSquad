using Bubbles;
using UnityEngine;

public class Organ : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    [SerializeField] private float consumeTimeStep;

    private float lastConsumeTime;

    public float HealthRatio => (float) currentHealth / maxHealth;

    private void OnTriggerStay2D(Collider2D other)
    {
        bool timeHasPassed = Time.time > lastConsumeTime + consumeTimeStep;
        bool hasBubbles = other.TryGetComponent(out BubblesRoot bubblesRoot);
        bool healthNotMax = currentHealth < maxHealth;
        if (healthNotMax && timeHasPassed && hasBubbles && bubblesRoot.TryConsumeLastBubble())
        {
            currentHealth++;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            lastConsumeTime = Time.time;
        }
    }
}
