using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    
    public static string isMusicOn = "isMusicOn";

    public static int GetMusicState(){
        return PlayerPrefs.GetInt(Game.isMusicOn);
    }

    public static void SetMusicState(int state){
        PlayerPrefs.SetInt(Game.isMusicOn, state);
    }
}
