using UrUtils.Extensions;

namespace UrUtils.System.OS
{
    public class OsSpecificInt : OsSpecificValue<int>
    {
        public UnityEventInt Callback;

        protected override void UpdateValue(int value)
        {
            Callback.Invoke(value);
        }
    }
}
