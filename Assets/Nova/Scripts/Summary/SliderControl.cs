using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider slider;
    public Player player;
    public bool done;
    public bool isWinner;

    private int feather;
    private List<PlayerCantain> hightFeather;
    private float valueShouldBe;

    void Start()
    {
        isWinner = false;
        done = false;
        feather = ScoringSystem.ReturnFeathersByEnum(player);
        if (feather < 0)
            feather = 0;
        hightFeather = ScoringSystem.FindMaxFeather();
        valueShouldBe = (float)feather / hightFeather[0].s;
        slider.value = 0f;
        if (feather == hightFeather[0].f)
            isWinner = true;
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
