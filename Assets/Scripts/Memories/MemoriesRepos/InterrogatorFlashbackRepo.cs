using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrogatorFlashbackRepo : MonoBehaviour
{
    public static string[] displayName =
   {
       "Code", "Stage"
    };

    public static string[] fullName =
    {
        "Code", "Stage"
    };

    public static string[] genderAndAge =
    {
        "", ""
    };

    public static string[] occupation =
    {
        "", ""
    };

    public static string[] description =
    {
        "   There were quite a few people interested in getting to The Shade’s horse statue.\n\n" +
            "   I’m told that someone had hidden something important in it, but I don’t know what that is. In any case, I need Harding to talk to me about it.",
        "   Harding’s not smart enough to notice the message Roy sent to him wasn’t from him at all. Who would even tell someone they’re straight up “uncompromised”?\n\n" +
            "   If he can answer to it, whether he denies it or not, he’ll reveal himself as Roy’s partner. But if he genuinely doesn’t know then it’ll all be for nothing."
    };

    public static MemoriesInfo.repoSprites[] sprites =
    {
        MemoriesInfo.repoSprites.horse,
        MemoriesInfo.repoSprites.evidence
    };
}
