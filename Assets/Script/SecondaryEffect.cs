using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryEffect : MonoBehaviour
{
    int duration;
    int roundCreated;
    bool earlyUpdate;
    bool unitTurnUpdate;
    Unit unit;

    effectType type;
    public enum effectType
    {
        Enhacer, Exhaust, Protection, Weakening, Shield, DoT, Regen, Spd, Slow
    }
    public virtual void Start()
    {
        if(type == effectType.Exhaust || type == effectType.Enhacer)
        {
            earlyUpdate = false;
            unitTurnUpdate = true;
        }
        else if(type == effectType.DoT || type == effectType.Regen)
        {
            earlyUpdate = true;
            unitTurnUpdate = true;
        }
        else
        {
            unitTurnUpdate = true;
        }
    }
    public virtual void RoundUpdate(bool startingTurn)
    {
        if (unitTurnUpdate)
        {
            if (earlyUpdate && startingTurn)
            {
                Trigger();
            }
            else if (!earlyUpdate && !startingTurn)
            {
                Trigger();
            }
        }
        else if (unit.GetManager().ronda == roundCreated)
        {
            Trigger();
        }
    }
    public virtual void Trigger()
    {
        duration--;
        if(duration <= 0)
        {
            EndEffect();
        }
    }

    public virtual void StartEffect()
    {
        Destroy(this);
    }
    public virtual void EndEffect()
    {
        Destroy(this);
    }
}
