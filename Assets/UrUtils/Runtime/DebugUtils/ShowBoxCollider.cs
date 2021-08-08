using UnityEngine;

namespace UrUtils.DebugUtils
{
    [RequireComponent(typeof(BoxCollider))]
    public class ShowBoxCollider : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool _alwaysDraw = true;
        [SerializeField] private Color _color = new Color(0f, 1f, 0f, 0.3f);

        void OnDrawGizmos()
        {
            if (_alwaysDraw)
            {
                DrawCollider();
            }
        }

        void OnDrawGizmosSelected()
        {
            if (!_alwaysDraw)
            {
                DrawCollider();
            }
        }

        void DrawCollider()
        {
            var t = transform;
            Gizmos.matrix = Matrix4x4.TRS(t.position, t.rotation, t.lossyScale);
            Gizmos.color = _color;

            var boxCollider = GetComponent<BoxCollider>();
            Gizmos.DrawCube(
                boxCollider ? boxCollider.center : Vector3.zero,
                boxCollider ? boxCollider.size : Vector3.one);
        }
#endif
    }
}
