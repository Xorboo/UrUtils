// Credit: Slipp Douglas Thompson
// Source: https://gist.github.com/capnslipp/349c18283f2fea316369
using UnityEngine;
using UnityEngine.UI;

namespace UrUtils.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class NonDrawingGraphic : Graphic
    {
        public override void SetMaterialDirty() { }
        public override void SetVerticesDirty() { }

        // Probably not necessary since the chain of calls `Rebuild()`->`UpdateGeometry()`->`DoMeshGeneration()`->`OnPopulateMesh()` won't happen; so here really just as a fail-safe.
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}