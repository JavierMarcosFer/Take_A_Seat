using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleRepo : MonoBehaviour
{
    public static string[] displayName =
    {
       "Roy"
    };

    public static string[] fullName =
    {
        "Vincent Roy"
    };

    public static string[] genderAndAge =
    {
        "Male, Age 32 (Deceased)"
    };

    public static string[] occupation =
    {
        "Secret Agent"
    };

    public static string[] description =
    {
        "   He was my partner. He made a lot of mistakes, and seemed to get in more trouble than he started, but whatever the case, he didn’t deserve to die.\n\n" +
            "   He was supposed to help me crack the code hidden in The Shade’s new statue. I’m not sure why it was so important. What I don’t get is why he left his door open. He wasn’t supposed to open it for anybody."
    };

    public static MemoriesInfo.repoSprites[] sprites =
    {
        MemoriesInfo.repoSprites.harding
    };
}
