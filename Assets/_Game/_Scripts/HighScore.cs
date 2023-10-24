using TMPro;
using UnityEngine;

namespace _Game._Scripts
{
    public class HighScore : MonoBehaviour
    {
        static public int score = 1000;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                score = PlayerPrefs.GetInt("HighScore");
            }
            PlayerPrefs.SetInt("HighScore", score);
        }

        // Update is called once per frame
        void Update()
        {
            TextMeshProUGUI gt = this.GetComponent<TextMeshProUGUI>();
            gt.text = "High Score: " + score;

            if(score> PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
    }
}
