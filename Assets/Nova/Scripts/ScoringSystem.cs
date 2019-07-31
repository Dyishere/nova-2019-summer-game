using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoringSystom
{
    private static int _p1Score = 0;
    private static int _p2Score = 0;
    private static int _p3Score = 0;
    private static int _p4Score = 0;
    private static int _p1Feathers = 0;
    private static int _p2Feathers = 0;
    private static int _p3Feathers = 0;
    private static int _p4Feathers = 0;

    // Score in every level
    public static int P1Score
    {
        get { return _p1Score; }
    }
    public static int P2Score
    {
        get { return _p2Score; }
    }
    public static int P3Score
    {
        get { return _p3Score; }
    }
    public static int P4Score
    {
        get { return _p4Score; }
    }
   
    // Players' feathers in whole game 
    public static int P1Feathers
    {
        get { return _p1Feathers; }
    }
    public static int P2Feathers
    {
        get { return _p2Feathers; }
    }
    public static int P3Feathers
    {
        get { return _p3Feathers; }
    }
    public static int P4Feathers
    {
        get { return _p4Feathers; }
    }

    public static void ResetScore()
    {
        ChangePlayerScore(Player.p1, (0 - P1Score));
        ChangePlayerScore(Player.p2, (0 - P2Score));
        ChangePlayerScore(Player.p3, (0 - P3Score));
        ChangePlayerScore(Player.p4, (0 - P4Score));
    }

    public static void ResetFeathers()
    {
        ChangePlayerFeather(Player.p1, (0 - P1Feathers));
        ChangePlayerFeather(Player.p2, (0 - P2Feathers));
        ChangePlayerFeather(Player.p3, (0 - P3Feathers));
        ChangePlayerFeather(Player.p4, (0 - P4Feathers));
    }

    public static void ChangePlayerScore(Player player, int change)
    {
        switch (player)
        {
            case Player.p1:
                _p1Score += change;
                break;
            case Player.p2:
                _p2Score += change;
                break;
            case Player.p3:
                _p3Score += change;
                break;
            case Player.p4:
                _p4Score += change;
                break;
        }
    }

    public static void ChangePlayerFeather(Player player, int change)
    {
        switch (player)
        {
            case Player.p1:
                _p1Feathers += change;
                break;
            case Player.p2:
                _p2Feathers += change;
                break;
            case Player.p3:
                _p3Feathers += change;
                break;
            case Player.p4:
                _p4Feathers += change;
                break;
        }
    }
}

public enum Player
{
    p1, p2, p3, p4
}