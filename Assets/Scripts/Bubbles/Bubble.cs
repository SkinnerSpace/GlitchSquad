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
        private Vector3 _initPosition;
        private Vector3 _targetPosition;
        private Vector3 _pushDirection;

        public int Index => _index;
        public bool IsConnected { get; private set; }

        private void Start()
        {
            _initPosition = transform.position;
            _targetPosition = _initPosition;

            if (sprites.Count > 0)
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count - 1)];
            }

            BubblesManager.Manager.bubbles ??= new List<Bubble>();

            if (BubblesManager.Manager.bubbles.Contains(this))
            {
                return;
            }

            BubblesManager.Manager.positions.Add(transform.position);

            BubblesManager.Manager.bubbles.Add(this);
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

        }

        public void Connect(TrailRenderer trailRenderer, int followIndex)
        {
            IsConnected = true;
            _target = trailRenderer;
            _index = followIndex;

            string[] fxs = new[] { "Action1", "Action2", "Action3" };

            SoundManager.Instance.PlaySfx(fxs[Random.Range(0, fxs.Length)]);
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

            if (_pushDirection != Vector3.zero)
            {
                _targetPosition = _initPosition + _pushDirection.normalized * pushDistance;
                _pushDirection = Vector3.zero;
            }

            bool targetReached = Vector3.Distance(transform.position, _targetPosition) < 0.2f;

            if (targetReached)
            {
                _targetPosition = _initPosition;
            }

            if (!targetReached)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, pushSpeed * Time.deltaTime);
            }
        }

        public void Push(Vector3 direction)
        {
            if (IsConnected)
            {
                return;
            }

            _pushDirection = direction.normalized;
        }

        public void Pop()
        {
        }
    }
}
