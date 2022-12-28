using CodeMonkey.Utils;
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
    public override void Awake()
    {
        base.Awake();
        repetitions1 = hab1Rmax;
        repetitions2 = hab2Rmax;
        repetitions3 = hab3Rmax;
        hab4CdTotal++;
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
                            Pathfinding.Instance.GetGrid().GetXY(UtilsClass.GetMouseWorldPosition(),out x, out y);
                            if (CheckVoid(UtilsClass.GetMouseWorldPosition()) && CheckRange(UtilsClass.GetMouseWorldPosition(), hab4Range) && Pathfinding.Instance.GetNode(x,y).isWalkable)
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
                    manager.Position(gameObject);
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
            case 3:
                if(repetitions3 > 0 && turnoRestante >= hab3Turn)
                {
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab3Range, 0);
                }
                break;
            case 4:
                if(hab4Cd<= 0 && turnoRestante >= hab4Turn)
                {
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(0, hab4Range, 0);
                }
                break;
        }
    }

    public override void AcabarTurno()
    {
        base.AcabarTurno();

        repetitions1 = hab1Rmax;
        repetitions2 = hab2Rmax;
        repetitions3 = hab3Rmax;
        if (hab4Cd != 0)
        {
            hab4Cd--;
        }
        castingHability = 0;

    }
}
    