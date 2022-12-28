using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diggeye : Unit
{

    [Header("Hab1")]
    public Hability hab1;
    public int hab1Turn;
    public int hab1Rmax;
    int repetitions1;
    public int hab1Range;
    public float hab1Dmg;
    [Header("Hab2")]
    public Hability hab2;
    public int hab2Turn;
    public int hab2Rmax;
    int repetitions2;
    public int hab2Range;
    public float hab2Dmg;
    [Header("Hab3")]
    public int hab3Turno;
    public int hab3CdTotal;
    public int hab3Cd;
    public int hab3Rango;
    public int hab3Ancho;
    public int hab3Rondas;
    public float hab3Escalado;
    [Header("Hab4")]
    public int hab4Turno;
    public int hab4CdTotal;
    public int hab4Cd;
    public int hab4Rango;
    public float hab4Escalado;
    public override void Awake()
    {
        base.Awake();
        repetitions1 = hab1Rmax;
        repetitions2 = hab2Rmax;
        hab3CdTotal++;
        hab4CdTotal++;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
       // ActualizarCDUI(repetitions1, repetitions2, hab3Cd, hab4Cd);
        if (Input.GetMouseButtonDown(0) && manager.casteando && turno )
        {
            bool impacto = false;
            switch (castingHability)
            {
                case 1:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && TargetAvaliable(unit.transform.position)&& unit == GetTarget(unit.transform.position))
                            {
                                CastHability(hab1.habilityType,hab1.habilityEffects[0],hab1.habilityRange,hab1.habilityTargetType,hab1.habilityMovement);
                                unit.RecibirDanoFisico(CalcularDanoFisico(hab1Dmg));
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        repetitions1--;
                        turnoRestante -= hab1Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 2:

                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && TargetAvaliable(unit.transform.position) && unit == GetTarget(unit.transform.position))
                            {
                                CastHability(hab2.habilityType, hab2.habilityEffects[0], hab2.habilityEffects[1], hab2.habilityRange, hab2.habilityTargetType, hab2.habilityMovement);
                                unit.RecibirDanoFisico(CalcularDanoFisico(hab2Dmg));
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        repetitions2--;
                        turnoRestante -= hab1Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                ShowHability(chosenHab1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                ShowHability(chosenHab2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                ShowHability(chosenHab3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && turno && !moving)
        {
            if (!habilityCasted &&( movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                ShowHability(chosenHab4);
            }
        }

    }

    public override void ShowHability(int hability)
    {
        manager.DestroyShowNodes();
        castingHability = hability;
        switch (castingHability)
        {
            case 1:
                if (repetitions1 > 0 && turnoRestante >= hab1Turn)
                {
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab1Range, 0);
                }
                break;
            case 2:
                if (repetitions2 > 0 &&  turnoRestante >= hab2Turn)
                {
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab2Range, 0);
                }
                break;
        }
    }

    public override void AcabarTurno()
    {
        base.AcabarTurno();

        repetitions1 = hab1Rmax;
        repetitions2 = hab2Rmax;
        if (hab3Cd != 0)
        {
            hab3Cd--;
        }
        if (hab4Cd != 0)
        {
            hab4Cd--;
        }
        castingHability = 0;

    }
}
    