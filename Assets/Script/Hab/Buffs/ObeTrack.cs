using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeTrack : Buff
{
    Obe owner;
    float mark;
    // Start is called before the first frame update

    private void Update()
    {
        if (owner == null)
        {
            End();
        }
    }
    public void BuffStart(Obe owner, float mark)
    {
        type = BuffType.debuff;
        this.owner = owner;
        rounds = 1000;
        this.mark = mark;
        unit.dmgMultiplier += mark;
    }

    public override void BuffUpdate()
    {
        base.BuffUpdate();
    }

    public override void End()
    {
        unit.dmgMultiplier -= mark;
        base.End();
    }
}
