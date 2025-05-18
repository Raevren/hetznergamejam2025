using TMPro;
using UnityEngine;

public class GamoverScoreDisplay : MonoBehaviour
{
    void Start()
    {
        var score = PlayerPrefs.GetFloat("score");
        var tmp = GetComponent<TMP_Text>();
        tmp.text =  "SCORE: " + (Mathf.Round(score * 100) / 100) + "M";
    }

}
