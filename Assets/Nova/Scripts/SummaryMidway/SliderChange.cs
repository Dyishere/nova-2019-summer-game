using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    public Slider slider;
    public Player player;
    public bool done;

    private int score;
    private List<PlayerCantain> hightScore;
    private float valueShouldBe;

    void Start()
    {
        done = false;
        score = ScoringSystom.ReturnScoreByEnum(player);
        if (score < 0)
            score = 0;
        hightScore = ScoringSystom.FindMaxScore();
        valueShouldBe = (float)score / hightScore[0].s;
        slider.value = 0f;
    }

    void Update()
    {
        SliderValueChange();
    }

    void SliderValueChange()
    {
        if (slider.value < valueShouldBe && !done)
            slider.value += 0.01f;
        else
            done = true;
    }
}
