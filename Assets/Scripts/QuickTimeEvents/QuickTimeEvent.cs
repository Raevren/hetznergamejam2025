using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuickTimeEvents
{
    public abstract class QuickTimeEvent
    {
        public Key Key { get; private set; }
        
        public QuickTimeEvent(Key key)
        {
            Key = key;
        }
        
        public event Action OnFailed;
        public event Action OnCompleted;

        public abstract void Started(Transform player);
        public abstract void OnButtonPressed(Transform player);
        
        public abstract void OnButtonReleased(Transform player);
        
        protected void OnFail()
        {
            OnFailed?.Invoke();
        }

        protected void OnComplete()
        {
            OnCompleted?.Invoke();
        }
    }
}