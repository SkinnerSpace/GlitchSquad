using System.Collections.Generic;
using UnityEngine;

namespace Bubbles
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private List<Sprite> sprites;

        [SerializeField]
        private float followSpeed;

        private TrailRenderer target;
        private int index;

        public int Index => index;
        public bool IsConnected { get; private set; }

        private void Start()
        {
            if (sprites.Count > 0)
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count - 1)];
            }
        }

        public void Connect(TrailRenderer trailRenderer, int followIndex)
        {
            IsConnected = true;
            target = trailRenderer;
            index = followIndex;
        }

        public void Consume()
        {
            gameObject.SetActive(false);
            IsConnected = false;
            target = null;
            index = -1;
        }

        public void Update()
        {
            if (IsConnected && target.positionCount > 0)
            {
                int positionCount = target.positionCount;
                Vector3 nextPosition = target.GetPosition(Mathf.Clamp(positionCount - index - 1, 0, positionCount));
                transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * followSpeed);
            }
        }

        public void Pop()
        {
        }
    }
}
