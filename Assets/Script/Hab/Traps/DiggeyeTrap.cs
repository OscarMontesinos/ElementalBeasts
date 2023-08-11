using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggeyeTrap : ObjetoInvocado
{
    Diggeye diggeye;
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
        diggeye = unidad.GetComponent<Diggeye>();
        dano = diggeye.CalcularDanoFisico(diggeye.hab8Dmg); 
    }
    public override void Golpear(Unit objetivo)
    {
        base.Golpear(objetivo);
        if (objetivo.team != diggeye.team)
        {
            diggeye.CastHability(diggeye.hab8.habilityType, diggeye.hab8.habilityEffects[0],diggeye.hab8.habilityEffects[1],diggeye.hab8.habilityEffects[2], diggeye.hab8.habilityRange, diggeye.hab8.habilityTargetType, diggeye.hab8.habilityMovement);
            objetivo.RecibirDanoFisico(dano);
            objetivo.root = true;
            diggeye.hab8Cd = diggeye.hab8CdTotal;
            objetivo.movementPoints = 0;
            objetivo.turnoRestante = 0;
            objetivo.StopMoving();
            Destroy(gameObject);
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Unit>() && !planning && diggeye.turno && collision.gameObject.GetComponent<Unit>().team!=team)
        {
            diggeye.turnoRestante += diggeye.hab8Turn;
            diggeye.hab8Cd =0;
            Destroy(gameObject);
        }
    }
}
