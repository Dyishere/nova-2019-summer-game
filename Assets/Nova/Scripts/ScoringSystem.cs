using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoringSystem
{
    
    private static int[] _playerScore = { 0, 0, 0, 0};
    private static int[] _playerFeathers = { 0, 0, 0, 0};

    public static PlayerChar[] PlayerShow = {
        new PlayerChar(false, Character.b1),
        new PlayerChar(false, Character.b1),
        new PlayerChar(false, Character.b1),
        new PlayerChar(false, Character.b1)
    };
    
    // Score in every level
    public static int P1Score
    {
        get { return _playerScore[(int)Player.p1]; }
    }
    public static int P2Score
    {
        get { return _playerScore[(int)Player.p2]; }
    }
    public static int P3Score
    {
        get { return _playerScore[(int)Player.p3]; }
    }
    public static int P4Score
    {
        get { return _playerScore[(int)Player.p4]; }
    }
   
    // Players' feathers in whole game 
    public static int P1Feathers
    {
        get { return _playerFeathers[(int)Player.p1]; }
    }
    public static int P2Feathers
    {
        get { return _playerFeathers[(int)Player.p2]; }
    }
    public static int P3Feathers
    {
        get { return _playerFeathers[(int)Player.p3]; }
    }
    public static int P4Feathers
    {
        get { return _playerFeathers[(int)Player.p4]; }
    }

    /// <summary>
    /// 将全部分数归零
    /// </summary>
    public static void ResetScore()
    {
        for (var i = Player.p1; i <= Player.p4; i++)
            _playerScore[(int)i] = 0;
    }

    /// <summary>
    /// 将全部羽毛数量归零
    /// </summary>
    public static void ResetFeathers()
    {
        for (var i = Player.p1; i <= Player.p4; i++)
            _playerFeathers[(int)i] = 0;
    }

    /// <summary>
    /// 初始化，分数、羽毛归零；选角色与控制器归零（输入相关）。
    /// </summary>
    public static void Init()
    {
        ResetFeathers();
        ResetScore();
        //输入相关
        ResetInput();
    }

    /// <summary>
    /// 改变分数的方法
    /// </summary>
    /// <param name="player">要改变分数的玩家枚举</param>
    /// <param name="change">分数变化量</param>
    public static void ChangePlayerScore(Player player, int change)
    {
        _playerScore[(int)player] += change;
    }

    /// <summary>
    /// 改变羽毛shuliang
    /// </summary>
    /// <param name="player">玩家枚举</param>
    /// <param name="change">变化量</param>
    public static void ChangePlayerFeather(Player player, int change)
    {
        _playerFeathers[(int)player] += change;
    }

    /// <summary>
    /// 通过玩家枚举返回分数
    /// </summary>
    /// <param name="p">玩家枚举</param>
    /// <returns>(int)玩家当前分数</returns>
    public static int ReturnScoreByEnum(Player p)
    {
        return _playerScore[(int)p];
    }

    /// <summary>
    /// 通过玩家枚举返回羽毛数
    /// </summary>
    /// <param name="p">玩家枚举</param>
    /// <returns>(int)玩家当前羽毛数</returns>
    public static int ReturnFeathersByEnum(Player p)
    {
        return _playerFeathers[(int)p];
    }

    /// <summary>
    /// 返回所有最高分角色的信息
    /// </summary>
    /// <returns> List<PlayerCantain> 
    /// PlayerCantain
    /// {
    ///     public Player p;
    ///     public int s;
    ///     public int f;
    /// }
    /// </returns>
    public static List<PlayerCantain> FindMaxScore()
    {
        List<PlayerCantain> l = new List<PlayerCantain>();
        var s = SortByScore(ToList());
        l.Add(s[0]);
        for (int i = 1; i < 4; i++)
        {
            if (s[i].s == s[0].s)
                l.Add(s[i]);
        }

        return l;
    }

    /// <summary>
    /// 返回所有羽毛最多的角色信息
    /// </summary>
    /// <returns>List<PlayerCantain></returns>
    public static List<PlayerCantain> FindMaxFeather()
    {
        List<PlayerCantain> l = new List<PlayerCantain>();
        var s = SortByFeathers(ToList());
        l.Add(s[0]);
        for (int i = 1; i < 4; i++)
        {
            if (s[i].f == s[0].f)
                l.Add(s[i]);
        }

        return l;
    }

    /// <summary>
    /// 返回排序后的角色分数dict
    /// </summary>
    /// <returns></returns>
    public static Dictionary<Player, int> SortedScoreDict()
    {
        var l = SortByScore(ToList());
        Dictionary<Player, int> d = new Dictionary<Player, int>();
        for (int i = 0; i < 4; i++)
            d.Add(l[i].p, l[i].s);

        return d;
    }

    // 辅助方法
    private static List<PlayerCantain> ToList()
    {
        List<PlayerCantain> l = new List<PlayerCantain>();
        for (var i = Player.p1; i <= Player.p4; i++)
            l.Add(new PlayerCantain(i, _playerScore[(int)i], _playerFeathers[(int)i]));
        return l;
    }

    private static List<PlayerCantain> SortByScore(List<PlayerCantain> l)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                if (l[i].s < l[j].s)
                {
                    var temp = l[i];
                    l[i] = l[j];
                    l[j] = temp;
                }
            }
        }

        return l;
    }

    private static List<PlayerCantain> SortByFeathers(List<PlayerCantain> l)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                if (l[i].f < l[j].f)
                {
                    var temp = l[i];
                    l[i] = l[j];
                    l[j] = temp;
                }
            }
        }

        return l;
    }

    //[以下输入相关]
    public static bool[] CharaSelected = new bool [4];
    public static bool[] PlayerJoin = new bool[5];
    public static ControllerInfo[] PlayerInpuController =
    {
        new ControllerInfo(Player.p1,Character.b0,"null"),
        new ControllerInfo(Player.p2,Character.b0,"null"),
        new ControllerInfo(Player.p3,Character.b0,"null"),
        new ControllerInfo(Player.p4,Character.b0,"null")
    };

    public static bool CheckCharaJoin(Character c)
    {
        for (var i = Player.p1; i <= Player.p4; i++)
            if (PlayerInpuController[(int)i].iCharaNum == c)
                return true;
        return false;
    }

    public static bool CheckControllerJoin(string c)
    {
        for (var i = Player.p1; i <= Player.p4; i++)
            if (PlayerInpuController[(int)i].iController == c)
                return true;
        return false;
    }

    public static string FindControllerByChara(Character c)
    {
        for (var i = Player.p1; i <= Player.p4; i++)
            if (PlayerInpuController[(int)i].iCharaNum == c)
                return PlayerInpuController[(int)i].iController;
        return "null";
    }

    public static int FindPlayerByChara(Character c)
    {
        for (var i = Player.p1; i <= Player.p4; i++)
            if (PlayerInpuController[(int)i].iCharaNum == c)
                return (int)i;
        return 5;
    }

    public static void ResetInput()
    {
        ResetCharaSelected();
        ResetPlayerController();
        ResetPlayerJoin();
    }

    private static void ResetCharaSelected()
    {
        for (var i = Player.p1; i <= Player.p4; i++)
        {
            CharaSelected[(int)i] = false;
            PlayerInpuController[(int)i].iCharaNum = Character.b0;
        }
    }

    private static void ResetPlayerJoin()
    {
        PlayerJoin[0] = true;
        for (var i = Player.p1; i <= Player.p4; i++)
            PlayerJoin[(int)i + 1] = false;
    }

    private static void ResetPlayerController()
    {
        for (var i = Player.p1; i <= Player.p4; i++)
            PlayerInpuController[(int)i].iController = "null";
    }
}

public struct PlayerCantain
{
    public PlayerCantain(Player _p, int _s, int _f)
    {
        p = _p;
        s = _s;
        f = _f;
    }

    public Player p;
    public int s;
    public int f;
}

public struct PlayerChar
{
    public PlayerChar(bool _show, Character _character)
    {
        Show = _show;
        character = _character;
    }
    public bool Show;
    public Character character;
}

public enum Player
{
    p1, p2, p3, p4,
}

public enum Character
{
    b1, b2, b3, b4, b0,
}

//[输入相关]
public struct ControllerInfo
{
    public Player iPlayerNum;
    public Character iCharaNum;
    public string iController;

    public ControllerInfo(Player pn, Character cn, string c)
    {
        iPlayerNum = pn;
        iCharaNum = cn;
        iController = c;
    }
}
