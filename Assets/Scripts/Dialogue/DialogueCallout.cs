using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCallout : DialogueParent
{
    public new static string[] lines =
    {
        "-0[The Night Roy Died-p10-004.wav]Even though Roy was your friend.:Interrogator",

        "-[The Night Roy Died-p10-005.wav]I didn’t know my friend died. He didn’t shoot me any message saying if he was alright. I had just assumed that he’d left already.:Todd",

        "-[The Night Roy Died-p10-006.wav]If you really knew anything about your friend, you’d know that he’s not as much of a yellow belly as you seem to be.:Interrogator",

        "-[The Night Roy Died-p10-007.wav]For knowing when to quit?:Todd",

        "-1[The Night Roy Died-p10-008.wav]For bailing out on your partner.:Interrogator",

        "-...: ",
        "+[The Night Roy Died-p10-009.wav]Not good.",

        "-0[The Night Roy Died-p10-010.wav]But speaking of call…:Investigator"
    };
}
