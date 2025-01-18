using UnityEngine;

namespace Bubbles
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField]
        private float followSpeed;

        private TrailRenderer target;
        private int index;

        public int Index => index;
        public bool IsConnected { get; private set; }

        public void Connect(TrailRenderer trailRenderer, int followIndex)
        {
            IsConnected = true;
            target = trailRenderer;
            index = followIndex;
        }

        public void Disconnect()
        {
            IsConnected = false;
            target = null;
            index = -1;
            gameObject.SetActive(false);
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
    }
}
