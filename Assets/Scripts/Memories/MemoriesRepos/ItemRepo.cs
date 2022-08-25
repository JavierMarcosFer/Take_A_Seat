using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRepo : MonoBehaviour
{
    public static string[] displayName =
    {
       "Brooch"
    };

    public static string[] fullName =
    {
        "Brooch"
    };

    public static string[] genderAndAge =
    {
        ""
    };

    public static string[] occupation =
    {
        ""
    };

    public static string[] description =
    {
        "   Before Roy and I began the mission, we both put on tracking devices to keep tabs on each other’s location.\n\n" +
            "   I know I kept mine on the entire night. But I can’t remember if Roy had his or not…"
    };

    public static MemoriesInfo.repoSprites[] sprites =
    {
        MemoriesInfo.repoSprites.brooch
    };
}
