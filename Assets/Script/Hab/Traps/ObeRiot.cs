using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeRiot : ObjetoInvocado
{
    Obe obe;
    float dano;
    List<Unit> targets;
    public override void Start()
    {
        base.Start();
        foreach (Unit unit in manager.unitList)
        {
            if (unit != null)
            {
                if (unit.turno == true)
                {
                    unidad = unit;
                }
            }
        }
        obe = unidad.GetComponent<Obe>();
        dano = obe.CalcularDanoMagico(obe.hab5Dmg);
        rondasDuracion = (obe.hab5Duration*CombatManager.Instance.unitList.Count);
    }
    public override void Golpear(Unit objetivo)
    {
        obe.CastHability(obe.hab5.habilityType, obe.hab5.habilityEffects[0], obe.hab5.habilityEffects[1], obe.hab5.habilityRange, obe.hab5.habilityTargetType, obe.hab5.habilityMovement);
        objetivo.RecibirDanoMagico(dano);
        objetivo.Stunn();
        base.Golpear(objetivo);
    }
    public override void Desgolpear(Unit objetivo)
    {
        base.Desgolpear(objetivo);
    }

    public override void Die()
    {
        base.Die();
    }

}
