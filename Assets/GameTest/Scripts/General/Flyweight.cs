using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Flyweight
{
    public const string DIALOG_CHAR_HALF = "half";
    public const string DIALOG_CHAR_HEAD = "head";
    public const string DIALOG_CHAR_STONE = "stone";

    public const string EMOTE_HALF_NORMAL = "/emote:Normal/";
    public const string EMOTE_HALF_SAD = "/emote:Sad/";
    public const string EMOTE_HALF_HAPPY = "/emote:Happy/";
    public const string EMOTE_HALF_THINKING = "/emote:Thinking/";
    public const string EMOTE_HALF_SCARED = "/emote:Scared/";

    public const string EMOTE_HEAD_NORMAL = "/emote:Normal/";
    public const string EMOTE_HEAD_SAD = "/emote:Sad/";
    public const string EMOTE_HEAD_HAPPY = "/emote:Happy/";
    public const string EMOTE_HEAD_THINK = "/emote:Thinking/";
    public const string EMOTE_HEAD_SHY = "/emote:Shy/";
    public const string EMOTE_HEAD_LAUGH = "/emote:Laugh/";
    public const string EMOTE_HEAD_SCARED = "/emote:Scared/";
    public const string EMOTE_HEAD_ANGRY = "/emote:Angry/";

    public static readonly int[] ROOMS_CREATION_EASY = { 5, 1, 0 };
    public static readonly int[] ROOMS_CREATION_NORMAL = { 3, 4, 1 };
    public static readonly int[] ROOMS_CREATION_HARD = { 0, 6, 6 };

    public const int RIDDLE_AMOUNT = 6;
}
