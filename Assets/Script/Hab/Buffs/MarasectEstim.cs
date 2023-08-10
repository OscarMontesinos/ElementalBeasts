using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarasectEstim : Buff
{
    Marasect owner;
    float pot;
    int cel;
    // Start is called before the first frame update

    public void BuffStart(Marasect owner, int rnds, float pot, int cel)
    {
        this.owner = owner;
        rounds = rnds;
        this.pot = pot;
        this.cel = cel;
        unit.pot += pot;
        unit.speedBuff += cel;
    }

    public override void End()
    {
        unit.pot -= pot;
        unit.speedBuff -= cel;
        base.End();
    }
}
