using Sirenix.OdinInspector;
using UnityEngine;


namespace UrUtils.Rendering
{
    public class FrameRateLock : MonoBehaviour
    {
        enum LockType
        {
            Unlocked,
            Locked
        };

        [SerializeField]
#if ODIN_INSPECTOR
        [EnumToggleButtons]
#endif
        LockType Lock = LockType.Unlocked;

        [SerializeField, Min(1)]
#if ODIN_INSPECTOR
        [ShowIf("@" + nameof(Lock) + " == " + nameof(LockType) + "." + nameof(LockType.Locked))]
#endif
        int FrameRate = 60;


        #region Unity

        void Awake()
        {
            UpdateFrameRate();
        }

        #endregion


        public void SetFramerate(int frameRate)
        {
            Lock = frameRate <= 0 ? LockType.Unlocked : LockType.Locked;
            FrameRate = frameRate;
            UpdateFrameRate();
        }


        void UpdateFrameRate()
        {
            switch (Lock)
            {
                case LockType.Unlocked:
                    Debug.Log("Unlocking frame rate");
                    Application.targetFrameRate = -1;
                    break;

                case LockType.Locked:
                    Debug.Log($"Locking frame rate to {FrameRate} fps");
                    Application.targetFrameRate = FrameRate;
                    break;

                default:
                    Debug.LogError($"Unsupported frame rate lock type: {Lock}, unlocking framerate");
                    Application.targetFrameRate = -1;
                    break;
            }
        }
    }
}
