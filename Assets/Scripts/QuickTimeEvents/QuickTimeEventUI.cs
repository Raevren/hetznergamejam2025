using TMPro;
using UnityEngine;

namespace QuickTimeEvents
{
    public class QuickTimeEventUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}