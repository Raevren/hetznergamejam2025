using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuickTimeEvents
{
    public abstract class QuickTimeEvent : MonoBehaviour
    {
        [SerializeField] private Key key;
        [SerializeField] private QuickTimeEventManager eventManager;
        private QuickTimeEventUI _eventUI;

        public Key Key => key;
        
        public event Action OnFailed;
        public event Action OnCompleted;

        public abstract void Started(Transform player);
        public abstract void OnButtonPressed(Transform player);
        
        public abstract void OnButtonReleased(Transform player);

        private void Start()
        {
            _eventUI = GetComponentInChildren<QuickTimeEventUI>();
            _eventUI.gameObject.SetActive(false);
            _eventUI.SetText(key.ToString());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("LevelGenerator")) return;
            
            eventManager.StartEvent(this);
            _eventUI.gameObject.SetActive(true);
        }

        private void End()
        {
            _eventUI.gameObject.SetActive(false);
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