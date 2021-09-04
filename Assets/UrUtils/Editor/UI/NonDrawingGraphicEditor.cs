// Credit: Slipp Douglas Thompson
// Source: https://gist.github.com/capnslipp/349c18283f2fea316369
using UnityEditor;
using UnityEditor.UI;

namespace UrUtils.UI
{
    [CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), false)]
    public class NonDrawingGraphicEditor : GraphicEditor
    {
        public override void OnInspectorGUI() { }
    }
}
