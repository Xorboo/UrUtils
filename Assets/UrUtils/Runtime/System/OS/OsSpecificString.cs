using UrUtils.Extensions;

namespace UrUtils.System.OS
{
    public class OsSpecificString : OsSpecificValue<string>
    {
        public UnityEventString Callback;

        protected override void UpdateValue(string value)
        {
            Callback.Invoke(value);
        }
    }
}
