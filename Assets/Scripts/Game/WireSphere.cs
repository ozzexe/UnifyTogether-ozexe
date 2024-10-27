using UnityEngine;

namespace Game
{
    public class WireSphere : MonoBehaviour
    {
        public Color color = Color.blue;
        public float size = 1f;

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, size);
        }
    }
}
