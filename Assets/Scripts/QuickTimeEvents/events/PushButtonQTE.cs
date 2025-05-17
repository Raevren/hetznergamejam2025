using UnityEngine;

namespace QuickTimeEvents.events
{
    public class PushButtonQTE : QuickTimeEvent
    {
        private bool hasJumped = false;
        
        public override void Started(Transform player)
        {
            Debug.Log("PushButtonQTE: Started");
        }

        public override void OnButtonPressed(Transform player)
        {
            if(hasJumped) return;
            player.SendMessage("jump");
        }

        public override void OnButtonReleased(Transform player)
        {
        }
    }
}