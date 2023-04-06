using UrUtils.Extensions;
using UrUtils.Extensions.ValueReference.Types;

namespace UrUtils.System.OS
{
    public class OsSpecificFloat : OsSpecificValue<FloatReference>
    {
        public UnityEventFloat Callback;

        protected override void UpdateValue(FloatReference value)
        {
            Callback.Invoke(value);
        }
    }
}
