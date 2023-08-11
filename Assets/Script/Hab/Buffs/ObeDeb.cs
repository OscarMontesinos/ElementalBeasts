using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeDeb : Buff
{
    Obe owner;
    float deb;
    public bool triggered;
    // Start is called before the first frame update

    private void Update()
    {
        if(owner == null)
        {
            End();
        }
    }

    public void BuffStart(Obe owner, int rnds,float deb)
    {
        type = BuffType.debuff;
        this.owner = owner;
        rounds = rnds;
        this.deb = deb;
    }
    public void Trigger()
    {
        triggered = true;
        unit.prot -= deb;
    }

    public override void BuffUpdate()
    {
        base.BuffUpdate();
    }

    public override void End()
    {
        if (triggered)
        {
            unit.prot += deb;
        }
        base.End();
    }
}
