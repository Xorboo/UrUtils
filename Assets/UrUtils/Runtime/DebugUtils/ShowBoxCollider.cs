using UnityEngine;
using UnityEngine.Serialization;

namespace UrUtils.DebugUtils
{
    [RequireComponent(typeof(BoxCollider))]
    public class ShowBoxCollider : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] bool AlwaysDraw = true;
        [SerializeField] Color Color = new Color(0f, 1f, 0f, 0.3f);

        void OnDrawGizmos()
        {
            if (AlwaysDraw)
            {
                DrawCollider();
            }
        }

        void OnDrawGizmosSelected()
        {
            if (!AlwaysDraw)
            {
                DrawCollider();
            }
        }

        void DrawCollider()
        {
            var t = transform;
            Gizmos.matrix = Matrix4x4.TRS(t.position, t.rotation, t.lossyScale);
            Gizmos.color = Color;

            var boxCollider = GetComponent<BoxCollider>();
            Gizmos.DrawCube(
                boxCollider ? boxCollider.center : Vector3.zero,
                boxCollider ? boxCollider.size : Vector3.one);
        }
#endif
    }
}
