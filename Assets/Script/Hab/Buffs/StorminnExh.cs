using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StorminnExh : Buff
{
    Storminn owner;
    float exh;
    // Start is called before the first frame update

    public void BuffStart(Storminn owner, int rnds, float exh)
    {
        type = BuffType.buff;
        this.owner = owner;
        rounds = rnds;
        this.exh = exh;
        unit.pot -= exh;
    }

    public override void End()
    {
        unit.pot += exh;
        base.End();
    }
}
