using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeTrap : ObjetoInvocado
{
    Obe obe;
    float dano;
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
        dano = obe.CalcularDanoMagico(obe.hab1Dmg);
        rondasDuracion = (obe.hab1Duration * CombatManager.Instance.unitList.Count);
    }
    public override void Golpear(Unit objetivo)
    {
        base.Golpear(objetivo);
        if (objetivo.team != obe.team)
        {
            obe.CastHability(obe.hab1.habilityType, obe.hab1.habilityEffects[0], obe.hab1.habilityRange, obe.hab1.habilityTargetType, obe.hab1.habilityMovement);
            objetivo.RecibirDanoMagico(dano);
            obe.hab1Cd = obe.hab1CdTotal;
            Destroy(gameObject);
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Unit>() && !planning && obe.turno && collision.gameObject.GetComponent<Unit>().team != team)
        {
            obe.turnoRestante += obe.hab1Turn;
            obe.hab1Cd = 0;
            Destroy(gameObject);
        }
    }
}
