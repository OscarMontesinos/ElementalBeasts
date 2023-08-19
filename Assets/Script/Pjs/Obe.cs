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
    public int hab1Duration;
    public float hab1Dmg;
    public GameObject hab1Trap;
    public ObeTrap hab1CurrentTrap;
    [Header("Hab2")]
    public Hability hab2;
    public int hab2Turn;
    public int hab2CdTotal;
    public int hab2Cd;
    public int hab2Range;
    public int hab2Duration;
    public GameObject hab2Screen;
    public ObeScreen hab2CurrentScreen;
    [Header("Hab3")]
    public Hability hab3;
    public int hab3Turn;
    public int hab3Range;
    public int hab3Area;
    public float hab3Prot;
    ObeLink hab3CurrentLink;
    [Header("Hab4")]
    public Hability hab4;
    public int hab4Turn;
    public int hab4CdTotal;
    public int hab4Cd;
    public int hab4Range;
    public float hab4Dmg;
    public int hab4Rnd;
    public int hab4Des;
    [Header("Hab5")]
    public Hability hab5;
    public int hab5Turn;
    public int hab5CdTotal;
    public int hab5Cd;
    public int hab5Range;
    public int hab5Duration;
    public float hab5Dmg;
    public GameObject hab5Trap;
    public ObeRiot hab5CurrentTrap;
    [Header("Hab6")]
    public Hability hab6;
    bool usingHab6;
    public int hab6Turn;
    public int hab6CdTotal;
    public int hab6Cd;
    public int hab6LastHab;
    [Header("Hab7")]
    public Hability hab7;
    public float hab7Mark;
    public ObeTrack hab7CurrentTrack;
    [Header("Hab8")]
    public Hability hab8;
    public int hab8Turn;
    public int hab8CdTotal;
    public int hab8Cd;
    public int hab8Range;
    public int hab8Rnds;
    public float hab8Dmg;
    public float hab8Deb;
    public override void Awake()
    {
        base.Awake();
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
                    if (CheckRange(UtilsClass.GetMouseWorldPosition() , hab1Range) && CheckWalls(transform.position,UtilsClass.GetMouseWorldPosition()))
                    {
                        if (usingHab6)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6LastHab = 0;
                        }
                        else
                        {
                            hab1CurrentTrap.SetUp();
                            hab1Cd = hab1Duration + hab1CdTotal;
                            turnoRestante -= hab1Turn;
                            hab6LastHab = 1;
                        }
                        MarcarHabilidad(4, 0, 0);
                    }
                    break;
                case 2:
                    if (CheckRange(UtilsClass.GetMouseWorldPosition(), hab2Range) && CheckWalls(transform.position,UtilsClass.GetMouseWorldPosition()))
                    {
                        if (usingHab6)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6LastHab = 0;
                        }
                        else
                        {
                            hab2CurrentScreen.SetUp();
                            hab2Cd = hab2Duration + hab2CdTotal;
                            turnoRestante -= hab2Turn;
                            hab6LastHab = 2;
                        }
                        MarcarHabilidad(4, 0, 0);
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
                                if (hab3CurrentLink != null)
                                {
                                    hab3CurrentLink.End();
                                }
                                ObeLink link = unit.gameObject.AddComponent<ObeLink>();
                                link.BuffStart(this, CalcularControl(hab3Prot), hab3Area);
                                hab3CurrentLink = link;
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        if (usingHab6)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6LastHab = 0;
                        }
                        else
                        {
                            hab6LastHab = 3;
                            turnoRestante -= hab3Turn;
                        }
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 4:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckWalls(transform.position,unit.transform.position))
                            {
                                bool inhibed = false;
                                CastHability(hab4.habilityType, hab4.habilityEffects[0], hab4.habilityRange, hab4.habilityTargetType, hab4.habilityMovement);
                                unit.RecibirDanoMagico(CalcularDanoMagico(hab4Dmg));
                                if(unit.escudo > 0)
                                {
                                    unit.EliminarEscudo();
                                    inhibed = true;
                                }
                                foreach(Buff buff in unit.GetComponents<Buff>())
                                {
                                    if(buff.type == Buff.BuffType.buff)
                                    {
                                        inhibed = true;
                                        buff.End();
                                        break;
                                    }
                                }
                                if (!inhibed)
                                {
                                    ObeInhib inhib = unit.gameObject.AddComponent<ObeInhib>();
                                    inhib.BuffStart(this, hab4Rnd, hab4Des);
                                }
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        if (usingHab6)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6LastHab = 0;
                        }
                        else
                        {
                            hab4Cd = hab4CdTotal;
                            turnoRestante -= hab4Turn;
                            hab6LastHab = 4;
                        }
                    }
                    MarcarHabilidad(4, 0, 0);
                    break;
                case 5:

                    if (CheckRange(UtilsClass.GetMouseWorldPosition(), hab5Range) && CheckWalls(transform.position, UtilsClass.GetMouseWorldPosition()))
                    {
                        if (usingHab6)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6LastHab = 0;
                        }
                        else
                        {
                            hab5CurrentTrap.SetUp();
                            hab5Cd = hab5Duration + hab5CdTotal;
                            turnoRestante -= hab5Turn;
                            hab6LastHab = 5;
                        }
                        MarcarHabilidad(4, 0, 0);
                    }
                    break;
                case 8:
                    foreach (Unit unit in manager.unitList)
                    {
                        if (unit != null)
                        {
                            if (unit.hSelected && CheckAll(unit, unit.transform.position, hab8Range))
                            {
                                CastHability(hab8.habilityType, hab8.habilityEffects[0], hab8.habilityRange, hab8.habilityTargetType, hab8.habilityMovement);
                                unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));
                                if (!unit.gameObject.GetComponent<ObeDeb>())
                                {
                                    ObeDeb deb = unit.gameObject.AddComponent<ObeDeb>();
                                    deb.BuffStart(this, hab8Rnds, CalcularControl(hab8Deb));
                                }
                                else
                                {
                                    if (!unit.gameObject.GetComponent<ObeDeb>().triggered)
                                    {
                                        unit.gameObject.GetComponent<ObeDeb>().Trigger();
                                    }
                                }

                                if (chosenHab1 == 7 || chosenHab2 == 7 || chosenHab3 == 7 || chosenHab4 == 7)
                                {
                                    if (hab7CurrentTrack != null)
                                    {
                                        hab7CurrentTrack.End();
                                    }
                                    if (!unit.gameObject.GetComponent<ObeTrack>())
                                    {
                                        ObeTrack track = unit.gameObject.AddComponent<ObeTrack>();
                                        track.BuffStart(this, CalcularControl(hab7Mark));
                                        hab7CurrentTrack = track;
                                    }
                                }

                                if (hab3CurrentLink != null && hab3CurrentLink.active && CheckAll(unit, hab3CurrentLink.transform.position, 100))
                                {
                                    unit.RecibirDanoMagico(CalcularDanoMagico(hab8Dmg));

                                    if (!unit.gameObject.GetComponent<ObeDeb>())
                                    {
                                        ObeDeb deb = unit.gameObject.AddComponent<ObeDeb>();
                                        deb.BuffStart(this, hab8Rnds, CalcularControl(hab8Deb));
                                    }
                                    else
                                    {
                                        if (!unit.gameObject.GetComponent<ObeDeb>().triggered)
                                        {
                                            unit.gameObject.GetComponent<ObeDeb>().Trigger();
                                        }
                                    }
                                }
                                impacto = true;
                            }
                        }
                    }
                    if (impacto)
                    {
                        if (usingHab6)
                        {
                            hab6Cd = hab6CdTotal;
                            turnoRestante -= hab6Turn;
                            hab6LastHab = 0;
                        }
                        else
                        {
                            hab8Cd = hab8CdTotal;
                            turnoRestante -= hab8Turn;
                            hab6LastHab = 8;
                        }
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


    }

    public override void ShowHability(int hability)
    {
        usingHab6 = false;
        switch (hability)
        {
            case 1:
                if (hab1Cd <= 0 && turnoRestante >= hab1Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(6, hab1Range, 0);
                }
                break;
            case 2:
                if (hab2Cd <= 0 && turnoRestante >= hab2Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = false;
                    MarcarHabilidad(7, hab2Range, 0);
                }
                break;
            case 3:
                if (turnoRestante >= hab3Turn)
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
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(5, hab4Range, 0);
                }
                break;
            case 5:
                if (hab5Cd <= 0 && turnoRestante >= hab5Turn)
                {
                    manager.DestroyShowNodes();
                    castingHability = hability;
                    manager.aliado = false;
                    manager.enemigo = true;
                    MarcarHabilidad(8, hab5Range, 0);
                }
                break;
            case 6:
                if (hab6Cd <= 0 && turnoRestante >= hab6Turn)
                {
                    Hab6ShowHability(hab6LastHab);
                }
                break;
            case 8:
                if (hab8Cd <= 0 && turnoRestante >= hab8Turn)
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
    void Hab6ShowHability(int hability)
    {
        switch (hability)
        {
            case 1:
                manager.DestroyShowNodes();
                castingHability = hability;
                manager.aliado = false;
                manager.enemigo = false;
                MarcarHabilidad(6, hab1Range, 0);
                break;
            case 2:
                manager.DestroyShowNodes();
                castingHability = hability;
                manager.aliado = false;
                manager.enemigo = false;
                MarcarHabilidad(7, hab2Range, 0);
                break;
            case 3:
                manager.DestroyShowNodes();
                castingHability = hability;
                manager.aliado = true;
                manager.enemigo = false;
                MarcarHabilidad(0, hab3Range, 0);
                break;
            case 4:
                manager.DestroyShowNodes();
                castingHability = hability;
                manager.aliado = false;
                manager.enemigo = true;
                MarcarHabilidad(5, hab4Range, 0);
                break;
            case 5:
                manager.DestroyShowNodes();
                castingHability = hability;
                manager.aliado = false;
                manager.enemigo = true;
                MarcarHabilidad(8, hab5Range, 0);
                break;
            case 8:
                manager.DestroyShowNodes();
                castingHability = hability;
                manager.aliado = false;
                manager.enemigo = true;
                MarcarHabilidad(0, hab8Range, 0);
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
            ObeTrap trap = Instantiate(hab1Trap).GetComponent<ObeTrap>();
            hab1CurrentTrap = trap;
        }
        if (forma == 7)
        {
            planeoInvocacion = true;
            manager.habSingle = false;
            rangoMarcador.SetActive(true);
            conoHabilidad.SetActive(false);
            marcadorHabilidad.SetActive(false);
            extensionMesher.SetActive(false);
            rangoMarcador.transform.localScale = new Vector3((rango + 1) * 2 - 1, (rango + 1) * 2 - 1, rangoMarcador.transform.localScale.z);
            ObeScreen screen = Instantiate(hab2Screen).GetComponent<ObeScreen>();
            hab2CurrentScreen = screen;
        }
        if (forma == 8)
        {
            planeoInvocacion = true;
            manager.habSingle = false;
            rangoMarcador.SetActive(true);
            conoHabilidad.SetActive(false);
            marcadorHabilidad.SetActive(false);
            extensionMesher.SetActive(false);
            rangoMarcador.transform.localScale = new Vector3((rango + 1) * 2 - 1, (rango + 1) * 2 - 1, rangoMarcador.transform.localScale.z);
            ObeRiot trap = Instantiate(hab5Trap).GetComponent<ObeRiot>();
            hab5CurrentTrap = trap;
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
                HabCd info1 = new HabCd(hab1Cd, CombatSheetHabImage.HabCdType.cd);
                return info1;
            case 2:
                HabCd info2 = new HabCd(hab2Cd, CombatSheetHabImage.HabCdType.cd);
                return info2;
            case 3:
                HabCd info3 = new HabCd(0, CombatSheetHabImage.HabCdType.none);
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
                HabCd info7 = new HabCd(0, CombatSheetHabImage.HabCdType.none);
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
                return "Coloca una mina que explota por proximidad haciendo " + CalcularDanoMagico(hab1Dmg) + " (S) de daño al objetivo";
            case 2:
                return "Coloca una pantalla translúcida que bloquea el camino";
            case 3:
                return "Vincula a una liado y lo protege " + CalcularControl(hab3Prot) + " (C) si está lo suficientemente cerca";
            case 4:
                return "Lanza un pulso eléctrico que inflige " + CalcularDanoMagico(hab4Dmg) + " (S) y elimina escudos y potenciaciones. Si el objetivo no está escudado o potenciado desorienta " +hab4Des  + " durante " + hab4Rnd + " rondas";
            case 5:
                return "Envía energía inestable a una zona que explota tras pasar una ronda aturdiéndo e infligngiendo " + CalcularDanoMagico(hab5Dmg) + " (S) de daño";
            case 6:
                return "Permite volver a utilizar la anterior habilidad usada";
            case 7:
                return "Marca al último enemigo golpeado, los próximos ataque hacen " + CalcularControl(hab7Mark) + "% (C) de daño aumentado";
            case 8:
                return "Lanza un láser de energía " + CalcularDanoMagico(hab8Dmg) + " (S) de daño, durante " + hab8Rnds + " rondas";
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
                return "Coloca una mina que explota por proximidad haciendo " + CalcularDanoMagico(hab1Dmg, sinergia) + " (S) de daño al objetivo";
            case 2:
                return "Coloca una pantalla translúcida que bloquea el camino";
            case 3:
                return "Vincula a una liado y lo protege " + CalcularControl(hab3Prot, control) + " (C) si está lo suficientemente cerca";
            case 4:
                return "Lanza un pulso eléctrico que inflige " + CalcularDanoMagico(hab4Dmg, sinergia) + " (S) y elimina escudos y potenciaciones. Si el objetivo no está escudado o potenciado desorienta " + hab4Des + " durante " + hab4Rnd + " rondas";
            case 5:
                return "Envía energía inestable a una zona que explota tras pasar una ronda aturdiéndo e infligngiendo " + CalcularDanoMagico(hab5Dmg, sinergia) + " (S) de daño";
            case 6:
                return "Permite volver a utilizar la anterior habilidad usada";
            case 7:
                return "Marca al último enemigo golpeado, los próximos ataque hacen " + CalcularControl(hab7Mark, control) + "% (C) de daño aumentado";
            case 8:
                return "Lanza un láser de energía que inflige " + CalcularDanoMagico(hab8Dmg, sinergia) + " (S) de daño, si tu aliado vinculado está disponible lanza también un laser, si en la misma ronda dos láseres impactan sobre un objetivo se ve debilitado por un " + CalcularControl(hab8Deb, control) + " (C) durante " + hab8Rnds + " rondas";
            default:
                return null;
        }
    }
}
