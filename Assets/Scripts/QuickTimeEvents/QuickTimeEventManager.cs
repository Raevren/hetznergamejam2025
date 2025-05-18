using System;
using QuickTimeEvents.events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace QuickTimeEvents
{
    public class QuickTimeEventManager : MonoBehaviour
    {
        private QuickTimeEvent _currentEvent;
        private QuickTimeEventUI _uiButton;
        private PlayerHealth _playerHealth;
        public bool IsActive => _currentEvent != null;

        private void Start()
        {
            _uiButton = GetComponentInChildren<QuickTimeEventUI>();
            _playerHealth = GetComponent<PlayerHealth>();
            _uiButton.gameObject.SetActive(false);

            if (_currentEvent is HoldButtonQTE)
            {
                _uiButton.EnableProgress();
            }
        }

        private void Update()
        {
            if (_currentEvent is HoldButtonQTE qte)
            {
                _uiButton.SetProgress(qte.Progress);
            }
        }
        
        public void OnQuickTimeAction(InputAction.CallbackContext context)
        {
            var keyControl = context.control as KeyControl;
            
            if(_currentEvent == null || keyControl == null) return;
            if(_currentEvent.Key != keyControl.keyCode) return;
            
            //TODO animate displayed button
            
            if (context.started)
            {
                _currentEvent.OnButtonPressed(transform);
            }

            if (context.canceled)
            {
                _currentEvent.OnButtonReleased(transform);
            }
        }
        
        public void StartEvent(QuickTimeEvent quickTimeEvent)
        {
            if (IsActive)
            {
                throw new System.Exception("QuickTimeEvent is already active");
            }
            
            _currentEvent = quickTimeEvent;
            _currentEvent.OnCompleted += RewardQuickTimeEvent;
            _currentEvent.OnFailed += PunishQuickTimeEvent;
            
            _uiButton.SetText(_currentEvent.Key.ToString());
            _uiButton.gameObject.SetActive(true);
            
            _currentEvent.Started(transform);
        }

        private void StopQuickTimeEvent()
        {
            _uiButton.gameObject.SetActive(false);
            _uiButton.DisableProgress();
            _currentEvent.OnCompleted -= RewardQuickTimeEvent;
            _currentEvent.OnFailed -= PunishQuickTimeEvent;
            _currentEvent = null;
        }

        private void RewardQuickTimeEvent()
        {
            StopQuickTimeEvent();
            Debug.Log("reward quicktime event");
        }

        private void PunishQuickTimeEvent()
        {
            StopQuickTimeEvent();
            _playerHealth.ReduceLive();
            Debug.Log("punish quicktime event");
        }
    }
}