using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace UrUtils
{
    [InitializeOnLoad]
    public static class EditorCoroutineRunner
    {
        static readonly List<IEnumerator> RunningCoroutines = new List<IEnumerator>();

        static EditorCoroutineRunner()
        {
            EditorApplication.update += ExecuteCoroutine;
        }

        public static IEnumerator Start(IEnumerator newCoroutine)
        {
            RunningCoroutines.Add(newCoroutine);
            return newCoroutine;
        }

        static void ExecuteCoroutine()
        {
            for (int i = RunningCoroutines.Count - 1; i >= 0; i--)
            {
                bool finish = !RunningCoroutines[i].MoveNext();
                if (finish)
                {
                    RunningCoroutines.RemoveAt(i);
                }
            }
        }
    }
}
