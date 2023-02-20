using CodeMonkey.Utils;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Storminn : Unit
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
    public int hab2CdTotal;
    public int hab2Cd;
    public int hab2Range;
    public int hab2Area;
    public float hab2Dmg;
    [Header("Hab3")]
    public Hability hab3;
    public int hab3Turn;
    public int hab3CdTotal;
    public int hab3Cd;
    public int hab3Range;
    public int hab3Area;
    public float hab3Dmg;
    public int hab3HabilitiesUsed;
    [Header("Hab4")]
    public Hability hab4;
    public int hab4Turn;
    public int hab4Rmax;
    int repetitions4;
    public int hab4Range;
    public float hab4Dmg;
    [Header("Hab5")]
    public Hability hab5;
    public int hab5Turn;
    public int hab5CdTotal;
    public int hab5Cd;
    public int hab5Range;
    public int hab5Area;
    public float hab5Exh;
    [Header("Hab6")]
    public Hability hab6;
    public int hab6Area;
    public float hab6Heal;
    [Header("Hab7")]
    public Hability hab7;
    public int hab7Turn;
    public int hab7CdTotal;
    public int hab7Cd;
    public int hab7Range;
    public int hab7Area;
    public float hab7Dmg;
    public float hab7Deb;
    [Header("Hab8")]
    public Hability hab8;
    public int hab8Turn;
    public int hab8CdTotal;
    public int hab8Cd;
    public int hab8Range;
    public override void Awake()
    {
        base.Awake();
        repetitions1 = hab1Rmax;
        hab2CdTotal++;
        hab3CdTotal++;
        repetitions4 = hab4Rmax;
        hab5CdTotal++;
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
                                    if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
                                    {
                                        foreach (Unit unit2 in manager.unitList)
                                        {
                                            if (unit2 != null)
                                            {
                                                if (unit2.hSelected && CheckAll(unit2, unit2.transform.position, hab6Area) && unit2.team == team)
                                                {
                                                    CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                                    unit2.Heal(CalcularControl(hab6Heal));
                                                }
                                            }
                                        }
                                    }
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab1Dmg));
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab3HabilitiesUsed++;
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
                                if (unit.hSelected && CheckWalls(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z))))
                                {
                                    CastHability(hab2.habilityType, hab2.habilityEffects[0], hab2.habilityRange, hab2.habilityTargetType, hab2.habilityMovement);
                                    if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
                                    {
                                        foreach (Unit unit2 in manager.unitList)
                                        {
                                            if (unit2 != null)
                                            {
                                                if (unit2.hSelected && CheckAll(unit2, unit2.transform.position, hab6Area) && unit2.team == team)
                                                {
                                                    CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                                    unit2.Heal(CalcularControl(hab6Heal));
                                                }
                                            }
                                        }
                                    }
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab2Dmg));
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab3HabilitiesUsed++;
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
                                if (unit.hSelected && hab3HabilitiesUsed>=3 && CheckWalls(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z))))
                                {
                                    CastHability(hab3.habilityType, hab3.habilityEffects[0], hab3.habilityRange, hab3.habilityTargetType, hab3.habilityMovement);
                                    if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
                                    {
                                        foreach (Unit unit2 in manager.unitList)
                                        {
                                            if (unit2 != null)
                                            {
                                                if (unit2.hSelected && CheckAll(unit2, unit2.transform.position, hab6Area) && unit2.team == team)
                                                {
                                                    CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                                    unit2.Heal(CalcularControl(hab6Heal));
                                                }
                                            }
                                        }
                                    }
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab3Dmg));
                                    hab3HabilitiesUsed = 0;
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
                                    CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);
                                    if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
                                    {
                                        foreach (Unit unit2 in manager.unitList)
                                        {
                                            if (unit2 != null)
                                            {
                                                if (unit2.hSelected && CheckAll(unit2, unit2.transform.position, hab6Area) && unit2.team == team)
                                                {
                                                    CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                                    unit2.Heal(CalcularControl(hab6Heal/2));
                                                }
                                            }
                                        }
                                    }
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab4Dmg));
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab3HabilitiesUsed++;
                            repetitions4--;
                            turnoRestante -= hab4Turn;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 5:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckWalls(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z))))
                                {
                                    CastHability(hab5.habilityType, hab5.habilityEffects[0], hab5.habilityRange, hab5.habilityTargetType, hab5.habilityMovement);
                                    if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
                                    {
                                        foreach (Unit unit2 in manager.unitList)
                                        {
                                            if (unit2 != null)
                                            {
                                                if (unit2.hSelected && CheckAll(unit2, unit2.transform.position, hab6Area) && unit2.team == team)
                                                {
                                                    CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                                    unit2.Heal(CalcularControl(hab6Heal));
                                                }
                                            }
                                        }
                                    }
                                    unit.pot = 0;
                                    unit.pot = -CalcularControl(hab5Exh);
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab3HabilitiesUsed++;
                            hab5Cd = hab5CdTotal;
                            turnoRestante -= hab5Turn;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break; 
                    case 7:
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckWalls(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z))))
                                {
                                    CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab7.habilityMovement);
                                    if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
                                    {
                                        foreach (Unit unit2 in manager.unitList)
                                        {
                                            if (unit2 != null)
                                            {
                                                if (unit2.hSelected && CheckAll(unit2, unit2.transform.position, hab6Area) && unit2.team == team)
                                                {
                                                    CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                                    unit2.Heal(CalcularControl(hab6Heal));
                                                }
                                            }
                                        }
                                    }
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab7Dmg));
                                    unit.prot = 0;
                                    unit.prot = -CalcularControl(hab7Deb);
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab3HabilitiesUsed++;
                            hab7Cd = hab7CdTotal;
                            turnoRestante -= hab7Turn;
                        }
                        MarcarHabilidad(4, 0, 0);
                        break;
                    case 8:
                        if (CheckVoid(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z))))
                        {
                            CastHability(hab8.habilityType, hab8.habilityEffects[0], hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);

                            Dash(this,Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z)));
                            if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
                            {
                                foreach (Unit unit2 in manager.unitList)
                                {
                                    if (unit2 != null)
                                    {
                                        if (unit2.hSelected && CheckAll(unit2, unit2.transform.position, hab6Area) && unit2.team == team)
                                        {
                                            CastHability(hab6.habilityType, hab6.habilityEffects[0], hab6.habilityRange, hab6.habilityTargetType, hab6.habilityMovement);
                                            unit2.Heal(CalcularControl(hab6Heal));
                                        }
                                    }
                                }
                            }
                            impacto = true;
                        }
                        if (impacto)
                        {
                            hab3HabilitiesUsed++;
                            hab8Cd = hab8CdTotal;
                            turnoRestante -= hab8Turn;
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
            case 2:
                if (hab2Cd <= 0 && turnoRestante >= hab2Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(1, hab2Range, hab2Area);
                }
                break;
            case 3:
                if (hab3Cd <= 0 && turnoRestante >= hab3Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(1, hab3Range, hab3Area);
                }
                break;
            case 4:
                if (repetitions4 > 0 && turnoRestante >= hab4Turn)
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
                    MarcarHabilidad(1, hab5Range, hab5Area);
                }
                break;
            case 7:
                if (hab7Cd <= 0 && turnoRestante >= hab7Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(1, hab7Range,hab7Area);
                }
                break;
            case 8:
                if (hab8Cd <= 0 && turnoRestante >= hab8Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab8Range,0);
                }
                break;
        }
    }

    public override void AcabarTurno()
    {

        repetitions1 = hab1Rmax;
        if (hab2Cd != 0)
        {
            hab2Cd--;
        }
        if (hab3Cd != 0)
        {
            hab3Cd--;
        }
        repetitions4 = hab4Rmax;
        if (hab5Cd != 0)
        {
            hab5Cd--;
        }
        if (hab7Cd != 0)
        {
            hab3Cd--;
        }
        if (hab8Cd != 0)
        {
            hab3Cd--;
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
                return "Lanza un pulso de arena a un objetivo realizando " + CalcularDanoMagico(hab1Dmg) + " (s) de daño al objetivo.";
            case 2:
                return "Conjura una bomba de arena de arena a un objetivo realizando " + CalcularDanoMagico(hab2Dmg) + " (s) de daño en área";
            case 3:
                return "Desata una ráfaga de poder que hace " + CalcularDanoMagico(hab3Dmg) + " (S) en área, solo puede usarse tras haber usado 3 habilidades";
            case 4:
                return "Dispara pequeños proyectiles de arena rápidamente a un objetivo, realiza " + CalcularDanoMagico(hab4Dmg) + " (S) de daño por cada golpe. Benevolencia cura " + CalcularControl(hab6Heal/2) + " (C) usando esta habilidad";
            case 5:
                return "Conjura unos vientos malditos que extenúan " + CalcularControl(hab5Exh) + " (C) a los objetivos alcanzados";
            case 6:
                return "Cada vez que se usa una habilidad cura " + CalcularControl(hab6Heal) + " (C) en área a tus aliados";
            case 7:
                return "Hace caer un cúmulo de arena en una zona dañando por " + CalcularDanoMagico(hab7Dmg) + " (s) y debilitando por "+ CalcularControl(hab7Deb) +" (C) a los enemigos";
            case 8:
                return "Desaparece y aparece en la posición seleccionada";
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
                return "Lanza un pulso de arena a un objetivo realizando " + CalcularDanoMagico(hab1Dmg,sinergia) + " (s) de daño al objetivo.";
            case 2:
                return "Conjura una bomba de arena de arena a un objetivo realizando " + CalcularDanoMagico(hab2Dmg, sinergia) + " (s) de daño en área";
            case 3:
                return "Desata una ráfaga de poder que hace " + CalcularDanoMagico(hab3Dmg, sinergia) + " (S) en área, solo puede usarse tras haber usado 3 habilidades";
            case 4:
                return "Dispara pequeños proyectiles de arena rápidamente a un objetivo, realiza " + CalcularDanoMagico(hab4Dmg, sinergia) + " (S) de daño por cada golpe. Benevolencia cura " + CalcularControl(hab6Heal / 2,control) + " (C) usando esta habilidad";
            case 5:
                return "Conjura unos vientos malditos que extenúan " + CalcularControl(hab5Exh,control) + " (C) a los objetivos alcanzados";
            case 6:
                return "Cada vez que se usa una habilidad cura " + CalcularControl(hab6Heal, control) + " (C) en área a tus aliados";
            case 7:
                return "Hace caer un cúmulo de arena en una zona dañando por " + CalcularDanoMagico(hab7Dmg, sinergia) + " (s) y debilitando por " + CalcularControl(hab7Deb, control) + " (C) a los enemigos";
            case 8:
                return "Desaparece y aparece en la posición seleccionada";
            default:
                return null;
        }
    }
}
