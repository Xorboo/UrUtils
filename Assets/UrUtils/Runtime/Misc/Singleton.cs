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
        static readonly object Lock = new object();
        static bool ApplicationIsQuitting = false;

        /// <summary>
        /// Whether Singleton should call [DontDestroyOnLoad] or not
        /// Override it for singletons that should only exist during its Scene lifetime
        /// </summary>
        protected virtual bool IsPersistent => true;

        public static T Instance
        {
            get
            {
                if (ApplicationIsQuitting && Application.isPlaying)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }

                lock (Lock)
                {
                    if (InstanceRef == null)
                    {
                        CreateInstance();
                    }

                    return InstanceRef;
                }
            }
        }

        static T InstanceRef;

        public bool IsOriginal => Instance == GetComponent<T>();

        /// True if instance already exists
        public static bool Exists(bool tryFinding = false)
        {
            if (InstanceRef != null)
            {
                return !ApplicationIsQuitting;
            }

            if (tryFinding)
            {
                CreateInstance(false);
            }

            return InstanceRef != null;
        }

        public static T CreateInstance(bool canCreateObject = true)
        {
            if (InstanceRef == null)
            {
                var instances = FindObjectsOfType<T>();

                if (instances.Length > 1)
                {
                    Debug.LogError(
                        $"[Singleton] Something went wrong  - there should never be more than 1 singleton (have {instances.Length})! Reopening the scene might fix it.");
                    return InstanceRef;
                }

                if (instances.Length > 0)
                {
                    InstanceRef = instances[0];
                }
                else
                {
                    if (canCreateObject)
                    {
#if UNITY_EDITOR
                        if (!Application.isPlaying)
                        {
                            Debug.LogWarning($"Not creating a singleton '{typeof(T)}' outside of play session!");
                            return null;
                        }
#endif

                        var singletonObject = new GameObject();
                        InstanceRef = singletonObject.AddComponent<T>();

                        Debug.Log(
                            $"[Singleton] An instance of '{typeof(T)}' is needed in the scene, so '{singletonObject}' was created");
                    }
                    else
                    {
                        Debug.Log(
                            $"Should create singleton '{typeof(T)}', but [canCreateObject] is false. Add it manually to the scene or fix script execution order");
                    }
                }
            }

            return InstanceRef;
        }

        protected virtual void Awake()
        {
            if (InstanceRef == null)
            {
                InstanceRef = GetComponent<T>();
                gameObject.name = $"[Singleton] {typeof(T)}";

                if (IsPersistent)
                {
                    if (transform.parent)
                    {
                        Debug.Log(
                            $"Singleton '{gameObject.name}' is not a root object. Orphaning it to properly set `DontDestroyOnLoad`");
                        transform.parent = null;
                    }

                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                if (InstanceRef != GetComponent<T>())
                {
                    Debug.Log(
                        $"Singleton '{typeof(T)}' awakes, but instance is already not null - destroying this copy");
                    DestroyImmediate(this);
                }
            }
        }

        protected void OnApplicationQuit()
        {
            ApplicationIsQuitting = true;
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
            if (GetComponent<T>() == InstanceRef)
            {
                InstanceRef = null;
            }
        }
    }
}
