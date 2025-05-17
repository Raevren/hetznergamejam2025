using System;
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
            hasJumped = true;
            player.SendMessage("jump");
        }

        public void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("LevelGenerator")) return;

            if (hasJumped)
            {
                OnComplete();
            }
            else
            {
                OnFail();
            }
        }

        public override void OnButtonReleased(Transform player)
        {
        }
    }
}