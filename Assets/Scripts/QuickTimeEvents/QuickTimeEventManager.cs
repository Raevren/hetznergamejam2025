using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuickTimeEvents
{
    public class QuickTimeEventManager : MonoBehaviour
    {
        private QuickTimeEvent _currentEvent;
        
        public bool IsActive => _currentEvent != null;

        public void OnQuickTimeAction(InputAction.CallbackContext context)
        {
            if (!_currentEvent.Key.DisplayName().Equals(context.control.displayName)) return;
            
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
            //TODO display qte overlay
            
            _currentEvent.Started(transform);
        }

        private void StopQuickTimeEvent()
        {
            _currentEvent.OnCompleted -= RewardQuickTimeEvent;
            _currentEvent.OnFailed -= PunishQuickTimeEvent;
        }

        private void RewardQuickTimeEvent()
        {
            
        }

        private void PunishQuickTimeEvent()
        {
            
        }
    }
}