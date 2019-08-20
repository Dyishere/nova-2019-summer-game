using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private enum Gamemode
    {
        Platform ,
        Skyland,
        Dungeon
    }

    [Header("场景名称")]
    [SerializeField] private string PlatformScenes = "Scenes_1";
    [SerializeField] private string SkylandScenes = "Scenes_2";
    [SerializeField] private string DungeonScenes = "Scenes_3";

    [Header("场景时间设置")]
    [SerializeField] private int PlatformTime = 210;
    [SerializeField] private int SkylandTime = 300;
    [SerializeField] private int DungeonTime = 300;

    [Header("UI时间显示")]
    [SerializeField] private Text TimeCountdownText;

    [Header("结算场景")]
    [SerializeField] private string NextScenes = "MidwaySummary";

    [Header("减分设置")]
    [SerializeField]private int ReduceScore = 20 ;

    private int _NowTime;
    private int _TotalGameTime;
    private bool IsPause = false;
    private Gamemode NowGamemode;
    private string CheckMethod;
    private float CheckTime;


    private GameObject[] players = new GameObject[4];
    private int RemainPlayer;
    private Dictionary<Player,int> PlayerRankDict;

    private void Awake()
    {
        InitGamemode();
        InitTime();
        InitCheck();
    }

    private void Start()
    {
        StartCoroutine("StartTimeCountdown");
        InvokeRepeating(CheckMethod, 0f, CheckTime);
    }

    private void InitTime()
    {
        SetTime();
        //Debug.Log(_TotalGameTime);
        _NowTime = _TotalGameTime;
        TimeCountdownText.text = FormTimeDisplay(_TotalGameTime);
    }


    private IEnumerator StartTimeCountdown()
    {
        while (_NowTime >= 0)
        {
            yield return new WaitForSeconds(1);
            _NowTime--;
            TimeCountdownText.text = FormTimeDisplay(_NowTime);
            if (_NowTime <= 0)
            {
                SceneManager.LoadScene(NextScenes);
            }
        }
    }


    private string FormTimeDisplay(int NowTime)
    {
        string TimeDispaly;
        int Minutes;
        int Seconds;
        Minutes = NowTime / 60;
        Seconds = NowTime % 60;
        //Debug.Log(Minutes);
        //Debug.Log(Seconds);
        if (_NowTime>=0)
        {
            TimeDispaly = string.Format("{0:D2}"+":"+"{1:D2}",Minutes,Seconds);
        }
        else
        {
            TimeDispaly = "00:00";
        }
        return TimeDispaly;
    }

    private void SetTime()
    {
        switch (NowGamemode)
        {
            case Gamemode.Platform:
                _TotalGameTime = PlatformTime;
                break;
            case Gamemode.Skyland:
                _TotalGameTime = SkylandTime;
                break;
            case Gamemode.Dungeon:
                _TotalGameTime = DungeonTime;
                break;
            default:
                Debug.LogError("未知的游戏模式");
                break;
        }
    }

    void InitCheck()
    {
        if ((NowGamemode == Gamemode.Dungeon)||(NowGamemode == Gamemode.Skyland))
        {
            initPlayerList();
        }
        switch (NowGamemode)
        {
            case Gamemode.Platform:
                CheckMethod = "CheckRank";
                CheckTime = 60f;
                break;
            case Gamemode.Skyland:
                CheckMethod = "CheckSkyLandPlayer";
                CheckTime = 1f;
                break;
            case Gamemode.Dungeon:
                CheckMethod = "CheckDungeonPlayer";
                CheckTime = 1f;
                break;
            default:
                Debug.LogError("未知的游戏模式");
                break;
        }
    }


    //</summary>
    //当为平台乱斗时执行ChceckRank计分
    //<\summary>
    void CheckRank()
    {
        PlayerRankDict = ScoringSystem.SortedScoreDict();
        foreach (Player p in PlayerRankDict.Keys)
        {
            if (PlayerRankDict[p] == 1)
            {
                ScoringSystem.ChangePlayerScore(p, ReduceScore);
            }
        }
        Debug.Log("RankChecked");
    }

    void initPlayerList()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        RemainPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        Debug.Log(RemainPlayer);
    }

    void CheckSkyLandPlayer()
    {
        foreach (var p in players)
        {
            if (p.activeInHierarchy == false)
            {
                RemainPlayer--;
            }
        }
        if (RemainPlayer <= 1) SceneManager.LoadScene(NextScenes);
        Debug.Log("RemainPlayer" + RemainPlayer);
    }

    void CheckDungeonPlayer()
    {
        foreach (var p in players)
        {
            if (p.activeInHierarchy == false)
            {
                RemainPlayer--;
            }
        }
        if (RemainPlayer <= 0) SceneManager.LoadScene(NextScenes);
        Debug.Log("RemainPlayer" + RemainPlayer);
    }

    void InitGamemode()
    {
        string ThisScenes = SceneManager.GetActiveScene().name;
        if (ThisScenes == PlatformScenes)
        {
            NowGamemode = Gamemode.Platform ;
        }
        else if (ThisScenes == SkylandScenes)
        {
            NowGamemode = Gamemode.Skyland;
        }
        else if (ThisScenes == DungeonScenes)
        {
            NowGamemode = Gamemode.Dungeon;
        }
        else
        {
            Debug.LogError("未知的游戏模式");
        }
    }
}
