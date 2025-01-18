using UnityEngine;

namespace Obstacles
{
    public class Slime : MonoBehaviour
    {
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
