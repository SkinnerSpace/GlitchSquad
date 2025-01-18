using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bubbles
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField]
        private float pushDistance;

        [SerializeField]
        private float pushSpeed;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private List<Sprite> sprites;

        [SerializeField]
        private float followSpeed;

        public static List<Bubble> allBubbles;

        private TrailRenderer _target;
        private int _index;
        private Vector3 initPosition;
        private Vector3 pushDirection;
        private Vector3 targetPosition;

        public int Index => _index;
        public bool IsConnected { get; private set; }

        private void Start()
        {
            initPosition = transform.position;
            if (sprites.Count > 0)
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count - 1)];
            }
        }

        public void Respawn(Vector2 position)
        {
            transform.position = position;

            gameObject.SetActive(true);
            IsConnected = false;
            _target = null;
            _index = -1;
        }

        private void Awake()
        {
            if (BubblesManager.Manager.bubbles.Contains(this))
            {
                return;
            }

            BubblesManager.Manager.positions.Add(transform.position);

            BubblesManager.Manager.bubbles.Add(this);
        }

        public void Connect(TrailRenderer trailRenderer, int followIndex)
        {
            IsConnected = true;
            _target = trailRenderer;
            _index = followIndex;
        }

        public void Consume()
        {
            gameObject.SetActive(false);
            IsConnected = false;
            _target = null;
            _index = -1;
        }

        public void Update()
        {
            if (IsConnected && _target.positionCount > 0)
            {
                int positionCount = _target.positionCount;
                Vector3 nextPosition = _target.GetPosition(Mathf.Clamp(positionCount - _index - 1, 0, positionCount));
                transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * followSpeed);
            }

            if (IsConnected || pushDistance < Mathf.Epsilon)
            {
                return;
            }

            if (pushDirection != Vector3.zero)
            {
                targetPosition = initPosition + pushDirection.normalized * pushDistance;
                pushDirection = Vector3.zero;
            }

            bool targetReached = Vector3.Distance(transform.position, targetPosition) < 0.1f;

            if (targetReached)
            {
                targetPosition = initPosition;
            }

            if (!targetReached)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, pushSpeed * Time.deltaTime);
            }
        }

        public void Push(Vector3 direction)
        {
            if (IsConnected)
            {
                return;
            }

            pushDirection = direction.normalized;
        }

        public void Pop()
        {
        }
    }
}
