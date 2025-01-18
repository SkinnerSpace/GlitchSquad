using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bubbles
{
    public class BubblesRoot : MonoBehaviour
    {
        [SerializeField]
        private Bubble tail;

        [SerializeField]
        private TrailRenderer trail;

        [SerializeField]
        private int indexOffset;

        [SerializeField]
        private int maxCount;

        private readonly List<Bubble> _bubbles = new();

        public bool IsFull => _bubbles.Count >= maxCount;

        private void Start()
        {
            tail.Connect(trail, 0);
            tail.transform.SetParent(null);
        }

        public bool TryConsumeLastBubble()
        {
            Bubble lastBubble = _bubbles.LastOrDefault();
            if (lastBubble != null)
            {
                _bubbles.Remove(lastBubble);
                lastBubble.Consume();
                return true;
            }

            return false;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bubble bubble) && !bubble.IsConnected)
            {
                if (!IsFull)
                {
                    Bubble lastBubble = _bubbles.LastOrDefault();
                    bubble.Connect(trail, lastBubble ? lastBubble.Index + indexOffset : indexOffset);
                    _bubbles.Add(bubble);
                }
            }

            if (other.CompareTag("SolidDoors"))
            {
                Debug.Log("-Pop");
            }
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bubble bubble) && !bubble.IsConnected)
            {
                if(IsFull && bubble != tail && !_bubbles.Contains(bubble))
                {
                    bubble.Push(bubble.transform.position - transform.position);
                }
            }
        }
    }
}
