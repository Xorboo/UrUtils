using UrUtils.Extensions;
using UrUtils.Extensions.ValueReference.Types;

namespace UrUtils.System.OS
{
    public class OsSpecificInt : OsSpecificValue<IntReference>
    {
        public UnityEventInt Callback;

        protected override void UpdateValue(IntReference value)
        {
            Callback.Invoke(value);
        }
    }
}
