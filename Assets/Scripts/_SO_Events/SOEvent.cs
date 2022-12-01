using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEvents
{

    [CreateAssetMenu(fileName = "SOEvent", menuName = "SOEvent/SOEvent", order = 0)]
    public class SOEvent : ScriptableObject
    {
        public UnityEvent objectEvent = new UnityEvent();
        

        public void AddListener(UnityAction call) => objectEvent.AddListener(call);

        public void RemoveListener(UnityAction call) => objectEvent.RemoveListener(call);

        public void RemoveAllListeners() => objectEvent.RemoveAllListeners();

        public void Invoke()
        {
            objectEvent.Invoke();
        }
    }

    public class SOEvent<T> : ScriptableObject
    {
        public UnityEvent<T> objectEvent = new UnityEvent<T>();

        // Optional string that can be used to specify what the argument provided should be e.g. Players Body
        public string arg0Description;
        

        public void AddListener(UnityAction<T> call) => objectEvent.AddListener(call);

        public void RemoveListener(UnityAction<T> call) => objectEvent.RemoveListener(call);

        public void RemoveAllListeners() => objectEvent.RemoveAllListeners();

        public void Invoke(T arg)
        {
            objectEvent.Invoke(arg);
        }
    }

    public class SOEvent<T, W> : ScriptableObject
    {
        public UnityEvent<T, W> objectEvent = new UnityEvent<T, W>();
        

        // Optional strings that can be used to specify what the argument provided should be e.g. Players Body and Enemy Body
        public string arg0Description;
        public string arg1Description;

        public void AddListener(UnityAction<T, W> call) => objectEvent.AddListener(call);

        public void RemoveListener(UnityAction<T, W> call) => objectEvent.RemoveListener(call);

        public void RemoveAllListeners() => objectEvent.RemoveAllListeners();

        public void Invoke(T arg0, W arg1)
        {
            objectEvent.Invoke(arg0, arg1);
        }
    }

    public class SOEvent<T, V, W> : ScriptableObject
    {
        public UnityEvent<T, V, W> objectEvent = new UnityEvent<T, V, W>();
        

        // Optional strings that can be used to specify what the argument provided should be e.g. Players Body, Enemy Body and Current Time.
        public string arg0Description;
        public string arg1Description;
        public string arg2Description;

        public void AddListener(UnityAction<T, V, W> call) => objectEvent.AddListener(call);

        public void RemoveListener(UnityAction<T, V, W> call) => objectEvent.RemoveListener(call);

        public void RemoveAllListeners() => objectEvent.RemoveAllListeners();

        public void Invoke(T arg0, V arg1, W arg2)
        {
            objectEvent.Invoke(arg0, arg1, arg2);
        }
    }
}
