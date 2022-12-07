using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Hability
{
    public string name;
    public int slot;
    public enum HabilityType
    {
        Basic, Hability, Innate
    }
    public enum HabilityEffect
    {
        None, Attack, Buff, Debuff, Heal, Shield, Purge, Stunn, Trap, Scan, Curse
    }
    public enum HabilityRange
    {
        None, Melee, Range
    }
    public enum HabilityTargetType
    {
        None, Single, Area, Self
    }
    public enum HabilityMovement
    {
        None, Dash, PhantomStep, Blink
    }


    public HabilityType habilityType;
    public List<HabilityEffect> habilityEffects;
    public HabilityRange habilityRange;
    public HabilityTargetType habilityTargetType;
    public HabilityMovement habilityMovement;
}
