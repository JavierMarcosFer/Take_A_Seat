using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbacksRepo : MonoBehaviour
{
    public static string[] displayName =
    {
       "Third Floor", "Alibi"
    };

    public static string[] fullName =
    {
        "Third Floor", "Alibi"
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
        "   There were a lot of people rushing around. Lots of people pushing and shoving, but I didn't get why.\n\n" +
            "   I was making my way downstairs to keep an eye on the statue, but when I came up, I smelled smoke. Real strong smell, but kind of far away from the staircase.\n\n" +
            "   So I came back to check. And I came face to face with his corpse.",
        "   I went to the second floor to check out the premises for suspicious activity and to keep an eye out for the statue."
    };

    public static MemoriesInfo.repoSprites[] sprites =
    {
        MemoriesInfo.repoSprites.thirdFloor,
        MemoriesInfo.repoSprites.horse
    };
}
