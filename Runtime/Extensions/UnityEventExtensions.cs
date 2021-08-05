using System;
using UnityEngine;
using UnityEngine.Events;

namespace UrUtils.Extensions
{
    [Serializable]
    public class UnityEventBool : UnityEvent<bool> { }

    [Serializable]
    public class UnityEventInt : UnityEvent<int> { }

    [Serializable]
    public class UnityEventFloat : UnityEvent<float> { }

    [Serializable]
    public class UnityEventString : UnityEvent<string> { }

    [Serializable]
    public class UnityEventVector2 : UnityEvent<Vector2> { }

    [Serializable]
    public class UnityEventVector3 : UnityEvent<Vector3> { }
}
