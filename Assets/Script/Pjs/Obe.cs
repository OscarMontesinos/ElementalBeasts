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
    public int hab1Range;
    public float hab1Dmg;
    public int hab1Rnd;
    [Header("Hab2")]
    public Hability hab2;
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
    public float hab4Area;
    public float hab4Dmg;
    public float hab4Desorientar;
    [Header("Hab5")]
    public Hability hab5;
    public int hab5Turn;
    public int hab5CdTotal;
    public int hab5Cd;
    public float hab5Dmg;
    [Header("Hab6")]
    public Hability hab6;
    public int hab6Turn;
    public int hab6CdTotal;
    public int hab6Cd;
    public int hab6hability;
    [Header("Hab7")]
    public Hability hab7;
    public int hab7Turn;
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
        hab2CdTotal++;
        hab3CdTotal++;
        hab4CdTotal++;
        hab5CdTotal++;
        hab6CdTotal++;
        hab8CdTotal++;
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
                    /*case 1:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab1Range))
                                {
                                    CastHability(hab1.habilityType, hab1.habilityEffects[0], hab1.habilityRange, hab1.habilityTargetType, hab1.habilityMovement);
                                    if (chosenHab1 == 7 || chosenHab2 == 7 || chosenHab3 == 7 || chosenHab4 == 7)
                                    {
                                        turnoRestante += hab7ExtraTurn;
                                        CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab1.habilityMovement);
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

                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab2Range))
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
                            turnoRestante -= hab2Turn;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 3:
                        CastHability(hab3.habilityType, hab3.habilityEffects[0], hab3.habilityRange, hab3.habilityTargetType, hab3.habilityMovement);
                        GameObject minerSearcher = Instantiate(hab3Searcher, transform.position, transform.rotation);
                        minerSearcher.GetComponent<MinerSearcher>().SetUp(team, UtilsClass.GetMouseWorldPosition(), hab3Range, this);
                        searcherList.Add(minerSearcher.GetComponent<MinerSearcher>());
                        if (searcherList.Count > hab3Max)
                        {
                            Destroy(searcherList[0].gameObject);
                            searcherList.RemoveAt(0);
                        }

                        repetitions3--;
                        turnoRestante -= hab3Turn;
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 4:
                        if (searcherList.Count > 0)
                        {
                            foreach (MinerSearcher unit in searcherList)
                            {
                                if (unit != null)
                                {
                                    if (unit.avaliable)
                                    {
                                        if (CheckVoid(unit.transform.position))
                                        {
                                            CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);

                                            Dash(this, new Vector3(unit.transform.position.x, unit.transform.position.y, transform.position.z));
                                            impacto = true;
                                        }
                                    }
                                }
                            }
                            if (impacto)
                            {
                                hab4Cd = hab4CdTotal;
                                turnoRestante -= hab4Turn;
                            }
                            else
                            {
                                int x;
                                int y;
                                Pathfinding.Instance.GetGrid().GetXY(UtilsClass.GetMouseWorldPosition(), out x, out y);
                                if (CheckVoid(UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab4Range) && Pathfinding.Instance.GetNode(x, y).isWalkable)
                                {
                                    CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);
                                    Dash(this, UtilsClass.GetMouseWorldPosition());

                                    hab4Cd = hab4CdTotal;
                                    turnoRestante -= hab4Turn;
                                }
                            }
                        }
                        else
                        {
                            int x;
                            int y;
                            Pathfinding.Instance.GetGrid().GetXY(UtilsClass.GetMouseWorldPosition(), out x, out y);
                            if (CheckVoid(UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab4Range) && Pathfinding.Instance.GetNode(x, y).isWalkable)
                            {
                                CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);
                                Dash(this, UtilsClass.GetMouseWorldPosition());

                                hab4Cd = hab4CdTotal;
                                turnoRestante -= hab4Turn;
                            }
                        }
                        MarcarHabilidad(4, 0, 0);
                        manager.diggeyeSearcherCasting = false;
                        break;
                    case 5:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckAll(unit, unit.transform.position, hab2Range))
                                {
                                    CastHability(hab5.habilityType, hab5.habilityEffects[0], hab5.habilityRange, hab5.habilityTargetType, hab5.habilityMovement);
                                    if (chosenHab1 == 7 || chosenHab2 == 7 || chosenHab3 == 7 || chosenHab4 == 7)
                                    {
                                        turnoRestante += hab7ExtraTurn;
                                        CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab1.habilityMovement);
                                    }
                                    unit.RecibirDanoFisico(CalcularDanoFisico(hab5Dmg));
                                    impacto = true;
                                    DashBehindTarget(this, unit);
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
                        if (!hab6Stage2)
                        {
                            foreach (Unit unit in manager.unitList)
                            {
                                if (unit != null)
                                {
                                    if (unit.hSelected && CheckAll(unit, unit.transform.position, hab6Range))
                                    {
                                        CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                        if (chosenHab1 == 7 || chosenHab2 == 7 || chosenHab3 == 7 || chosenHab4 == 7)
                                        {
                                            turnoRestante += hab7ExtraTurn;
                                            CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab1.habilityMovement);
                                        }
                                        unit.RecibirDanoFisico(CalcularDanoFisico(hab6Dmg));
                                        UpdateCell(true);
                                        transform.position = unit.transform.position;
                                        impacto = true;
                                    }
                                }
                            }
                            if (impacto)
                            {
                                hab6Stage2 = true;
                                manager.enemigo = false;
                            }
                        }
                        else
                        {
                            int x;
                            int y;
                            Pathfinding.Instance.GetGrid().GetXY(UtilsClass.GetMouseWorldPosition(), out x, out y);
                            if (CheckWalls(UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab6Range) && Pathfinding.Instance.GetNode(x, y).isWalkable)
                            {
                                Dash(this, UtilsClass.GetMouseWorldPosition());

                                hab6Cd = hab6CdTotal;
                                turnoRestante -= hab6Turn;
                                MarcarHabilidad(4, 0, 0);
                                hab6Stage2 = false;
                            }
                        }
                        break;
                    case 8:
                        if (CheckRange(UtilsClass.GetMouseWorldPosition(), hab8Range))
                        {

                            hab8CurrentTrap.SetUp();
                            hab8Cd = hab8Duration + hab8CdTotal;
                            turnoRestante -= hab8Turn;
                            MarcarHabilidad(4, 0, 0);
                        }
                        break;*/

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
        /*switch (hability)
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
                if (repetitions2 > 0 && turnoRestante >= hab2Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab2Range, 0);
                }
                break;
            case 3:
                if (repetitions3 > 0 && turnoRestante >= hab3Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab3Range, 0);
                }
                break;
            case 4:
                if (hab4Cd <= 0 && turnoRestante >= hab4Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.diggeyeSearcherCasting = true;
                    manager.aliado = false;
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
            case 8:
                if (hab8Cd <= 0 && turnoRestante >= hab8Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(6, hab8Range, 0);
                }
                break;
        }*/
    }

    public override void AcabarTurno()
    {
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
                return "Coloca un dron flotante en una zona que dura " + hab1Rnd + " rondas. Cuando un enemigo entra en su rango explota inflingiendo " + CalcularDanoMagico(hab1Dmg) + " (S) de daño al objetivo.";
            case 2:
                return "Protege a un aliado con una pantalla protectora que otorga " + CalcularControl(hab2Shld) + " (C) de escudo. El aliado vinculado obtiene un escudo adicional de " + CalcularControl(hab2Shld) * hab2ShldMod / 100 + "(C)";
            case 3:
                return "Vincula a un aliado, alguans habilidades interactuan con este vinculo, además, otorga una bonificación de protecciones de  " + CalcularControl(hab3Prot) + " (C)";
            case 4:
                return "Lanza un pulso energético su alrededor que inflige " + CalcularDanoMagico(hab4Dmg) + " (S) de daño. Desorienta " + hab4Desorientar + " al impactar";
            case 5:
                return "LAnza un pulso energético a una zona que daña " + CalcularDanoMagico(hab5Dmg) + " (s) de daño y aturde";
            case 6:
                return "Repite la anterior habilidad usada";
            case 7:
                return "Marca a un enemigo que aplicando una debilitación de " + CalcularControl(hab7Deb) + " (C) durante " + hab7Rnd + " rondas";
            case 8:
                return "Lanza un laser que inflije " + CalcularDanoMagico(hab8Dmg) + " (S) de daño si hay un aliado vinculado hace el doble de daño ";
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
                return "Coloca un dron flotante en una zona que dura " + hab1Rnd + " rondas. Cuando un enemigo entra en su rango explota inflingiendo " + CalcularDanoMagico(hab1Dmg,sinergia) + " (S) de daño al objetivo.";
            case 2:
                return "Protege a un aliado con una pantalla protectora que otorga " + CalcularControl(hab2Shld,control) + " (C) de escudo. El aliado vinculado obtiene un escudo adicional de " + CalcularControl(hab2Shld,control) * hab2ShldMod / 100 + "(C)";
            case 3:
                return "Vincula a un aliado, alguans habilidades interactuan con este vinculo, además, otorga una bonificación de protecciones de  " + CalcularControl(hab3Prot,control) + " (C)";
            case 4:
                return "Lanza un pulso energético su alrededor que inflige " + CalcularDanoMagico(hab4Dmg, sinergia) + " (S) de daño. Desorienta " + hab4Desorientar + " al impactar";
            case 5:
                return "LAnza un pulso energético a una zona que daña " + CalcularDanoMagico(hab5Dmg, sinergia) + " (s) de daño y aturde";
            case 6:
                return "Repite la anterior habilidad usada";
            case 7:
                return "Marca a un enemigo que aplicando una debilitación de " + CalcularControl(hab7Deb,control) + " (C) durante " + hab7Rnd + " rondas";
            case 8:
                return "Lanza un laser que inflije " + CalcularDanoMagico(hab8Dmg, sinergia) + " (S) de daño si hay un aliado vinculado hace el doble de daño ";
            default:
                return null;
        }
    }
}
