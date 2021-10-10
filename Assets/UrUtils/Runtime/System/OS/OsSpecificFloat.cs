using UrUtils.Extensions;

namespace UrUtils.System.OS
{
    public class OsSpecificFloat : OsSpecificValue<float>
    {
        public UnityEventFloat Callback;

        protected override void UpdateValue(float value)
        {
            Callback.Invoke(value);
        }
    }
}
