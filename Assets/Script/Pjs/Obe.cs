using CodeMonkey.Utils;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obe : Unit
{

    [Header("Hab1")]
    public Hability hab1;
    public int hab1Turn;
    public int hab1CdTotal;
    public int hab1Cd;
    public int hab1Range;
    public int hab1Area;
    public float hab1Dmg;
    [Header("Hab2")]
    public Hability hab2;
    public int hab2Turn;
    public int hab2Range;
    public int hab2CdTotal;
    public int hab2Cd;
    public float hab2Shld;
    public float hab2ShldMod;
    [Header("Hab3")]
    public Hability hab3;
    public int hab3Turn;
    public int hab3CdTotal;
    public int hab3Cd;
    public int hab3Range;
    public float hab3Prot;
    [Header("Hab4")]
    public Hability hab4;
    public int hab4Turn;
    public int hab4CdTotal;
    public int hab4Cd;
    public int hab4Area;
    public float hab4Dmg;
    [Header("Hab5")]
    public Hability hab5;
    public int hab5Turn;
    public int hab5CdTotal;
    public int hab5Cd;
    public int hab5Range;
    public int hab5Area;
    public float hab5Dmg;
    [Header("Hab6")]
    public Hability hab6;
    public int hab6Turn;
    public int hab6CdTotal;
    public int hab6Cd;
    public int hab6LastHability;
    public bool hab6UsingHab;
    [Header("Hab7")]
    public Hability hab7;
    public int hab7Turn;
    public int hab7CdTotal;
    public int hab7Cd;
    public int hab7Range;
    public int hab7Rnd;
    public float hab7Deb;
    [Header("Hab8")]
    public Hability hab8;
    public int hab8Turn;
    public int hab8CdTotal;
    public int hab8Cd;
    public int hab8Range;
    public float hab8Dmg;
    public override void Awake()
    {
        base.Awake();
        hab1CdTotal++;
        hab2CdTotal++;
        hab3CdTotal++;
        hab4CdTotal++;
        hab5CdTotal++;
        hab6CdTotal++;
        hab7CdTotal++;
        hab8CdTotal++;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // ActualizarCDUI(repetitions1, repetitions2, hab3Cd, hab4Cd);
        if (manager != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                hab6UsingHab = false;
            }
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
                                if (unit.hSelected && CheckWalls(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z))))
                                {
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab1Dmg));
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab1Cd = hab1CdTotal;
                            turnoRestante -= hab1Turn;
                            hab6LastHability = 1;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 2:

                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab2Range))
                                {
                                    CastHability(hab2.habilityType, hab2.habilityEffects[0], hab2.habilityRange, hab2.habilityTargetType, hab2.habilityMovement);
                                    if (unit.vinculo)
                                    {
                                        unit.Escudo(CalcularControl(hab2Shld + hab2ShldMod));
                                    }
                                    else
                                    {
                                        unit.Escudo(CalcularControl(hab2Shld));
                                        foreach (Unit unit2 in manager.unitList)
                                        {
                                            if (unit2.vinculo && unit2.team == team)
                                            {
                                                unit.Escudo(CalcularControl(hab2ShldMod));
                                            }
                                        }
                                    }
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto && !hab6UsingHab)
                        {
                            hab2Cd = hab2CdTotal;
                            turnoRestante -= hab4Turn;
                            hab6LastHability = 2;
                        }
                        else if (impacto)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6UsingHab = false;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 3:

                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab3Range))
                                {
                                    CastHability(hab3.habilityType, hab3.habilityEffects[0], hab3.habilityRange, hab3.habilityTargetType, hab3.habilityMovement);

                                    foreach (Unit unit2 in manager.unitList)
                                    {
                                        if (unit2.team == team)
                                        {
                                            unit2.vinculo = false;
                                        }
                                    }
                                    unit.AddProt(CalcularControl(hab3Prot));
                                    unit.vinculo = true;
                                    impacto = true;
                                }
                            }
                        }


                        if (impacto && !hab6UsingHab)
                        {
                            hab3Cd = hab3CdTotal;
                            turnoRestante -= hab3Turn;
                            hab6LastHability = 3;
                        }
                        else if (impacto)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6UsingHab = false;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 4:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab4Area))
                                {
                                    CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);

                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab4Dmg));
                                    if (unit.prot > 0)
                                    {
                                        unit.AddProt(0);
                                    }
                                    if (unit.pot > 0)
                                    {
                                        unit.AddPot(0);
                                    }
                                    if (unit.escudo > 0)
                                    {
                                        unit.Escudo(0);
                                    }
                                    impacto = true;
                                }
                            }
                        }

                        if (impacto && !hab6UsingHab)
                        {
                            hab4Cd = hab4CdTotal;
                            turnoRestante -= hab4Turn;
                            hab6LastHability = 4;
                        }
                        else if (impacto)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6UsingHab = false;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 5:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab5Area))
                                {
                                    CastHability(hab5.habilityType, hab5.habilityEffects[0], hab5.habilityRange, hab5.habilityTargetType, hab5.habilityMovement);

                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab5Dmg));
                                    unit.AddProt(0);
                                    unit.AddPot(0);
                                    unit.Escudo(0);
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto && !hab6UsingHab)
                        {
                            hab5Cd = hab5CdTotal;
                            turnoRestante -= hab5Turn;
                            hab6LastHability = 5;
                        }
                        else if (impacto)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6UsingHab = false;
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

                                    unit.AddProt(hab7Deb);
                                }
                                impacto = true;
                            }
                        }
                        if (impacto && !hab6UsingHab)
                        {
                            hab7Cd = hab7CdTotal;
                            turnoRestante -= hab7Turn;
                            hab6LastHability = 7;
                        }
                        else if (impacto)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6UsingHab = false;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 8:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab7Range))
                                {
                                    CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab7.habilityMovement);

                                    foreach (Unit unit2 in manager.unitList)
                                    {
                                        if (unit2.team == team && vinculo)
                                        {
                                            unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));
                                            impacto = true;
                                        }
                                    }
                                    if (!impacto)
                                    {
                                        unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg * 2));
                                    }
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto && !hab6UsingHab)
                        {
                            hab8Cd = hab8CdTotal;
                            turnoRestante -= hab8Turn;
                            hab6LastHability = 8;
                        }
                        else if (impacto)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6UsingHab = false;
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
                if (chosenHab1 == 6 && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    ShowHability(hab6LastHability);
                    hab6UsingHab = true;
                }
                else
                {
                    ShowHability(chosenHab1);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                if (chosenHab2 == 6 && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    ShowHability(hab6LastHability);
                    hab6UsingHab = true;
                }
                else
                {
                    ShowHability(chosenHab2);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                if (chosenHab3 == 6 && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    ShowHability(hab6LastHability);
                    hab6UsingHab = true;
                }
                else
                {
                    ShowHability(chosenHab3);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && turno && !moving)
        {
            if (!habilityCasted && (movementPoints >= 2 || slow >= 3 && movementPoints >= 1 || slow > 5) || habilityCasted)
            {
                if (chosenHab4 == 6 && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    ShowHability(hab6LastHability);
                    hab6UsingHab = true;
                }
                else
                {
                    ShowHability(chosenHab4);
                }
            }
        }

        if (castingHability == 1 && manager.casteando == true && turno || castingHability == 6 && hab6LastHability == 1 && manager.casteando == true && turno)
        {
            ShowHability(1);
        }
        if (castingHability == 4 && manager.casteando == true && turno || castingHability == 6 && hab6LastHability == 4 && manager.casteando == true && turno)
        {
            ShowHability(4);
        }
        if (castingHability == 5 && manager.casteando == true && turno || castingHability==6 && hab6LastHability==5 && manager.casteando == true && turno)
        {
            ShowHability(5);
        }

    }

    public override void ShowHability(int hability)
    {
        switch (hability)
        {
            case 1:
                if (hab1Cd <= 0 && turnoRestante >= hab1Turn || hab6UsingHab && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(1, hab1Range, hab1Area);
                }
                break;
            case 2:
                if (hab2Cd <= 0 && turnoRestante >= hab2Turn || hab6UsingHab && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = true;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab2Range, 0);
                }
                break;
            case 3:
                if (hab3Cd <= 0 && turnoRestante >= hab3Turn || hab6UsingHab && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = true;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab3Range, 0);
                }
                break;
            case 4:
                if (hab4Cd <= 0 && turnoRestante >= hab4Turn || hab6UsingHab && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.diggeyeSearcherCasting = true;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(5, hab4Area, 0);
                }
                break;
            case 5:
                if (hab5Cd <= 0 && turnoRestante >= hab5Turn || hab6UsingHab && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(1, hab5Range, hab5Area);
                }
                break;
            case 7:
                if (hab7Cd <= 0 && turnoRestante >= hab7Turn || hab6UsingHab && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab7Range, 0);
                }
                break;
            case 8:
                if (hab8Cd <= 0 && turnoRestante >= hab8Turn || hab6UsingHab && hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab8Range, 0);
                }
                break;
        }
    }

    public override void AcabarTurno()
    {
        if (hab1Cd != 0)
        {
            hab1Cd--;
        }
        if (hab2Cd != 0)
        {
            hab2Cd--;
        }
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
        if (hab7Cd != 0)
        {
            hab7Cd--;
        }
        if (hab8Cd != 0)
        {
            hab8Cd--;
        }
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
                return "Turno: "+ hab1Turn +"Recarga: "+ hab1Cd+"\nLanza una mina que explota inflingiendo " + CalcularDanoMagico(hab1Dmg) + " (S) de daño al objetivo.";
            case 2:
                return "Turno: " + hab2Turn + "Recarga: " + hab2Cd + "\nProtege a un aliado con una pantalla protectora que otorga " + CalcularControl(hab2Shld) + " (C) de escudo. El aliado vinculado obtiene un escudo adicional de " + CalcularControl(hab2Shld) * hab2ShldMod / 100 + "(C)";
            case 3:
                return "Turno: " + hab3Turn + "Recarga: " + hab3Cd + "\nVincula a un aliado, alguans habilidades interactuan con este vinculo, además, otorga una bonificación de protecciones de  " + CalcularControl(hab3Prot) + " (C)";
            case 4:
                return "Turno: " + hab4Turn + "Recarga: " + hab4Cd + "\nLanza un pulso energético su alrededor que inflige " + CalcularDanoMagico(hab4Dmg) + " (S) de daño. Elimina cualquier aumento de estadísticas y escudos";
            case 5:
                return "Turno: " + hab5Turn + "Recarga: " + hab5Cd + "\nLanza un pulso energético a una zona que daña " + CalcularDanoMagico(hab5Dmg) + " (s) de daño y aturde";
            case 6:
                return "Turno: " + hab6Turn + "Recarga: " + hab6Cd + "\nRepite la anterior habilidad usada";
            case 7:
                return "Turno: " + hab7Turn + "Recarga: " + hab7Cd + "\nMarca a un enemigo que aplicando una debilitación de " + CalcularControl(hab7Deb) + " (C) durante " + hab7Rnd + " rondas";
            case 8:
                return "Turno: " + hab8Turn + "Recarga: " + hab8Cd + "\nLanza un laser que inflije " + CalcularDanoMagico(hab8Dmg) + " (S) de daño si hay un aliado vinculado hace el doble de daño ";
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
                return "Turno: "+ hab1Turn +" Recarga: "+ hab1Cd+"\nLanza una mina que explota inflingiendo " + CalcularDanoMagico(hab1Dmg,sinergia) + " (S) de daño al objetivo.";
            case 2:
                return "Turno: " + hab2Turn + " Recarga: " + hab2Cd + "\nProtege a un aliado con una pantalla protectora que otorga " + CalcularControl(hab2Shld,control) + " (C) de escudo. El aliado vinculado obtiene un escudo adicional de " + CalcularControl(hab2Shld,control) * hab2ShldMod / 100 + "(C)";
            case 3:
                return "Turno: " + hab3Turn + " Recarga: " + hab3Cd + "\nVincula a un aliado, alguans habilidades interactuan con este vinculo, además, otorga una bonificación de protecciones de  " + CalcularControl(hab3Prot,control) + " (C)";
            case 4:
                return "Turno: " + hab4Turn + " Recarga: " + hab4Cd + "\nLanza un pulso energético su alrededor que inflige " + CalcularDanoMagico(hab4Dmg,sinergia) + " (S) de daño. Elimina cualquier aumento de estadísticas y escudos";
            case 5:
                return "Turno: " + hab5Turn + " Recarga: " + hab5Cd + "\nLanza un pulso energético a una zona que daña " + CalcularDanoMagico(hab5Dmg, sinergia) + " (s) de daño y aturde";
            case 6:
                return "Turno: " + hab6Turn + " Recarga: " + hab6Cd + "\nRepite la anterior habilidad usada";
            case 7:
                return "Turno: " + hab7Turn + " Recarga: " + hab7Cd + "\nMarca a un enemigo que aplicando una debilitación de " + CalcularControl(hab7Deb,control) + " (C) durante " + hab7Rnd + " rondas";
            case 8:
                return "Turno: " + hab8Turn + " Recarga: " + hab8Cd + "\nLanza un laser que inflije " + CalcularDanoMagico(hab8Dmg, sinergia) + " (S) de daño si no hay un aliado vinculado hace el doble de daño ";
            default:
                return null;
        }
    }
}
