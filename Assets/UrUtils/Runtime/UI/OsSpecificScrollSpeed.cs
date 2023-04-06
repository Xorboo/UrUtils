using UnityEngine;
using UnityEngine.UI;
using UrUtils.Extensions.ValueReference.Types;
using UrUtils.System.OS;

namespace UrUtils.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class OsSpecificScrollSpeed : OsSpecificValue<FloatReference>
    {
        private ScrollRect ScrollRect;


        protected override void UpdateValue(FloatReference value)
        {
            if (ScrollRect == null)
                ScrollRect = GetComponent<ScrollRect>();

            ScrollRect.scrollSensitivity = value;
        }
    }
}
