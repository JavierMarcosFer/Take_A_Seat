using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrogatorItemRepo : MonoBehaviour
{
    public static string[] displayName =
   {
       "Evidence?"
    };

    public static string[] fullName =
    {
        "Evidence?"
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
        "   Eyewitness report tells of a man who came out of Vincent Roy's room. Black suit, necktie, red collared shirt, about 5'8'' or taller.\n\n" +
            "   We have photographic evidence of him being at the scene. Or at least, that’s the story we’re going with anyway."
    };
    public static MemoriesInfo.repoSprites[] sprites =
    {
        MemoriesInfo.repoSprites.evidence
    };
}
