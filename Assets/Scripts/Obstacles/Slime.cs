using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class Slime : MonoBehaviour
    {
        [SerializeField]
        private float hideSmooth;

        [SerializeField]
        private Transform visual;

        [SerializeField]
        private Transform hiddenPosition;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private List<Sprite> sprites;

        private Vector3 initialPosition;

        private void Start()
        {
            initialPosition = visual.transform.localPosition;
            if (sprites.Count > 0)
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count - 1)];
            }
        }

        public void SetHideProgress(float progress)
        {
            Vector3 desiredPosition = Vector3.Lerp(initialPosition, hiddenPosition.localPosition, progress);
            visual.transform.localPosition = Vector3.Lerp(visual.transform.localPosition, desiredPosition, Time.deltaTime * hideSmooth);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.SetIsSlow(true);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.SetIsSlow(false);
            }
        }
    }
}
