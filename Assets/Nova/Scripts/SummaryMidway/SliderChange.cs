using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    public Slider slider;
    public Player player;
    public bool done;
    public Text text;

    private int score;
    private List<PlayerCantain> hightScore;
    private float valueShouldBe;

    void Start()
    {
        done = false;
        score = ScoringSystem.ReturnScoreByEnum(player);
        if (score < 0)
            score = 0;
        hightScore = ScoringSystem.FindMaxScore();
        valueShouldBe = (float)score / hightScore[0].s;
        slider.value = 0f;

	text.text = score.ToString();

        if (score == hightScore[0].s)
            ScoringSystem.ChangePlayerScore(player, 1);
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
