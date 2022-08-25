using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSilent : DialogueParent
{
    public new static string[] lines =
    {
        "-...:Todd",

        "-0...:Interrogator",
        "-1[Compromised-p14-006.wav]Um...",
        "+[Denouement-p17-001.wav]You’ll probably be taken in later for further questioning, but for now, you're free to go.",

        "-[Denouement-p17-002.wav]He looks rather annoyed with me as he shows me out, shoving me as I go.: ",

        "-[Denouement-p17-003.wav]But don’t think for a second that we’re done with you.:Interrogator",

        "-[Denouement-p17-004.wav]With that, I walk out of the room. It feels rather anticlimactic, but at the very least I’m okay.: "
    };
}
