using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeInhib : Buff
{
    Obe owner;
    int value;
    // Start is called before the first frame update

    public void BuffStart(Obe owner, int rnds, int value)
    {
        type = BuffType.debuff;
        this.owner = owner;
        rounds = rnds;
        this.value = value;
        unit.desorientarValue += value;
    }

    public override void BuffUpdate()
    {
        base.BuffUpdate();
    }

    public override void End()
    {
        unit.desorientarValue -= value;
        base.End();
    }
}
