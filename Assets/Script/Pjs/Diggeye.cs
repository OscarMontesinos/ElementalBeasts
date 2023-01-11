using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public Hability hab3;
    public int hab3Turn;
    public int hab3Rmax;
    int repetitions3;
    public int hab3Range;
    public int hab3Max;
    public GameObject hab3Searcher;
    public List<MinerSearcher> searcherList = new List<MinerSearcher>();
    [Header("Hab4")]
    public Hability hab4;
    public int hab4Turn;
    public int hab4CdTotal;
    public int hab4Cd;
    public int hab4Range;
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
    public bool hab6Stage2;
    [Header("Hab7")]
    public Hability hab7;
    public int hab7ExtraTurn;
    [Header("Hab8")]
    public Hability hab8;
    public int hab8Turn;
    public int hab8CdTotal;
    public int hab8Cd;
    public int hab8Range;
    public int hab8Duration;
    public float hab8Dmg;
    public GameObject hab8Trap;
    public DiggeyeTrap hab8CurrentTrap;
    public override void Awake()
    {
        base.Awake();
        Debug.Log("helo?");
        repetitions1 = hab1Rmax;
        repetitions2 = hab2Rmax;
        repetitions3 = hab3Rmax;
        hab4CdTotal++;
        hab5CdTotal++;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // ActualizarCDUI(repetitions1, repetitions2, hab3Cd, hab4Cd);
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
                                if(chosenHab1==7||chosenHab2 == 7 ||chosenHab3 == 7 ||chosenHab4 == 7)
                                {
                                    turnoRestante+=hab7ExtraTurn; 
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
                                CastHability(hab5.habilityType, hab5.habilityEffects[0],  hab5.habilityRange, hab5.habilityTargetType, hab5.habilityMovement);
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
                    if(CheckRange(UtilsClass.GetMouseWorldPosition(), hab8Range))
                    {
                        
                        hab8CurrentTrap.SetUp();
                        hab8Cd = hab8Duration + hab8CdTotal;
                        turnoRestante -= hab8Turn;
                        MarcarHabilidad(4, 0, 0);
                    }
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

        if (hab6Stage2)
        {
            ShowHability(6);
        }

    }

    public override void ShowHability(int hability)
    {
        Debug.Log(hability);
        switch (castingHability)
        {
            case 1:
                if (repetitions1 > 0 && turnoRestante >= hab1Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab1Range, 0);
                    Debug.Log("yo wtf chat");
                }
                break;
            case 2:
                if (repetitions2 > 0 &&  turnoRestante >= hab2Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab2Range, 0);
                }
                break;
            case 3:
                if(repetitions3 > 0 && turnoRestante >= hab3Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab3Range, 0);
                }
                break;
            case 4:
                if(hab4Cd<= 0 && turnoRestante >= hab4Turn)
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
                if(hab5Cd<= 0 && turnoRestante >= hab5Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(0, hab5Range, 0);
                }
                break;
            case 6:
                if(hab6Cd<= 0 && turnoRestante >= hab6Turn)
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
        }
    }

    public override void AcabarTurno()
    {

        repetitions1 = hab1Rmax;
        repetitions2 = hab2Rmax;
        repetitions3 = hab3Rmax;
        if (hab4Cd != 0)
        {
            hab4Cd--;
        }
        if (hab5Cd != 0)
        {
            hab5Cd--;
        }
        castingHability = 0;
        base.AcabarTurno();

    }

    public override void MarcarHabilidad(int forma, int rango, int ancho)
    {
        base.MarcarHabilidad(forma, rango, ancho);
        if (forma == 6)
        {
            planeoInvocacion = true;
            manager.habSingle = false;
            rangoMarcador.SetActive(true);
            conoHabilidad.SetActive(false);
            marcadorHabilidad.SetActive(false);
            extensionMesher.SetActive(false);
            rangoMarcador.transform.localScale = new Vector3((rango + 1) * 2 - 1, (rango + 1) * 2 - 1, rangoMarcador.transform.localScale.z);
            GameObject trap = Instantiate(hab8Trap);
            hab8CurrentTrap= trap.GetComponent<DiggeyeTrap>();
        }
    }
}
    