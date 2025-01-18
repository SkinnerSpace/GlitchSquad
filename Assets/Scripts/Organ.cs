using System;
using Bubbles;
using UnityEngine;

public class Organ : MonoBehaviour
{
    public OrganType organType;
    public int currentHealth;
    public int maxHealth;
    [SerializeField] private float consumeTimeStep;

    private float lastConsumeTime;
    private float lastInteractionTime;

    public Animator animator;

    public float HealthRatio => (float) currentHealth / maxHealth;

    public delegate void MaxHealthReachedHandler(Organ organ);
    public event MaxHealthReachedHandler OnMaxHealthReached;
    public event Action OnInteractionTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool timeHasPassed = Time.time > lastInteractionTime + 15;
        if (timeHasPassed)
        {
            OnInteractionTriggered?.Invoke();
            lastInteractionTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        bool timeHasPassed = Time.time > lastConsumeTime + consumeTimeStep;
        bool hasBubbles = other.TryGetComponent(out BubblesRoot bubblesRoot);
        bool healthNotMax = currentHealth < maxHealth;
        if (healthNotMax && timeHasPassed && hasBubbles && bubblesRoot.TryConsumeLastBubble(animator))
        {
            currentHealth++;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            lastConsumeTime = Time.time;

            if (currentHealth == maxHealth)
            {
                OnMaxHealthReached?.Invoke(this);
            }
        }
    }
}

public enum OrganType
{
    Heart,
    Liver,
    Stomach,
    Lungs,

}
