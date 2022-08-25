using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrogatorPeopleRepo : MonoBehaviour
{
    public static string[] displayName =
   {
       "Victim", "Harding"
    };

    public static string[] fullName =
    {
        "Vincent Roy", "Todd Harding"
    };

    public static string[] genderAndAge =
    {
        "Male, Age 32 (Deceased)", "Male, Age 27"
    };

    public static string[] occupation =
    {
        "Public Figure", "Landscaping"
    };

    public static string[] description =
    {
        "   No close relatives, no notable acquaintances.\n\n" +
            "   Multiple bruises found on his body. He was found dead at the scene of the crime before police came in. The perpetrator escaped.",
        "   Current target for interrogation.\n\n" +
            "   Should treat with caution. I do not know a lot about him, but he seems like he may know Roy more than he lets on. I need to get information out of him."
    };

    public static MemoriesInfo.repoSprites[] sprites =
    {
        MemoriesInfo.repoSprites.harding,
        MemoriesInfo.repoSprites.harding
    };
}
