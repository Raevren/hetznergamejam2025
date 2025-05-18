using System;
using UnityEngine;
using UnityEngine.UI;

namespace QuickTimeEvents.events
{
    public class HoldButtonQTE : QuickTimeEvent
    {
        [SerializeField] private float holdTime;

        private SpriteRenderer playerSprite;

        private float startPressed = 0;

        private float ElepsedTime => Time.time - startPressed;
        
        private bool IsSuccesfully => ElepsedTime >= holdTime;
        
        public float Progress => startPressed == 0 ? 0 : ElepsedTime / holdTime;
        
        public override void Started(Transform player)
        {
            Debug.Log("HoldButtonQTE: Started");
            playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        }

        public override void End()
        {
            base.End();
            playerSprite.transform.localScale = Vector3.one;
        }

        public override void OnButtonPressed(Transform player)
        {
            playerSprite.transform.localScale = new Vector3(1, 0.3f, 1);
            startPressed = Time.time;
            Debug.Log("HoldButtonQTE: OnButtonPressed");
        }

        public override void OnButtonReleased(Transform player)
        {
            Debug.Log("HoldButtonQTE: OnButtonReleased");
            playerSprite.transform.localScale = Vector3.one;
            if(Complete) return;
            Debug.Log("HoldButtonQTE: OnButtonReleased Not comp");
            if (IsSuccesfully)
            {
                Debug.Log("HoldButtonQTE: OnButtonReleased Successfully");
                OnComplete();
            }
            
            startPressed = 0;
        }

        private void Update()
        {
            if(startPressed == 0) return;
            if(Complete) return;
            if (IsSuccesfully)
            {
                Debug.Log("HoldButtonQTE: Update complete");
                OnComplete();
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if(startPressed == 0) return;
            if (!other.gameObject.CompareTag("LevelGenerator")) return;
            Debug.Log("HoldButtonQTE: OnTriggerExit");
            if(Complete) return;

            Debug.Log("HoldButtonQTE: OnTriggerExit Not comp");
            if (IsSuccesfully)
            {
                Debug.Log("HoldButtonQTE: OnTriggerExit Successfully");
                OnComplete();
            }
            else
            {
                Debug.Log("HoldButtonQTE: OnTriggerExit Not comp " + ElepsedTime);
                OnFail();
            }
        }
    }
}