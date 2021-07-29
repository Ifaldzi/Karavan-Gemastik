using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    Text levelText;

    [SerializeField]
    Text totalScoreText;

    public void setExpValue(int exp)
    {
        slider.value = exp;
    }

    public void setMaxExp(int maxExp)
    {
        slider.maxValue = maxExp;
    }

    public void setLevel(int level)
    {
        levelText.text = string.Format("Lv. {0}", level);
    }

    public void SetTotalScore(int score)
    {
        totalScoreText.text = score.ToString("#,###0");
    }
}
