using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuickTimeEvents
{
    public class QuickTimeEventUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image loader;

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void EnableProgress()
        {
            loader.transform.parent.gameObject.SetActive(true);
            loader.fillAmount = 0;
        }

        public void DisableProgress()
        {
            loader.transform.parent.gameObject.SetActive(false);
        }
        
        public void SetProgress(float progress) { loader.fillAmount = progress; }
    }
}