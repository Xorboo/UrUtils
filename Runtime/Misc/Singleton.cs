using System;
using UnityEngine;

namespace UrUtils.Misc
{
    /// <summary>
    /// Be aware this will not prevent a non singleton constructor
    ///   such as `T myT = new T();`
    /// To prevent that, add `protected T () {}` to your singleton class.
    ///
    /// As a note, this is made as MonoBehaviour because we need Coroutines.
    /// </summary>
    [DefaultExecutionOrder(-10000)]
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly object _lock = new object();
        private static bool _applicationIsQuitting = false;

        /// <summary>
        /// Whether Singleton should call [DontDestroyOnLoad] or not
        /// Override it for singletons that should only exist during its Scene lifetime
        /// </summary>
        protected virtual bool IsPersistent => true;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting && Application.isPlaying)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        CreateInstance();
                    }

                    return _instance;
                }
            }
        }

        private static T _instance;

        public bool IsOriginal => Instance == GetComponent<T>();

        /// True if instance already exists
        public static bool Exists(bool tryFinding = false)
        {
            if (_instance != null)
            {
                return !_applicationIsQuitting;
            }

            if (tryFinding)
            {
                CreateInstance(false);
            }

            return _instance != null;
        }

        public static T CreateInstance(bool canCreateObject = true)
        {
            if (_instance == null)
            {
                var instances = FindObjectsOfType<T>();

                if (instances.Length > 1)
                {
                    Debug.LogError($"[Singleton] Something went wrong  - there should never be more than 1 singleton (have {instances.Length})! Reopening the scene might fix it.");
                    return _instance;
                }

                if (instances.Length > 0)
                {
                    _instance = instances[0];
                }
                else
                {
                    if (canCreateObject)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();

                        Debug.Log($"[Singleton] An instance of '{typeof(T)}' is needed in the scene, so '{singletonObject}' was created");
                    }
                    else
                    {
                        Debug.Log($"Should create singleton '{typeof(T)}', but [canCreateObject] is false. Add it manually to the scene or fix script execution order");
                    }
                }

            }

            return _instance;
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = GetComponent<T>();
                gameObject.name = $"[Singleton] {typeof(T)}";
                if (transform.parent)
                {
                    Debug.Log($"Sindleton '{gameObject.name}' is not a root object. Orphaning it to properly set `DontDestroyOnLoad`");
                    transform.parent = null;
                }

                if (IsPersistent)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                if (_instance != GetComponent<T>())
                {
                    Debug.Log($"Singleton '{typeof(T)}' awakes, but instance is already not null - destroying this copy");
                    DestroyImmediate(this);
                }
            }
        }

        protected void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed,
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (GetComponent<T>() == _instance)
            {
                _instance = null;
            }
        }
    }
}