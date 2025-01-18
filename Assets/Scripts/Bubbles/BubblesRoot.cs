using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bubbles
{
    public class BubblesRoot : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D root;

        [SerializeField]
        private TrailRenderer trail;

        [SerializeField]
        private int indexOffset;

        private readonly List<Bubble> _bubbles = new();

        public void ConsumeLastBubble()
        {
            Bubble lastBubble = _bubbles.LastOrDefault();
            if (lastBubble != null)
            {
                _bubbles.Remove(lastBubble);
                lastBubble.Disconnect();
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bubble bubble) && !bubble.IsConnected)
            {
                Bubble lastBubble = _bubbles.LastOrDefault();
                bubble.Connect(trail, lastBubble ? lastBubble.Index + indexOffset : indexOffset);
                _bubbles.Add(bubble);
            }
        }
    }
}
