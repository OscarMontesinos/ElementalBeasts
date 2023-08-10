using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarasectVenom : Buff
{
    Marasect owner;
    float dmg;
    // Start is called before the first frame update

    public void BuffStart(Marasect owner, int rnds, float dmg)
    {
        this.owner = owner;
        rounds = rnds;
        this.dmg = dmg;
    }

    public override void BuffUpdate()
    {
        unit.RecibirDanoMagico(dmg);
        base.BuffUpdate();
    }

    public override void End()
    {

        base.End();
    }
}
