using CodeMonkey.Utils;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Marasect : Unit
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
    public int hab2Range;
    public bool hab2Stage2;
    [Header("Hab3")]
    public Hability hab3;
    public int hab3Turn;
    public int hab3CdTotal;
    public int hab3Cd;
    public int hab3Range;
    public float hab3Dmg;
    public int hab3Rnd;
    public float hab3Pot;
    public int hab3Cel;
    [Header("Hab4")]
    public Hability hab4;
    public int hab4Turn;
    public int hab4CdTotal;
    public int hab4Cd;
    public int hab4Range;
    public float hab4Dmg;
    public int hab4Rnd;
    public float hab4Regen;
    [Header("Hab5")]
    public Hability hab5;
    public int hab5Turn;
    public int hab5CdTotal;
    public int hab5Cd;
    public int hab5Range;
    public float hab5Dmg;
    [Header("Hab6")]
    public Hability hab6;
    public int hab6Turn;
    public int hab6CdTotal;
    public int hab6Cd;
    public int hab6Range;
    public float hab6Dmg;
    [Header("Hab7")]
    public Hability hab7;
    public int hab7Turn;
    public int hab7Rmax;
    int repetitions7;
    public int hab7Range;
    public float hab7Dmg;
    [Header("Hab8")]
    public Hability hab8;
    public float hab8Dmg;
    public int hab8Rnds;
    public override void Awake()
    {
        base.Awake();
        repetitions1 = hab1Rmax;
        repetitions7 = hab7Rmax;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // ActualizarCDUI(repetitions1, repetitions2, hab3Cd, hab4Cd);
        if (!CombatManager.Instance.settingUp && Input.GetMouseButtonDown(0) && manager.casteando && turno)
        {
            bool impacto = false;
            switch (castingHability)
            {
                case 1:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckAll(unit, unit.transform.position, hab1Range))
                            {
                                CastHability(hab1.habilityType, hab1.habilityEffects[0], hab1.habilityRange, hab1.habilityTargetType, hab1.habilityMovement);
                                if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                {
                                    hab2Stage2 = true;
                                }
                                if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                {
                                    MarasectVenom venom = unit.gameObject.AddComponent<MarasectVenom>();
                                    venom.BuffStart(this, hab8Rnds, CalcularDanoMagico(hab8Dmg));
                                }
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
                    if (hab2Stage2)
                    {
                        int x;
                        int y;
                        Pathfinding.Instance.GetGrid().GetXY(UtilsClass.GetMouseWorldPosition(), out x, out y);
                        if (CheckWalls(transform.position, UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab6Range) && Pathfinding.Instance.GetNode(x, y).isWalkable)
                        {
                            Dash(this, UtilsClass.GetMouseWorldPosition());
                            MarcarHabilidad(4, 0, 0);
                            hab2Stage2 = false;
                        }
                    }
                    break;
                case 3:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckAll(unit, unit.transform.position, hab3Range))
                            {
                                CastHability(hab3.habilityType, hab3.habilityEffects[0], hab3.habilityRange, hab3.habilityTargetType, hab3.habilityMovement);
                                if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                {
                                    hab2Stage2 = true;
                                }
                                unit.RecibirDanoFisico(CalcularDanoFisico(hab3Dmg));
                                MarasectEstim estim = unit.gameObject.AddComponent<MarasectEstim>();
                                estim.BuffStart(this, hab3Rnd, CalcularDanoMagico(hab3Pot), hab3Cel);
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        hab3Cd = hab3CdTotal;
                        turnoRestante -= hab3Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 4:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckAll(unit, unit.transform.position, hab4Range))
                            {
                                CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);
                                if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                {
                                    hab2Stage2 = true;
                                }
                                unit.RecibirDanoFisico(CalcularDanoFisico(hab4Dmg));
                                MarasectRecon recon = unit.gameObject.AddComponent<MarasectRecon>();
                                recon.BuffStart(this, hab4Rnd, CalcularDanoMagico(hab4Regen));
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        hab4Cd = hab4CdTotal;
                        turnoRestante -= hab4Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 5:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckAll(unit, unit.transform.position, hab5Range))
                            {
                                CastHability(hab5.habilityType, hab5.habilityEffects[0], hab5.habilityRange, hab5.habilityTargetType, hab5.habilityMovement);
                                if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                {
                                    hab2Stage2 = true;
                                }
                                if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                {
                                    MarasectVenom venom = unit.gameObject.AddComponent<MarasectVenom>();
                                    venom.BuffStart(this, hab8Rnds, CalcularDanoMagico(hab8Dmg));
                                }
                                if (unit.escudo > 0 || unit.prot > 0)
                                {
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab5Dmg*2));
                                }
                                else
                                {

                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab5Dmg));
                                }
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        hab5Cd = hab5CdTotal;
                        turnoRestante -= hab5Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 6:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckAll(unit, unit.transform.position, hab6Range))
                            {
                                CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                {
                                    hab2Stage2 = true;
                                }
                                if (unit.hp > unit.hp / 2)
                                {
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg * 2));
                                }
                                else
                                {

                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg));
                                }
                                if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                {
                                    MarasectVenom venom = unit.gameObject.AddComponent<MarasectVenom>();
                                    venom.BuffStart(this, hab8Rnds, CalcularDanoMagico(hab8Dmg));
                                }
                                if (unit.hp < unit.hp / 2)
                                {
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg * 2));
                                }
                                else
                                {

                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg));
                                }
                                if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                {
                                    MarasectVenom venom = unit.gameObject.AddComponent<MarasectVenom>();
                                    venom.BuffStart(this, hab8Rnds, CalcularDanoMagico(hab8Dmg));
                                }
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        hab6Cd = hab6CdTotal;
                        turnoRestante -= hab6Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 7:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckAll(unit, unit.transform.position, hab7Range))
                            {
                                CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab7.habilityMovement);
                                if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                {
                                    hab2Stage2 = true;
                                }
                                if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                {
                                    MarasectVenom venom = unit.gameObject.AddComponent<MarasectVenom>();
                                    venom.BuffStart(this, hab8Rnds, CalcularDanoMagico(hab8Dmg));
                                }
                                unit.RecibirDanoFisico(CalcularDanoFisico(hab7Dmg));
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        repetitions7--;
                        turnoRestante -= hab7Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;

            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || speedBuff >= 3 && movementPoints >= 1 || speedBuff > 5) || habilityCasted)
            {
                ShowHability(chosenHab1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || speedBuff >= 3 && movementPoints >= 1 || speedBuff > 5) || habilityCasted)
            {
                ShowHability(chosenHab2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || speedBuff >= 3 && movementPoints >= 1 || speedBuff > 5) || habilityCasted)
            {
                ShowHability(chosenHab3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || speedBuff >= 3 && movementPoints >= 1 || speedBuff > 5) || habilityCasted)
            {
                ShowHability(chosenHab4);
            }
        }

        if (hab2Stage2 && turno)
        {
            ShowHability(2);
        }
        else
        {
            hab2Stage2 = false;
        }

    }

    public override void ShowHability(int hability)
    {
        switch (hability)
        {
            case 1:
                if (repetitions1 > 0 && turnoRestante >= hab1Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab1Range, 0);
                }
                break;
            case 2:
                if (hab2Stage2)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab2Range, 0);
                }
                break;
            case 3:
                if (hab3Cd <= 0 && turnoRestante >= hab3Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = true;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab3Range, 0);
                }
                break;
            case 4:
                if (hab4Cd <= 0 && turnoRestante >= hab4Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = true;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab4Range, 0);
                }
                break;
            case 5:
                if (hab5Cd <= 0 && turnoRestante >= hab5Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab5Range, 0);
                }
                break;
            case 6:
                if (hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab6Range, 0);
                }
                break;
            case 7:
                if (repetitions7 > 0 && turnoRestante >= hab7Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab7Range, 0);
                }
                break;
        }
    }

    public override void AcabarTurno()
    {
        repetitions1 = hab1Rmax;
        repetitions7 = hab7Rmax;
        if (hab3Cd != 0)
        {
            hab3Cd--;
        }
        if (hab4Cd != 0)
        {
            hab4Cd--;
        }
        if (hab5Cd != 0)
        {
            hab5Cd--;
        }
        if (hab6Cd != 0)
        {
            hab6Cd--;
        }
        castingHability = 0;
        base.AcabarTurno();

    }

    public override HabCd GetHabCds(int hability)
    {
        switch (hability)
        {
            case 0:
                HabCd info0 = new HabCd(0, CombatSheetHabImage.HabCdType.none);
                return info0;
            case 1:
                HabCd info1 = new HabCd(repetitions1, CombatSheetHabImage.HabCdType.repetitions);
                return info1;
            case 2:
                HabCd info2 = new HabCd(0, CombatSheetHabImage.HabCdType.none);
                return info2;
            case 3:
                HabCd info3 = new HabCd(hab3Cd, CombatSheetHabImage.HabCdType.cd);
                return info3;
            case 4:
                HabCd info4 = new HabCd(hab4Cd, CombatSheetHabImage.HabCdType.cd);
                return info4;
            case 5:
                HabCd info5 = new HabCd(hab5Cd, CombatSheetHabImage.HabCdType.cd);
                return info5;
            case 6:
                HabCd info6 = new HabCd(hab6Cd, CombatSheetHabImage.HabCdType.cd);
                return info6;
            case 7:
                HabCd info7 = new HabCd(repetitions7, CombatSheetHabImage.HabCdType.repetitions);
                return info7;
            case 8:
                HabCd info8 = new HabCd(0, CombatSheetHabImage.HabCdType.none);
                return info8;
            default:
                HabCd info = new HabCd(0, CombatSheetHabImage.HabCdType.none);
                return info;
        }
    }

    public override Sprite GetHabIcon(int hability)
    {
        switch (hability)
        {
            case 0:
                return defaultHabSprite;
            case 1:
                return hab1.icon;
            case 2:
                return hab2.icon;
            case 3:
                return hab3.icon;
            case 4:
                return hab4.icon;
            case 5:
                return hab5.icon;
            case 6:
                return hab6.icon;
            case 7:
                return hab7.icon;
            case 8:
                return hab8.icon;
            default:
                return null;
        }
    }

    public override string GetHabName(int hability)
    {
        switch (hability)
        {
            case 0:
                return " ";
            case 1:
                return hab1.name;
            case 2:
                return hab2.name;
            case 3:
                return hab3.name;
            case 4:
                return hab4.name;
            case 5:
                return hab5.name;
            case 6:
                return hab6.name;
            case 7:
                return hab7.name;
            case 8:
                return hab8.name;
            default:
                return null;
        }
    }

    public override string GetHabDescription(int hability)
    {
        switch (hability)
        {
            case 0:
                return " ";
            case 1:
                return "Dispara un aguijón que inflige " + CalcularDanoFisico(hab1Dmg) + " (F) de daño al objetivo.";
            case 2:
                return "Cada vez que se usa una habilidad permite moverse hasta " + hab2Range + " casillas";
            case 3:
                return "Inyecta un dardo estimulante a un aliado, dañándolo " + CalcularDanoFisico(hab3Dmg) + " (F) y potenciándolo " + CalcularDanoMagico(hab3Pot) + " (S) durante " + hab3Rnd + " rondas" ;
            case 4:
                return "Inyecta un dardo regenerador a un aliado, dañándolo " + CalcularDanoFisico(hab4Dmg) + " (F) cada ronda se curará " + CalcularDanoMagico(hab4Regen) + " (S) durante " + hab4Rnd + " rondas";
            case 5:
                return "Dispara un aguijón perforante que hace " + CalcularDanoFisico(hab5Dmg) + " (F) de daño, si el objetivo tiene un escudo o se ha protegido inflige el doble de daño";
            case 6:
                return "Dispara dos aguijones que infligen " + CalcularDanoFisico(hab6Dmg) + " (F) de daño cada uno. El primero hace el doble de daño si la vida del objetivo es mayor a la mitad, el otro si es inferior a la mitad";
            case 7:
                return "Con una maniobra fugaz lanza un aguijón a alto rango que inflige " + CalcularDanoFisico(hab7Dmg) + " (F) de daño a cambio de muy poco turno";
            case 8:
                return "Todos los ataques a enemigos envenenan " + CalcularDanoMagico (hab8Dmg) + " (S) de daño, durante " + hab8Rnds + " rondas";
            default:
                return null;
        }
    }
    public override string GetHabDescription(int hability, float sinergia, float fuerza, float control)
    {
        switch (hability)
        {
            case 0:
                return " ";
            case 1:
                return "Dispara un aguijón que inflige " + CalcularDanoFisico(hab1Dmg,fuerza) + " (F) de daño al objetivo.";
            case 2:
                return "Cada vez que se usa una habilidad permite moverse hasta " + hab2Range + " casillas";
            case 3:
                return "Inyecta un dardo estimulante a un aliado, dañándolo " + CalcularDanoFisico(hab3Dmg, fuerza) + " (F) y potenciándolo " + CalcularDanoMagico(hab3Pot, sinergia) + " (S) durante " + hab3Rnd + " rondas";
            case 4:
                return "Inyecta un dardo regenerador a un aliado, dañándolo " + CalcularDanoFisico(hab4Dmg, fuerza) + " (F) cada ronda se curará " + CalcularDanoMagico(hab4Regen, sinergia) + " (S) durante " + hab4Rnd + " rondas";
            case 5:
                return "Dispara un aguijón perforante que hace " + CalcularDanoFisico(hab5Dmg, fuerza) + " (F) de daño, si el objetivo tiene un escudo o se ha protegido inflige el doble de daño";
            case 6:
                return "Dispara dos aguijones que infligen " + CalcularDanoFisico(hab6Dmg, fuerza) + " (F) de daño cada uno. El primero hace el doble de daño si la vida del objetivo es mayor a la mitad, el otro si es inferior a la mitad";
            case 7:
                return "Con una maniobra fugaz lanza un aguijón a alto rango que inflige " + CalcularDanoFisico(hab7Dmg, fuerza) + " (F) de daño a cambio de muy poco turno";
            case 8:
                return "Todos los ataques a enemigos envenenan " + CalcularDanoMagico(hab8Dmg, sinergia) + " (S) de daño, durante " + hab8Rnds + " rondas";
            default:
                return null;
        }
    }
}
