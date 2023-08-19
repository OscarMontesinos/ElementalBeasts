using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorminnSlow : Buff
{
    Storminn owner;
    int slow;
    // Start is called before the first frame update

    public void BuffStart(Storminn owner, int rnds, int slow)
    {
        type = BuffType.buff;
        this.owner = owner;
        rounds = rnds;
        this.slow = slow;
        unit.speedBuff += slow;
    }

    public override void End()
    {
        unit.speedBuff -= slow;
        base.End();
    }
}
