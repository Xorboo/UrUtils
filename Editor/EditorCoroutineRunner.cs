using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace UrUtils.Editor
{
    [InitializeOnLoad]
    public static class EditorCoroutineRunner
    {
        private static readonly List<IEnumerator> _runningCoroutines = new List<IEnumerator>();

        static EditorCoroutineRunner()
        {
            EditorApplication.update += ExecuteCoroutine;
        }

        public static IEnumerator Start(IEnumerator newCoroutine)
        {
            _runningCoroutines.Add(newCoroutine);
            return newCoroutine;
        }

        private static void ExecuteCoroutine()
        {
            for (int i = _runningCoroutines.Count - 1; i >= 0; i--)
            {
                bool finish = !_runningCoroutines[i].MoveNext();
                if (finish)
                {
                    _runningCoroutines.RemoveAt(i);
                }
            }
        }
    }
}
