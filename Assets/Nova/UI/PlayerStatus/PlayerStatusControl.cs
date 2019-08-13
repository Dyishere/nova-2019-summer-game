using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusControl : MonoBehaviour
{
    //public Image PlayerImage;
    public Slider ScoreSlider;
    public Text FeatherNum;
    public Player player;
    public Text ScoreNum;
    
    private List<PlayerCantain> HighestScore;
    private int Score;
    private int Feather;

    // Start is called before the first frame update
    void Start()
    {
        ScoreSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HighestScore = ScoringSystom.FindMaxScore();
        Score = ScoringSystom.ReturnScoreByEnum(player);
        Feather = ScoringSystom.ReturnFeathersByEnum(player);
        ScoreSlider.value = (float)Score / HighestScore[0].s;
        FeatherNum.text = " * " + Feather.ToString();
        ScoreNum.text = Score.ToString();
    }
}
