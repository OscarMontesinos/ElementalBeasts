using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarasectRecon : Buff
{
    Marasect owner;
    float regen;
    // Start is called before the first frame update

    public void BuffStart(Marasect owner, int rnds, float regen)
    {
        this.owner = owner;
        rounds = rnds;
        this.regen = regen;
    }

    public override void BuffUpdate()
    {
        unit.Heal(regen);
        base.BuffUpdate();
    }

    public override void End()
    {

        base.End();
    }
}
