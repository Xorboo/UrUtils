using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UrUtils.System.OS
{
    public abstract class OsSpecificValue<T> : MonoBehaviour
    {
        [SerializeField]
#if UNITY_EDITOR && ODIN_INSPECTOR
        [ValidateInput(nameof(OsListIsValid), "Duplicate entries found, only first for each type will be used")]
#endif
        List<OsValue> OsValues;


        #region Unity

        void Awake()
        {
            CheckOs();
        }

        #endregion

        protected abstract void UpdateValue(T value);

        void CheckOs()
        {
            var osValue = OsValues.FirstOrDefault(osValue => osValue.Os == SystemUtils.OperatingSystemFamily);
            if (osValue == null)
                return;

            UpdateValue(osValue.Value);
        }


#if UNITY_EDITOR && ODIN_INSPECTOR
        bool OsListIsValid()
        {
            if (OsValues == null)
                return true;

            var newList = OsValues?.GroupBy(x => x.Os).Select(x => x.First()).ToList();
            return OsValues.Count == newList.Count;
        }
#endif


        [Serializable]
        class OsValue
        {
            public OperatingSystemFamily Os = OperatingSystemFamily.Other;
            public T Value = default;
        }
    }
}
