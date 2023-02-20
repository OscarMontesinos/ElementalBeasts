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
    [Header("Hab3")]
    public Hability hab3;
    public int hab3Turn;
    public int hab3CdTotal;
    public int hab3Cd;
    public int hab3Range;
    public int hab3Rnd;
    public float hab3Dmg;
    public float hab3Pot;
    public int hab3Spd;
    [Header("Hab4")]
    public Hability hab4;
    public int hab4Turn;
    public int hab4CdTotal;
    public int hab4Cd;
    public int hab4Rnd;
    public int hab4Range;
    public float hab4Dmg;
    public int hab4Regen;
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
    public int hab8Rnd;
    public float hab8Dmg;
    public override void Awake()
    {
        base.Awake();
        repetitions1 = hab1Rmax;
        hab3CdTotal++;
        hab4CdTotal++;
        hab5CdTotal++;
        hab6CdTotal++;
        repetitions7 = hab7Rmax;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // ActualizarCDUI(repetitions1, repetitions2, hab3Cd, hab4Cd);
        if (manager != null)
        {
            if (Input.GetMouseButtonDown(0) && manager.casteando && turno)
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
                                    if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                    {
                                        CastHability(hab8.habilityType, Hability.HabilityEffect.None, hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);
                                        unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));
                                    }
                                    if(chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                    {
                                        movementPoints += 2;
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
                    case 3:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab1Range))
                                {
                                    CastHability(hab3.habilityType, hab3.habilityEffects[0], hab3.habilityRange, hab3.habilityTargetType, hab3.habilityMovement);
                                    if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                    {
                                        movementPoints += 2;
                                    }
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab3Dmg));
                                    unit.pot = 0;
                                    unit.pot = CalcularDanoMagico(hab3Pot);
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
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab1Range))
                                {
                                    CastHability(hab3.habilityType, hab3.habilityEffects[0], hab3.habilityRange, hab3.habilityTargetType, hab3.habilityMovement);
                                    if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                    {
                                        movementPoints += 2;
                                    }
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab4Dmg));
                                    unit.Heal(CalcularDanoMagico(hab4Regen));
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
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab2Range))
                                {
                                    CastHability(hab1.habilityType, hab1.habilityEffects[0], hab1.habilityRange, hab1.habilityTargetType, hab1.habilityMovement);
                                    if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                    {
                                        CastHability(hab8.habilityType, Hability.HabilityEffect.None, hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);
                                        unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));
                                    }
                                    if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                    {
                                        movementPoints += 2;
                                    }
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab5Dmg));
                                    if(unit.escudo > 0 || unit.prot>0)
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
                                    CastHability(hab1.habilityType, hab1.habilityEffects[0], hab1.habilityRange, hab1.habilityTargetType, hab1.habilityMovement);
                                    if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                    {
                                        CastHability(hab8.habilityType, Hability.HabilityEffect.None, hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);
                                        unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));
                                    } 
                                    if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                    {
                                        CastHability(hab8.habilityType, Hability.HabilityEffect.None, hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);
                                        unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));
                                    }
                                    if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                    {
                                        movementPoints += 2;
                                    }
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg));
                                    if(unit.hp > unit.mHp / 2)
                                    {
                                        unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg));
                                    }
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg));
                                    if (unit.hp < unit.mHp / 2)
                                    {
                                        unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg));
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
                       
                        break;
                    case 7:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab1Range))
                                {
                                    CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab7.habilityMovement);
                                    if (chosenHab1 == 8 || chosenHab2 == 8 || chosenHab3 == 8 || chosenHab4 == 8)
                                    {
                                        CastHability(hab8.habilityType, Hability.HabilityEffect.None, hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);
                                        unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));
                                    }
                                    if (chosenHab1 == 2 || chosenHab2 == 2 || chosenHab3 == 2 || chosenHab4 == 2)
                                    {
                                        movementPoints += 2;
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
            if (!habilityCasted && (movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                ShowHability(chosenHab4);
            }
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
            case 3:
                if (hab3Cd <= 0 && turnoRestante >= hab3Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab3Range, 0);
                }
                break;
            case 4:
                if (hab4Cd <= 0 && turnoRestante >= hab4Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
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
        repetitions7 = hab7Rmax;
        castingHability = 0;
        base.AcabarTurno();

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
                return "Dispara un aguijón puntiagudo que inflige " + CalcularDanoFisico(hab1Dmg) + " (F) de daño al objetivo.";
            case 2:
                return "Cada vez que Marasect usa una habilidad se puede mover " + hab2Range + " casillas";
            case 3:
                return "Dispara un dardo estimulante que otorga un potenciador de " + CalcularDanoMagico(hab3Pot) + " (S) al aliado objetivo, sin embargo le causa " + CalcularDanoFisico(hab3Dmg) + " (F) de daño";
            case 4:
                return "Dispara un dardo estimulante que otorga " + CalcularDanoMagico(hab4Regen) + " (S) de curación al aliado objetivo, sin embargo le causa " + CalcularDanoFisico(hab4Dmg) + "(F) de daño";
            case 5:
                return "Lanza un aguijón con fueraz que penetra la armaduara haciendo " + CalcularDanoFisico(hab5Dmg) + " (F) de daño. Si el objetivo tiene un escudo o un aumento en sus resistencias hace el doble de daño";
            case 6:
                return "Lanza dos aguijones consecuentes que hacen " + CalcularDanoFisico(hab6Dmg) + " (F) de daño cada uno.  El primero duplica el daño si el rival supera la media vida máxima, el segundo si no la supera";
            case 7:
                return "Lanza rápidamente un aguijón pequeño que hace " + CalcularDanoFisico(hab7Dmg) + " (F) y llega más lejos que el resto de sus aguijones.";
            case 8:
                return "Los ataques envenenan al objetivo por " + CalcularDanoMagico(hab8Dmg) + " (S) de daño extra por imacto";
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
                return "Dispara un aguijón puntiagudo que inflige " + CalcularDanoFisico(hab1Dmg, fuerza) + " (F) de daño al objetivo.";
            case 2:
                return "Cada vez que Marasect usa una habilidad se puede mover " + hab2Range + " casillas";
            case 3:
                return "Dispara un dardo estimulante que otorga un potenciador de " + CalcularDanoMagico(hab3Pot,sinergia) + " (S) al aliado objetivo, sin embargo le causa " + CalcularDanoFisico(hab3Dmg,fuerza) + " (F) de daño";
            case 4:
                return "Dispara un dardo estimulante que otorga " + CalcularDanoMagico(hab4Regen,sinergia) + " (S) de curación al aliado objetivo, sin embargo le causa " + CalcularDanoFisico(hab4Dmg,fuerza) + "(F) de daño";
            case 5:
                return "Lanza un aguijón con fueraz que penetra la armaduara haciendo " + CalcularDanoFisico(hab5Dmg,fuerza) + " (F) de daño. Si el objetivo tiene un escudo o un aumento en sus resistencias hace el doble de daño";
            case 6:
                return "Lanza dos aguijones consecuentes que hacen " + CalcularDanoFisico(hab6Dmg,fuerza) + " (F) de daño cada uno.  El primero duplica el daño si el rival supera la media vida máxima, el segundo si no la supera";
            case 7:
                return "Lanza rápidamente un aguijón pequeño que hace " + CalcularDanoFisico(hab7Dmg,fuerza) + " (F) y llega más lejos que el resto de sus aguijones.";
            case 8:
                return "Los ataques envenenan al objetivo por " + CalcularDanoMagico(hab8Dmg,sinergia) + " (S) de daño extra por imacto";
            default:
                return null;
        }
    }
}
