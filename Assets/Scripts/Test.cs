using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UrUtils.Extensions.ValueReference;
using UrUtils.Extensions.ValueReference.Types;

public class Test : MonoBehaviour
{
    public ColorReference ColorTest;

    void OnEnable()
    {
        Dictionary<StringReference, GameObject> dict = new Dictionary<StringReference, GameObject>();
    }
}
