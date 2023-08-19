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
    public int hab1Area;
    public float hab1Dmg;
    [Header("Hab2")]
    public Hability hab2;
    public int hab2Turn;
    public int hab2CdTotal;
    public int hab2Cd;
    public int hab2Ext;
    public int hab2Range;
    public int hab2Push;
    public float hab2Dmg;
    [Header("Hab3")]
    public Hability hab3;
    public int hab3Turn;
    public int hab3MaxCharges;
    int hab3Charges;
    public int hab3Range;
    public int hab3Area;
    public float hab3Dmg;
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
    public int hab5Rnds;
    [Header("Hab6")]
    public Hability hab6;
    public int hab6Area;
    public float hab6Hlth;
    [Header("Hab7")]
    public Hability hab7;
    public int hab7Turn;
    public int hab7CdTotal;
    public int hab7Cd;
    public int hab7Range;
    public int hab7Area;
    public int hab7Slow;
    public int hab7Rnds;
    public float hab7Dmg;
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
        repetitions4 = hab4Rmax;
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
                                if(hab3Charges < 3)
                                {
                                    hab3Charges++;
                                }
                                unit.RecibirDanoMagico(CalcularDanoMagico(hab1Dmg));
                                foreach (Collider2D colldier in Physics2D.OverlapBoxAll(unit.transform.position, new Vector2(hab1Area, hab1Area), 0, unitLayer))
                                {
                                    Unit unit_ = colldier.GetComponent<Unit>();
                                    if (unit_.team != team && unit_ != unit)
                                    {
                                        unit_.RecibirDanoMagico(CalcularDanoMagico(hab1Dmg));
                                    }
                                }
                                CheckHab6();
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
                                if (unit.hSelected && CheckWalls(transform.position,unit.transform.position))
                                {
                                    if (hab3Charges < 3)
                                    {
                                        hab3Charges++;
                                    }
                                    CastHability(hab2.habilityType, hab2.habilityEffects[0], hab2.habilityRange, hab2.habilityTargetType, hab2.habilityMovement);
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab2Dmg));
                                    PushTarget(unit, hab2Push);
                                CheckHab6();
                                impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab2Cd = hab2CdTotal;
                            turnoRestante -= hab2Turn;
                        } 
                    
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 3:
                    if (CheckWalls(transform.position, UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab2Range))
                    {
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckWalls(UtilsClass.GetMouseWorldPosition(), unit.transform.position))
                                {
                                    CastHability(hab3.habilityType, hab3.habilityEffects[0], hab3.habilityRange, hab3.habilityTargetType, hab3.habilityMovement);
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab3Dmg));
                                    CheckHab6();
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            turnoRestante -= hab3Turn;
                            hab3Charges = 0;
                        }
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
                                if (hab3Charges < 3)
                                {
                                    hab3Charges++;
                                }
                                CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);
                                unit.RecibirDanoMagico(CalcularDanoMagico(hab4Dmg));
                                CheckHab6();
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        repetitions4--;
                        turnoRestante -= hab4Turn;
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 5:
                    if (CheckWalls(transform.position, UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab5Range))
                    {
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckWalls(UtilsClass.GetMouseWorldPosition(),unit.transform.position))
                                {
                                    if (hab3Charges < 3)
                                    {
                                        hab3Charges++;
                                    }
                                    CastHability(hab5.habilityType, hab5.habilityEffects[0], hab5.habilityRange, hab5.habilityTargetType, hab5.habilityMovement);
                                    StorminnExh storminExh = unit.gameObject.AddComponent<StorminnExh>();
                                    storminExh.BuffStart(this, hab5Rnds, CalcularControl(hab5Exh));
                                    CheckHab6();
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab5Cd = hab5CdTotal;
                            turnoRestante -= hab5Turn;
                        }
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 7:
                    if (CheckWalls(transform.position, UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab7Range))
                    {
                        foreach (Unit unit in manager.unitList)
                        {
                            if (unit != null)
                            {
                                if (unit.hSelected && CheckWalls(UtilsClass.GetMouseWorldPosition(), unit.transform.position))
                                {
                                    if (hab3Charges < 3)
                                    {
                                        hab3Charges++;
                                    }
                                    CastHability(hab7.habilityType, hab7.habilityEffects[0], hab7.habilityRange, hab7.habilityTargetType, hab7.habilityMovement);
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab7Dmg));
                                    StorminnSlow storminSlow = unit.gameObject.AddComponent<StorminnSlow>();
                                    storminSlow.BuffStart(this, hab7Rnds, hab7Slow);
                                    CheckHab6();
                                    impacto = true;
                                }
                            }
                        }
                        if (impacto)
                        {
                            hab7Cd = hab7CdTotal;
                            turnoRestante -= hab7Turn;
                        }
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 8:
                    int x;
                    int y;
                    Pathfinding.Instance.GetGrid().GetXY(UtilsClass.GetMouseWorldPosition(), out x, out y);
                    if (Pathfinding.Instance.GetNode(x, y).isWalkable)
                    {
                        if (hab3Charges < 3)
                        {
                            hab3Charges++;
                        }
                        CastHability(hab8.habilityType, hab8.habilityEffects[0], hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);
                        Dash(this, UtilsClass.GetMouseWorldPosition());
                        CheckHab6();
                        impacto = true;
                    }
                    if (impacto)
                    {
                        hab8Cd = hab8CdTotal;
                        turnoRestante -= hab8Turn;
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


        if (castingHability == 2 && manager.casteando == true && turno)
        {
            ShowHability(2);
        }
        if (castingHability == 3 && manager.casteando == true && turno)
        {
            ShowHability(3);
        }
        if (castingHability == 5 && manager.casteando == true && turno)
        {
            ShowHability(5);
        }
        if (castingHability == 7 && manager.casteando == true && turno)
        {
            ShowHability(7);
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
                    MarcarHabilidad(3, hab2Range, hab2Ext);
                }
                break;
            case 3:
                if (hab3Charges >= hab3MaxCharges && turnoRestante >= hab3Turn)
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
                    MarcarHabilidad(1, hab7Range, hab7Area);
                }
                break;
            case 8:
                if (hab8Cd <= 0 && turnoRestante >= hab8Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab8Range, 0);
                }
                break;
        }
    }

    public override void AcabarTurno()
    {
        repetitions1 = hab1Rmax;
        repetitions4 = hab4Rmax;
        if (hab2Cd != 0)
        {
            hab2Cd--;
        }
        if (hab5Cd != 0)
        {
            hab5Cd--;
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

    void CheckHab6()
    {
        if (chosenHab1 == 6 || chosenHab2 == 6 || chosenHab3 == 6 || chosenHab4 == 6)
        {
            foreach (Collider2D colldier2 in Physics2D.OverlapBoxAll(transform.position, new Vector2(hab1Area, hab1Area), 0, unitLayer))
            {
                Unit unit_ = colldier2.GetComponent<Unit>();
                Debug.Log(unit_);
                if (unit_.team == team && unit_ != this)
                {
                    unit_.Heal(CalcularControl(hab6Hlth));
                }
                else if (unit_ == this)
                {
                    unit_.Heal(CalcularControl(hab6Hlth / 2));
                }
            }
        }
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
                HabCd info3 = new HabCd(hab3MaxCharges - hab3Charges, CombatSheetHabImage.HabCdType.cd);
                return info3;
            case 4:
                HabCd info4 = new HabCd(repetitions4, CombatSheetHabImage.HabCdType.repetitions);
                return info4;
            case 5:
                HabCd info5 = new HabCd(hab5Cd, CombatSheetHabImage.HabCdType.cd);
                return info5;
            case 6:
                HabCd info6 = new HabCd(0, CombatSheetHabImage.HabCdType.none);
                return info6;
            case 7:
                HabCd info7 = new HabCd(hab7Cd, CombatSheetHabImage.HabCdType.cd);
                return info7;
            case 8:
                HabCd info8 = new HabCd(hab8Cd, CombatSheetHabImage.HabCdType.cd);
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
                return "Lanza un pulso de arena a un enemigo que inflige " + CalcularDanoMagico(hab1Dmg) + " (S) de daño alrededor del objetivo.";
            case 2:
                return "Lanza un pulso de viento que inflije " + CalcularDanoMagico(hab2Dmg) + " (S) de daño, empujando " + hab2Push + " casillas";
            case 3:
                return "Desata una tormenta que inflije " + CalcularDanoMagico(hab3Dmg) + " (S) en área, solo puede usarse esta habilidad tras usar " + hab3MaxCharges + " habilidades";
            case 4:
                return "Lanza un proyectil de arena a cambio de muy poco turno que inflije " + CalcularDanoMagico(hab4Dmg) + " (S) de daño";
            case 5:
                return "Invoca vientos en espiral cargados de energía negativa que extenúan " + CalcularControl(hab5Exh) + " (C)";
            case 6:
                return "Al usar una habilidad cura alrededor tuyo " + CalcularControl(hab6Hlth) + " (C) de vida. Este valor se reduce a la mitad para el usuario";
            case 7:
                return "Hace caer un cumulo de arena que inflije " + CalcularDanoMagico(hab7Dmg) + " (S) de daño y ralentiza " + hab7Slow;
            case 8:
                return "Se teletransporta a la ubicación indicada";
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
                return "Lanza un pulso de arena a un enemigo que inflige " + CalcularDanoMagico(hab1Dmg, sinergia) + " (S) de daño alrededor del objetivo.";
            case 2:
                return "Lanza un pulso de viento que inflije " + CalcularDanoMagico(hab2Dmg, sinergia) + " (S) de daño, empujando " + hab2Push + " casillas";
            case 3:
                return "Desata una tormenta que inflije " + CalcularDanoMagico(hab3Dmg, sinergia) + " (S) en área, solo puede usarse esta habilidad tras usar " + hab3MaxCharges + " habilidades";
            case 4:
                return "Lanza un proyectil de arena a cambio de muy poco turno que inflije " + CalcularDanoMagico(hab4Dmg, sinergia) + " (S) de daño";
            case 5:
                return "Invoca vientos en espiral cargados de energía negativa que extenúan " + CalcularControl(hab5Exh, control) + " (C)";
            case 6:
                return "Al usar una habilidad cura alrededor tuyo " + CalcularControl(hab6Hlth, control) + " (C) de vida. Este valor se reduce a la mitad para el usuario";
            case 7:
                return "Hace caer un cumulo de arena que inflije " + CalcularDanoMagico(hab7Dmg, sinergia) + " (S) de daño y ralentiza " + hab7Slow;
            case 8:
                return "Se teletransporta a la ubicación indicada";
            default:
                return null;
        }
    }

    private void OnDrawGizmos()
    {
       //Gizmos.DrawWireCube(transform.position, new Vector3(hab6Area, hab6Area, hab6Area));
       //Gizmos.DrawWireCube(transform.position, new Vector3(hab1Area, hab1Area, hab1Area));
    }
}
