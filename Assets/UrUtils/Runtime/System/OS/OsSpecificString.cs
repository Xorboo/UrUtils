using UrUtils.Extensions;
using UrUtils.Extensions.ValueReference.Types;

namespace UrUtils.System.OS
{
    public class OsSpecificString : OsSpecificValue<StringReference>
    {
        public UnityEventString Callback;

        protected override void UpdateValue(StringReference value)
        {
            Callback.Invoke(value);
        }
    }
}
