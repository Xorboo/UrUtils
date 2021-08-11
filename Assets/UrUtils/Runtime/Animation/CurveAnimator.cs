#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;


namespace UrUtils.Animation
{
    public abstract class CurveAnimator : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [BoxGroup("Animation")]
#endif
        [SerializeField]
        [Tooltip("Value curve during a single loop")]
        protected AnimationCurve ValueCurve = null;

#if ODIN_INSPECTOR
        [BoxGroup("Animation")]
#endif
        [SerializeField]
        [Tooltip("Period of animation (in seconds)")]
        [Min(0f)]
        protected float AnimationPeriod = 2f;


        protected bool AnimationEnabled
        {
            get => DoAnimation;
            set
            {
                DoAnimation = value;

                // Reset to 0 when disabling
                if (!DoAnimation)
                    UpdateValue();
            }
        }

        bool DoAnimation = false;


        #region Unity

        protected virtual void Update()
        {
            if (AnimationEnabled)
                UpdateValue();
        }

        #endregion

        // Override to disable calculation logic
        protected virtual bool ShouldCalculate => true;

        // Override with actual logic aplying the value
        protected abstract void SetValue(float value);


        void UpdateValue()
        {
            if (!ShouldCalculate)
                return;

            float timeFactor = (Time.time % AnimationPeriod) / AnimationPeriod;
            float value = AnimationEnabled ? ValueCurve.Evaluate(timeFactor) : 0f;
            SetValue(value);
        }
    }
}
