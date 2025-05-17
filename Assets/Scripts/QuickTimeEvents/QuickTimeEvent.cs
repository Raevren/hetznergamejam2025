using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuickTimeEvents
{
    public abstract class QuickTimeEvent : MonoBehaviour
    {
        [SerializeField] private Key key;
        
        private QuickTimeEventManager eventManager;
        
        public Key Key => key;
        
        public event Action OnFailed;
        public event Action OnCompleted;

        public abstract void Started(Transform player);
        public abstract void OnButtonPressed(Transform player);
        
        public abstract void OnButtonReleased(Transform player);
        
        public bool Complete { get; set;}

        private void Start()
        {
            eventManager = FindFirstObjectByType<QuickTimeEventManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("LevelGenerator")) return;
            
            eventManager.StartEvent(this);
        }

        public virtual void End()
        {
            Complete = true;
        }
        
        protected void OnFail()
        {
            End();
            OnFailed?.Invoke();
        }

        protected void OnComplete()
        {
            End();
            OnCompleted?.Invoke();
        }
    }
}