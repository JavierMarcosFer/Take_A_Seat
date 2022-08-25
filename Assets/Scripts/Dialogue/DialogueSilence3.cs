using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSilence3 : DialogueParent
{
    public new static string[] lines =
    {
        "-...:Todd",

        "-[FILE]Goddamit, Todd, talk to me!:Interrogator",
        "+[FILE]Don't you care about any of this? at all?",
        "+[FILE]Are you really going to withhold this information from us!?",

        "-[FILE]He seems quite flustered. He might be about to do something, or maybe not: ",
        "-[FILE]Either way, this is probably my last chance to change my mind about this"
    };
}
