using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class currentPlayThrough
{
    static int lives = 3;
    static int score = 0;
    
    public static void Reset()
    {
        lives = 3;
        score = 0;
    }
    public static int GetLives()
    {
        return lives;
    }
    public static int GetScore()
    {
        return score;
    }
     public static bool ReduceLives()
    {
        lives--;
        if (lives == 0) return true;
        return false;
    }
    public static void IncrementScore(int levelScore)
    {
        score += levelScore;
    }
}
