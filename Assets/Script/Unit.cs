using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    [Header("SetUp")]
    public SpawnCell spawnCell;
    [Header("System")]
    public Color32 color;
    float rondas;
    GameObject pj;
    BoxCollider2D collider2D;
    public bool selected;
    public bool hSelected;
    public bool pSelected;
    public bool freelook;
    public int team;
    public bool turno;
    public bool pasar;
    public bool planeoInvocacion;
    public GameObject marcadorHabilidad;
    public GameObject rotadorHabilidad;
    public GameObject conoHabilidad;
    public GameObject extensionMesher;
    public GameObject rangoMarcador;
    GameObject inmortalMarcador;
    GameObject stunnMarcador;
    GameObject imparableMarcador;
    GameObject elegibleMarcador;
    Camera cam;
    public CombatManager manager;
    bool inmortal;
    public bool escondido;
    public LayerMask wallLayer;
    public LayerMask voidLayer;
    public LayerMask unitLayer;
    public bool imparable;
    public int desorientarValue;
    public int slow;
    public bool moving;
    public bool habilityCasted;
    public int revealed;
    


    [Header("Habilidades")]
    public int chosenHab1;
    public int chosenHab2;
    public int chosenHab3;
    public int chosenHab4;
    public int castingHability;


    [Header("Stats")]
    public float sinergiaElemental = 0;
    public float fuerza = 0;
    public float control = 0;
    public float mHp = 0;
    public float resistenciaFisica = 0;
    public float resistenciaMagica = 0;
    public int iniciativa = 0;
    public int maxMovementPoints = 0;
    public int movementPoints = 0;
    public float hp = 0;
    public float escudo = 0;
    public int iniciativaTurno = 0;
    public int turnoRestante = 5;
    public bool stun;
    public bool justStun;
    public bool root;
    public bool vinculo;
    [Header("Bufos o debufos")]
    public float prot = 0;
    public float pot = 0;
    [Header("Owner")]
    public Player owner;
    [Header("UI")]
    public SpriteRenderer teamColor;
    GameObject selectUi;
    GameObject turnoUi;
    Text hpText;
    Text escudoText;
    Text turnoText;
    Text hab1CDText;
    Text hab2CDText;
    Text hab3CDText;
    Text hab4CDText;
    Text atkText;
    Text protText;
    Text slowText;
    Slider hpBar;
    public CombatBeastSheet sheet;

    [Header("Pathfinding")]
    public float speed;
    int currentPathIndex;
    List<Vector3> pathVectorList;
    public MapPathfinder mapPathfinder;

    [Header("HabilitiesData")]
    public Sprite defaultHabSprite;

    public CombatManager GetManager()
    {
        return manager;
    }

    public List<Vector3> GetPathVectorList()
    {
        return pathVectorList;
    }

    public virtual void Awake()
    {
        collider2D =GetComponent<BoxCollider2D>();
        /*hab1CDText = Object.FindObjectOfType<Hab1T>().GetComponent<Text>(); 
        hab2CDText = Object.FindObjectOfType<Hab2T>().GetComponent<Text>();
        hab3CDText= Object.FindObjectOfType<Hab3T>().GetComponent<Text>();
        hab4CDText = Object.FindObjectOfType<Hab4T>().GetComponent<Text>();*/
        marcadorHabilidad = transform.GetChild(3).gameObject;   
        rotadorHabilidad = transform.GetChild(2).gameObject;
        conoHabilidad = transform.GetChild(0).transform.GetChild(0).gameObject;
        extensionMesher = transform.GetChild(2).transform.GetChild(0).gameObject;
        rangoMarcador = transform.GetChild(1).gameObject;
        inmortalMarcador = transform.GetChild(6).gameObject;
        pj = transform.GetChild(5).gameObject;
        teamColor = pj.GetComponent<SpriteRenderer>();
        selectUi = transform.GetChild(4).gameObject;
        turnoUi = pj.transform.GetChild(0).gameObject;
        hpText = pj.transform.GetChild(3).transform.GetChild(0).transform.GetChild(3).GetComponent<Text>();
        escudoText = pj.transform.GetChild(3).transform.GetChild(0).transform.GetChild(4).GetComponent<Text>();
        turnoText = pj.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        hpBar = pj.transform.GetChild(3).transform.GetChild(0).GetComponent<Slider>();
        atkText = pj.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>();
        protText = pj.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>();
        slowText = pj.transform.GetChild(3).transform.GetChild(3).GetComponent<Text>();
        stunnMarcador = transform.GetChild(7).gameObject;
        imparableMarcador = transform.GetChild(8).gameObject;
        elegibleMarcador = transform.GetChild(9).gameObject;


    }
    
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        DontDestroyOnLoad(this);
        object[] instantiationData = info.photonView.InstantiationData;

        Items items = GetComponent<Items>();

        if ((int)instantiationData[0] != items.items.Count - 1)
        {
            items.items[(int)instantiationData[0]].equipado = true;
            items.items[(int)instantiationData[0]].cantidad++;
        }
        if ((int)instantiationData[0] != items.items.Count - 1)
        {
            items.items[(int)instantiationData[1]].equipado = true;
            items.items[(int)instantiationData[1]].cantidad++;
        }

        chosenHab1 = (int)instantiationData[2];
        chosenHab2 = (int)instantiationData[3];
        chosenHab3 = (int)instantiationData[4];
        chosenHab4 = (int)instantiationData[5];

        team = (int)instantiationData[6];

        foreach (Player player in FindObjectsOfType<Player>())
        {
            if (player.team == team)
            {
                player.beastsToPlace.Add(gameObject);

                owner = player;
            }
        }
    }

    public void SetElegibleMarcador(bool value)
    {
        elegibleMarcador.SetActive(value);
    }

    private void Start()
    {
        IniciativaCalc();

        hp = mHp;
        
        hpBar.maxValue = mHp;
        hpBar.value = hpBar.maxValue;
        hpText.text = hp.ToString("F0");
        //iniText.text = iniciativa.ToString("F0"); 
    }
    public virtual void Update()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.00001f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.00001f);
        if (manager==null || mapPathfinder == null || cam == null)
        {

            cam = Object.FindObjectOfType<Camera>();
            mapPathfinder = FindObjectOfType<MapPathfinder>();
            manager = Object.FindObjectOfType<CombatManager>();
        }
        else
        {
            
            teamColor.color = manager.teamColorList[team];
        }
        HandleMovement();
        if (pot<0.1f&& pot>0 || pot> -0.1f && pot < 0)
        {
            pot = 0;
        }
        if (prot < 0.1f && prot > 0 || prot > -0.1f && prot < 0)
        {
            prot = 0;
        }
        if (escondido)
        {
            transform.position = new Vector3(pj.transform.position.x, pj.transform.position.y, 30.27f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)&&turno)
        {
            escondido = !escondido;
        }
        if (imparable)
        {
            stun = false;
            root = false;
            desorientarValue = 0;
            imparableMarcador.SetActive(true);
        }
        if (justStun || imparable)
        {
            imparableMarcador.SetActive(true);
        }
        else
        {
            imparableMarcador.SetActive(false);
        }
        if (stun)
        {
            stunnMarcador.SetActive(true);
        }
        else
        {
            stunnMarcador.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inmortal = !inmortal;
        }
        if (inmortal)
        {
            inmortalMarcador.SetActive(true);
        }
        else
        {
            inmortalMarcador.SetActive(false);
        }

        //Esto pone el turno y actualiza las units para desaparecer cuando se metan en pisos
        turnoText.text = turnoRestante.ToString("F0");
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            freelook = !freelook;
        }


        if (Input.GetMouseButtonDown(1) && manager.casteando && turno)
        {

            manager.casteando = false;
            MarcarHabilidad(4, 0, 0);
            manager.ShowNodesInRange();
        }

               
        

        

        if (hSelected)
        {
            selectUi.SetActive(true);
        }
        else
        {
            selectUi.SetActive(false);
        }


        if (slow != 0)
        {
            slowText.gameObject.SetActive(true);
            slowText.text = "Sp: " + slow;
        }
        else
        {
            slowText.gameObject.SetActive(false);
        }


        if(pot == 0)
        {
            atkText.gameObject.SetActive(false);
        }
        else if(pot>0)
        {
            atkText.gameObject.SetActive(true);
            atkText.color = new Color32(255, 64, 64, 255);
        }
        else
        {
            atkText.gameObject.SetActive(true);
            atkText.color = new Color32(64, 15, 255, 255);
        }


        if (prot == 0)
        {
            protText.gameObject.SetActive(false);
        }
        else if (prot > 0)
        {
            protText.gameObject.SetActive(true);
            protText.color = new Color32(255, 64, 64, 255);
        }
        else
        {
            protText.gameObject.SetActive(true);
            protText.color = new Color32(64, 15, 255, 255);
        }

        /*if (!string.IsNullOrEmpty(valor.text))
        {
            value = float.Parse(valor.text);

        }

        if (!string.IsNullOrEmpty(turnos.text))
        {
            rondas = float.Parse(turnos.text);

        }*/

    }
    #region RPC

    void IniciativaCalc()
    {

        iniciativaTurno = iniciativa + Random.Range(0, 20);
        photonView.RPC("ReplicateIniciativaCalc", RpcTarget.All,iniciativaTurno);
    }

    [PunRPC]
    public void ReplicateIniciativaCalc(int iniciativa)
    {
        iniciativaTurno = iniciativa;
    }

    public void ReplicateTransformCall(float x, float y)
    {
        photonView.RPC("ReplicateTransform", RpcTarget.All,x,y);
    }


    [PunRPC]
    public void ReplicateTransform(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }
    public void UpdateHP()
    {
        photonView.RPC("ReplicateHP", RpcTarget.All,hp);
    }


    [PunRPC]
    public void ReplicateHP(float newHp)
    {
        if(newHp != hp)
        {
            hp = newHp;
            StartCoroutine(SetHpBar());
        }
    }


    [PunRPC]
    protected virtual void StunnRPC(bool value)
    {
        stun = value;
        if (!value)
        {
            justStun = true;
        }

    }
    public void UpdateCell(bool value)
    {
        photonView.RPC("ReplicateUpdateCell", RpcTarget.All, value);
    }
    [PunRPC]
    protected virtual void ReplicateUpdateCell(bool value)
    {

        UpdateCellEnd(value);

    }

    public void UpdateStats()
    {
        photonView.RPC("UpdateEscudoRPC", RpcTarget.All, escudo);
        photonView.RPC("UpdateProtRPC", RpcTarget.All, prot);
        photonView.RPC("UpdatePotRPC", RpcTarget.All, pot);
    }


    [PunRPC]
    public void UpdateEscudoRPC(float newEscudo)
    {
        escudo = newEscudo;
        if (escudo > 0)
        {
            escudoText.text = "(" + escudo.ToString("F0") + ")";
        }
    }
    [PunRPC]
    public void UpdateProtRPC(float newProt)
    {
        prot = newProt;
    }
    [PunRPC]
    public void UpdatePotRPC(float newPot)
    {
        pot = newPot;
    }

    #endregion

    #region Pathfinding
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public List<Pathnode> GetNodesInRange()
    {
        int x;
        int y;
        mapPathfinder.GetPathfinding().GetGrid().GetXY(transform.position, out x, out y);
        Pathnode startingPathnode = mapPathfinder.GetPathfinding().GetNode(x,y);
        List<Pathnode> inRangeNodes = new List<Pathnode>();
        int stepCount = 0;

        inRangeNodes.Add(startingPathnode);

        List<Pathnode> nodeForPrevoiusStep = new List<Pathnode>();
        nodeForPrevoiusStep.Add(startingPathnode);

        while (stepCount < movementPoints)
        {
            List<Pathnode> surrondingNodes = new List<Pathnode>();

            foreach (Pathnode pathnode in nodeForPrevoiusStep)
            {
                    if (pathnode.isWalkable || stepCount == 0)
                    {
                        surrondingNodes.AddRange(Pathfinding.Instance.GetNeighbourList(pathnode));
                    }
                
            }
            inRangeNodes.AddRange(surrondingNodes);
            nodeForPrevoiusStep = surrondingNodes.Distinct().ToList();
            stepCount++;
        }

        return inRangeNodes.Distinct().ToList();
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        List<Vector3> tentativePath = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        if (tentativePath.Count-1 <= movementPoints && !root && turno && tentativePath.Count - 1 > 0)
        {
            UpdateCell(true);
            manager.DestroyShowNodes();
            pathVectorList = tentativePath;

            if (pathVectorList != null && pathVectorList.Count > 1)
            {
                pathVectorList.RemoveAt(0);
            }
        }

    }

    void HandleMovement()
    {
        int x;
        int y;
        mapPathfinder.GetPathfinding().GetGrid().GetXY(transform.position, out x, out y);
        if (pathVectorList != null && pathVectorList.Count!=0)
        {
            moving = true;
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.502f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                transform.position = transform.position + moveDir * speed * Time.deltaTime;

                photonView.RPC("ReplicateTransform", RpcTarget.All, transform.position.x, transform.position.y);
            }
            else
            {
                movementPoints--;
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    public void StopMoving()
    {
        moving = false;
        UpdateCell(false);
        manager.ShowNodesInRange();
        pathVectorList = null;

    }
    public void UpdateCellEnd(bool value)
    {
        mapPathfinder.GetPathfinding().GetGrid().GetXY(transform.position, out int x, out int y);
        if (x != 1000 && y != 1000)
        {
            mapPathfinder.GetPathfinding().GetNode(x, y).SetIsWalkable(value);
        }
    }

   


    
    #endregion

    #region TurnManagment
    public virtual void ActualizarCDUI(int hab1, int hab2, int hab3, int hab4)
    {
        if (turno)
        {
            hab1CDText.text = "Hab 1: " + hab1.ToString();
            hab2CDText.text = "Hab 2: " + hab2.ToString();
            hab3CDText.text = "Hab 3: " + hab3.ToString();
            hab4CDText.text = "Hab 4: " + hab4.ToString();
        }
    }
    public virtual void PrepararTurno()
    {
        sheet.UpdateSheet();
        owner.TurnGived(this);
        if (manager.centrarCamara)
        {
            cam.transform.position = transform.position;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);
        }
        turnoUi.SetActive(true);
        if (justStun)
        {
            justStun = false;
        }
        if (stun)
        {
            justStun = true;
            pasar = true;
            stun = false;
            photonView.RPC("StunnRPC", RpcTarget.AllBuffered, false);
            manager.SiguienteTurno();
        }
        if (desorientarValue > 0)
        {
            turnoRestante -= desorientarValue;
            desorientarValue = 0;
        }
        if (turnoRestante < 0)
        {
            turnoRestante = 0;
        }
        movementPoints = maxMovementPoints -slow;
        mapPathfinder.unitToMove = this;

        manager.ShowNodesInRange();
        turno = true;
    }
    public virtual void AcabarTurno()
    {
        habilityCasted = false;
        owner.beastSelected = null;
        manager.DestroyShowNodes();
        turno = false;
        turnoUi.SetActive(false);
        pasar = true;
        root = false;
        manager.SiguienteTurno();
        MarcarHabilidad(4, 0, 0);
    }
    public void NuevaRonda()
    {
        if (sheet != null)
        {
            sheet.UpdateSheet();
        }
        turnoRestante = 5;
        pasar = false;
        
    }
    public virtual void Desorientar(int value)
    {
        desorientarValue += value;
    }
    #endregion

    #region Calculators
    public virtual float CalcularDanoFisico(float calculo)
    {
        float value = fuerza + pot;
        value *= calculo / 100;
        return value;
    } public virtual float CalcularDanoFisico(float calculo, float fuerza)
    {
        float value = fuerza + pot;
        value *= calculo / 100;
        return value;
    }
    public virtual float CalcularDanoMagico(float calculo)
    {
        float value = sinergiaElemental + pot;
        value *= calculo / 100;
        return value;

    }
    public virtual float CalcularDanoMagico(float calculo, float sinergiaElemental)
    {
        float value = sinergiaElemental + pot;
        value *= calculo / 100;
        return value;

    }
    public virtual float CalcularControl(float calculo)
    {
        float value = control + (pot/2);
        value *= calculo / 100;
        return value;
    }
    public virtual float CalcularControl(float calculo, float control)
    {
        float value = control + (pot/2);
        value *= calculo / 100;
        return value;
    }
    public virtual void Escudo(float value)
    {
        escudo = 0;
        escudo = value;
            escudoText.text = "(" + escudo.ToString("F0") + ")";
        UpdateStats();
    }
    public void EliminarEscudo()
    {
        escudo = 0;
        UpdateStats();
        escudoText.text = "";


    }
    public void AddPot(float value)
    {
        pot = 0;
        pot = value;
        UpdateStats();
    }
    public void AddProt(float value)
    {
        prot = 0;
        prot = value;
        UpdateStats();
    }
    public virtual void RecibirDanoFisico(float value)
    {
        if (!inmortal)
        {
            float calculo = resistenciaFisica + prot;
            if (calculo < 0)
            {
                calculo = 0;
            }
            value -= ((value * ((calculo / (100 + calculo) * 100))) / 100);

            CalculoDeDaño(value);
        }
    }

    public virtual void RecibirDanoMagico(float value)
    {
        if (!inmortal)
        {
            float calculo = resistenciaMagica + prot;
            if (calculo < 0)
            {
                calculo = 0;
            }
            value -= ((value * ((calculo / (100 + calculo) * 100))) / 100);

            CalculoDeDaño(value);
        }
    }

    public void CalculoDeDaño(float value)
    {
        if (value <= 0)
        {
            value = 1;
        }
        if (escudo < value || escudo < 0)
        {
            value -= escudo;
            escudo = 0;
        }
        else
        {
            escudo -= value;
            escudoText.text = "(" + escudo.ToString("F0") + ")";
        }

        hp -= value;

        StartCoroutine(SetHpBar());
        UpdateHP();

    }

    public virtual void Heal(float value)
    {
        hp += value;
        if (hp > mHp)
        {
            hp = mHp;
        }
        StartCoroutine(SetHpBar());
        UpdateHP();
    }

    public virtual void Stunn()
    {
        if (!justStun)
        {
            photonView.RPC("StunnRPC", RpcTarget.AllBuffered, true);
        }
    }

    #endregion

    #region Habilities

    public virtual void ShowHability(int hability)
    {

       
    }

    public bool CheckAll(Unit unit, Vector3 position, int range)
    {
        if( CheckWalls(position) && unit == GetTarget(position) && CheckRange(unit.transform.position, range))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckRange(Vector3 pos, int range)
    {
        int x, y, tx, ty;
        Pathfinding.Instance.GetGrid().GetXY(pos, out tx, out ty);
        Pathfinding.Instance.GetGrid().GetXY(transform.position, out x, out y);

        x -= tx;
        y -= ty;

        if (x < 0)
        {
            x *= -1;
        }
        if (y < 0)
        {
            y *= -1;
        }
        if (x >= y && x <= range)
        {
            return true;
        }
        else if (y > x && y <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckWalls(Vector3 position)
    {
        position -= transform.position;
        if (Physics2D.Raycast(transform.position, position,position.magnitude, wallLayer))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckVoid(Vector3 position)
    {
        position -= transform.position;
        if (Physics2D.Raycast(transform.position, position,position.magnitude, voidLayer))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Unit GetTarget(Vector3 position)
    {
        position -= transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, position, unitLayer) ;
        if (Physics2D.Raycast(transform.position, position, unitLayer))
        {
            return hit.collider.GetComponent<Unit>();
        }
        else
        {
            return null;
        }
    }

   
    public virtual void CastHability(Hability.HabilityType type, Hability.HabilityEffect effect, Hability.HabilityRange rangeType, Hability.HabilityTargetType targetType, Hability.HabilityMovement movement)
    {

        CastHability(type,effect,Hability.HabilityEffect.None, Hability.HabilityEffect.None, rangeType,targetType,movement);
    }
    public virtual void CastHability(Hability.HabilityType type, Hability.HabilityEffect effect1,Hability.HabilityEffect effect2, Hability.HabilityRange rangeType, Hability.HabilityTargetType targetType, Hability.HabilityMovement movement)
    {
        CastHability(type, effect1, effect2, Hability.HabilityEffect.None, rangeType, targetType, movement);
    }
    public virtual void CastHability(Hability.HabilityType type, Hability.HabilityEffect effect1, Hability.HabilityEffect effect2, Hability.HabilityEffect effect3, Hability.HabilityRange rangeType, Hability.HabilityTargetType targetType, Hability.HabilityMovement movement)
    {
        if (!habilityCasted)
        {
            if (slow <= 2)
            {
                movementPoints -= 2;
            }
            else if (slow <= 4)
            {
                movementPoints -= 1;
            }
            habilityCasted = true;
        }
        switch (type)
        {
            case Hability.HabilityType.Basic:
                UseBasic();
                break;
            case Hability.HabilityType.Hability:
                UseHability();
                break;
            case Hability.HabilityType.Innate:
                UseInnate();
                break;
        }
        switch (effect1)
        {
            case Hability.HabilityEffect.Attack:
                UseAttack();
        break;
            case Hability.HabilityEffect.Buff:
                UseBuff();
        break;
            case Hability.HabilityEffect.Debuff:
                UseDebuff();
        break;
            case Hability.HabilityEffect.Heal:
                UseHeal();
        break;
            case Hability.HabilityEffect.Shield:
                UseShield();
        break;
            case Hability.HabilityEffect.Stunn:
                UseStunn();
        break;
            case Hability.HabilityEffect.Trap:
                UseTrap();
        break;
            case Hability.HabilityEffect.Curse:
                UseCurse();
        break;
    }
        switch (effect2)
        {
            case Hability.HabilityEffect.Attack:
                UseAttack();
        break;
            case Hability.HabilityEffect.Buff:
                UseBuff();
        break;
            case Hability.HabilityEffect.Debuff:
                UseDebuff();
        break;
            case Hability.HabilityEffect.Heal:
                UseHeal();
        break;
            case Hability.HabilityEffect.Shield:
                UseShield();
        break;
            case Hability.HabilityEffect.Stunn:
                UseStunn();
        break;
            case Hability.HabilityEffect.Trap:
                UseTrap();
        break;
            case Hability.HabilityEffect.Curse:
                UseCurse();
        break;
    }
        switch (effect3)
        {
            case Hability.HabilityEffect.Attack:
                UseAttack();
        break;
            case Hability.HabilityEffect.Buff:
                UseBuff();
        break;
            case Hability.HabilityEffect.Debuff:
                UseDebuff();
        break;
            case Hability.HabilityEffect.Heal:
                UseHeal();
        break;
            case Hability.HabilityEffect.Shield:
                UseShield();
        break;
            case Hability.HabilityEffect.Stunn:
                UseStunn();
        break;
            case Hability.HabilityEffect.Trap:
                UseTrap();
        break;
            case Hability.HabilityEffect.Curse:
                UseCurse();
        break;
    }
        switch (rangeType)
        {

            case Hability.HabilityRange.Melee:
                UseMelee();
                break;
            case Hability.HabilityRange.Range:
                UseRange();
                break;
        }
        switch (targetType)
        {

            case Hability.HabilityTargetType.Single:
                UseSingle();
                break;
            case Hability.HabilityTargetType.Area:
                UseArea();
                break;
            case Hability.HabilityTargetType.Self:
                UseSelf();
                break;
        }
        switch (movement)
        {

            case Hability.HabilityMovement.Dash:
                UseDash();
                break;
        }
    }

    


    #region type
    public virtual void UseBasic()
    {

    }
    public virtual void UseHability()
    {

    }
    public virtual void UseInnate()
    {

    }
    #endregion

    #region effect
    public virtual void UseAttack()
    {

    }
    public virtual void UseBuff()
    {

    }
    public virtual void UseDebuff()
    {

    }
    public virtual void UseHeal()
    {

    }
    public virtual void UseShield()
    {

    }
    public virtual void UseStunn()
    {

    }
    public virtual void UseTrap()
    {

    }
    public virtual void UseCurse()
    {

    }
    #endregion

    #region rangeType
    public virtual void UseMelee()
    {

    }
    public virtual void UseRange()
    {

    }
    #endregion

    #region targetType
    public virtual void UseSingle()
    {

    }
    public virtual void UseArea()
    {

    }
    public virtual void UseSelf()
    {

    }
    #endregion
    public virtual void UseDash()
    {

    }

    public virtual void Dash(Unit unit, Vector3 newPos)
    {
        UpdateCell(true);
        unit.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        UpdateCell(false);
        manager.Position(gameObject);
        photonView.RPC("ReplicateTransform", RpcTarget.All, transform.position.x, transform.position.y);
    }
    public virtual void DashBehindTarget(Unit unit, Unit targetUnit)
    {
        UpdateCell(true);
        Pathfinding.Instance.GetGrid().GetXY(unit.transform.position, out int x, out int y);
        Pathfinding.Instance.GetGrid().GetXY(targetUnit.transform.position, out int tx, out int ty);
        x -= tx;
        y -= ty;
        if (x != 0)
        {
            if (x > 0)
            {
                tx--;
            }
            else
            {
                tx++;
            }
        }
        if (y != 0)
        {
            if (y > 0)
            {
                ty--;
            }
            else
            {
                ty++;
            }
        }
        if (Pathfinding.Instance.GetNode(tx, ty) == null || !Pathfinding.Instance.GetNode(tx, ty).isWalkable)
        {
            if (x != 0)
            {
                if (x > 0)
                {
                    tx += 2;
                }
                else
                {
                    tx -= 2;
                }
            }
            if (y != 0)
            {
                if (y > 0)
                {
                    ty += 2;
                }
                else
                {
                    ty -= 2;
                }
            }
        }
        unit.transform.position = new Vector3(tx, ty, transform.position.z);
        UpdateCell(false);
        manager.Position(gameObject);
        photonView.RPC("ReplicateTransform", RpcTarget.All, transform.position.x, transform.position.y);

    }
    #endregion

    public IEnumerator SetHpBar()
    {
        yield return new WaitForSeconds(0.1f);
        hpBar.maxValue = mHp;
        
        if (hpBar.value > hp)
        {
            while (hpBar.value > hp +1 && hpBar.value != 0)
            {
                hpText.text = hpBar.value.ToString("F0");
                hpBar.value -= 1 * mHp / 125;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if(hpBar.value < hp)
        {
            while (hpBar.value < hp +1 && hpBar.value != 0)
            {
                hpText.text =  hpBar.value.ToString("F0");

                hpBar.value += 1 * mHp / 125;
                yield return new WaitForSeconds(0.01f);
            }
        }
        hpText.text = hp.ToString("F0");

        if (hp <= 0)
        {
            Die();
        }
        if (sheet != null)
        {
            sheet.UpdateSheet();
        }
    }

    private void OnMouseDown()
    {
        if (owner.giveTurno && !pasar &&photonView.IsMine)
        {
            PrepararTurno();
        }
    }
    public virtual void OnMouseOver()
    {
        if (!owner.settingUnitsUp)
        {
            if (!manager.casteando && photonView.IsMine && !freelook || !manager.casteando && photonView.IsMine && freelook || manager.casteando && photonView.IsMine && freelook)
            {
                selected = true;
            }
            else
            {
                selected = false;
            }

            if (manager.casteando)
            {

                if (manager.aliado && team == manager.singleTeam && manager.habSingle)
                {
                    hSelected = true;
                }
                if (manager.enemigo && team != manager.singleTeam && manager.habSingle)
                {

                    hSelected = true;
                }
            }
            else
            {
                hSelected = false;
            }
        }
    }
    public virtual void OnMouseExit()
    {
        if (!owner.settingUnitsUp )
        {
            if (owner.giveTurno && photonView.IsMine)
            {
                pSelected = false;
            }
            if (manager.casteando && manager.habSingle)
            {
                hSelected = false;
            }
            selected = false;
        }
    }

   
    
    public virtual void MarcarHabilidad(int forma, int rango, int ancho)
    {
        manager.casteando = true;
        collider2D.enabled = false;
        if (forma == 0)
        {
            manager.habSingle = true;
            manager.singleTeam = team;
            conoHabilidad.SetActive(false);
            marcadorHabilidad.SetActive(false);
            rangoMarcador.SetActive(true);
            extensionMesher.SetActive(false);
            rangoMarcador.transform.localScale = new Vector3((rango + 1) * 2 - 1, (rango + 1) * 2 - 1, rangoMarcador.transform.localScale.z);
        }
        else if (forma == 1)//area
        {
            manager.habSingle = false;
            rangoMarcador.SetActive(true);
            conoHabilidad.SetActive(false);
            marcadorHabilidad.SetActive(true);
            extensionMesher.SetActive(false);
            rangoMarcador.transform.localScale = new Vector3((rango + 1) * 2 - 1, (rango + 1) * 2 - 1, rangoMarcador.transform.localScale.z);
            marcadorHabilidad.transform.localScale = new Vector3(ancho + 2, ancho + 2, marcadorHabilidad.transform.localScale.z);
            marcadorHabilidad.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
            marcadorHabilidad.transform.position = new Vector3(marcadorHabilidad.transform.position.x, marcadorHabilidad.transform.position.y, -0.7f);

            if (marcadorHabilidad.transform.position.x > 0 && marcadorHabilidad.transform.position.y > 0)
            {
                marcadorHabilidad.transform.position = new Vector3(marcadorHabilidad.transform.position.x - (marcadorHabilidad.transform.position.x % 1) + 0.5f, marcadorHabilidad.transform.position.y - (marcadorHabilidad.transform.position.y % 1) + 0.5f, marcadorHabilidad.transform.position.z);
            }
            else if (marcadorHabilidad.transform.position.x < 0 && marcadorHabilidad.transform.position.y > 0)
            {
                marcadorHabilidad.transform.position = new Vector3(marcadorHabilidad.transform.position.x - (marcadorHabilidad.transform.position.x % 1) - 0.5f, marcadorHabilidad.transform.position.y - (marcadorHabilidad.transform.position.y % 1) + 0.5f, marcadorHabilidad.transform.position.z);
            }
            else if (marcadorHabilidad.transform.position.x < 0 && marcadorHabilidad.transform.position.y < 0)
            {
                marcadorHabilidad.transform.position = new Vector3(marcadorHabilidad.transform.position.x - (marcadorHabilidad.transform.position.x % 1) - 0.5f, marcadorHabilidad.transform.position.y - (marcadorHabilidad.transform.position.y % 1) - 0.5f, marcadorHabilidad.transform.position.z);
            }
            else if (marcadorHabilidad.transform.position.x > 0 && marcadorHabilidad.transform.position.y < 0)
            {
                marcadorHabilidad.transform.position = new Vector3(marcadorHabilidad.transform.position.x - (marcadorHabilidad.transform.position.x % 1) + 0.5f, marcadorHabilidad.transform.position.y - (marcadorHabilidad.transform.position.y % 1) - 0.5f, marcadorHabilidad.transform.position.z);
            }
        }
        else if (forma == 2)//cono
        {
            manager.habSingle = false;
            conoHabilidad.SetActive(true);
            rangoMarcador.SetActive(false);
            marcadorHabilidad.SetActive(false);
            extensionMesher.SetActive(false);
            if (ancho == 1)
            {
                conoHabilidad.transform.localScale = new Vector3(0.85f, 1.5f + rango, conoHabilidad.transform.localScale.z);
            }
            else
            {
                conoHabilidad.transform.localScale = new Vector3(0.85f + (1.7f * (ancho - 1)), 0.5f + rango, conoHabilidad.transform.localScale.z);
            }
            conoHabilidad.transform.up = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z)) - transform.localPosition;
            conoHabilidad.transform.localEulerAngles = new Vector3(0, 0, conoHabilidad.transform.localEulerAngles.z);
        }
        else if (forma == 3)//Extensión
        {
            manager.habSingle = false;
            conoHabilidad.SetActive(false);
            rangoMarcador.SetActive(false);
            marcadorHabilidad.SetActive(false);
            extensionMesher.SetActive(true);

            extensionMesher.transform.localScale = new Vector3(ancho, rango, extensionMesher.transform.localScale.z);
            rotadorHabilidad.transform.up = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z)) - transform.localPosition;
            rotadorHabilidad.transform.localEulerAngles = new Vector3(0, 0, rotadorHabilidad.transform.localEulerAngles.z);
        }
        else if (forma == 5)//AreaEstática
        {
            manager.habSingle = false;
            rangoMarcador.SetActive(false);
            conoHabilidad.SetActive(false);
            marcadorHabilidad.SetActive(true);
            extensionMesher.SetActive(false);
            marcadorHabilidad.transform.localScale = new Vector3(rango + 2, rango + 2, marcadorHabilidad.transform.localScale.z);
            marcadorHabilidad.transform.position = transform.position;
        }
        else if (forma == 4)
        {
            sheet.UpdateSheet();
            manager.casteando = false;
            manager.habSingle = false;
            conoHabilidad.SetActive(false);
            rangoMarcador.SetActive(false);
            marcadorHabilidad.SetActive(false);
            extensionMesher.SetActive(false);
            collider2D.enabled = true;
            if (turno)
            {
                manager.ShowNodesInRange();
            }
        }
    }
    public virtual Sprite GetHabIcon(int hability)
    {
        return null;
    }
    public virtual string GetHabName(int hability)
    {
        return null;
    }
    public virtual string GetHabDescription(int hability, float sinergia, float fuerza, float control)
    {
        return null;
    }public virtual string GetHabDescription(int hability)
    {
        return null;
    }

    public virtual void Die()
    {
        UpdateCell(true);
            Destroy(gameObject);
        
    }

   
}
