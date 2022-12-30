using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoInvocado : MonoBehaviour
{
    public bool planning = true;
    public bool aliado;
    public bool enemigo;
    public int rondasDuracion;
    public int rondaCreada;
    public int rondasMax;
    public CombatManager manager;
    public Unit unidad;
    public Camera cam;
    public List<Unit> objetivos;
    public int team;
    public GameObject objeto;
    public bool activatedThisRound;
    public TextMeshProUGUI indicadorRondas;

    float value;
    int rondas;

    public bool activacionPasiva;
    public bool seguirUnidad;
    public bool starthit;
    public bool enterHit;
    public bool endHit;
    public bool onlyOnce;
    public bool contarRondas = true;
    int muertosPrevios;

    public bool root;
    // Start is called before the first frame update
    public virtual void Start()
    {
        objeto = transform.GetChild(0).gameObject;
        cam = FindObjectOfType<Camera>();
        manager = FindObjectOfType<CombatManager>();

        foreach (Unit unitC in manager.unitList)
        {
            if (unidad == null)
            {

                if (unitC != null)
                {
                    if (unitC.turno)
                    {
                        unidad = unitC.GetComponent<Unit>();
                    }
                }

            }
        }

        team = unidad.team;

        rondaCreada = (manager.unitList.Count - manager.muertos) * rondasDuracion;
        rondasDuracion = rondaCreada;
        rondasDuracion++;

        rondasMax = rondasDuracion;
        muertosPrevios = manager.muertos;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (indicadorRondas != null)
        {
            indicadorRondas.text = rondasDuracion.ToString();
        }
        if (unidad == null)
        {
            Destroy(gameObject);
        }
        if (seguirUnidad)
        {
            transform.position = new Vector3(unidad.transform.position.x, unidad.transform.position.y, unidad.transform.position.z - 0.05f);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.00001f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.00001f);

        if (unidad.planeoInvocacion && planning)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -0.1f - cam.transform.position.z));
        }

        if (Input.GetMouseButtonDown(1) && planning)
        {
            Destroy(gameObject);
            unidad.planeoInvocacion = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && planning)
        {
            Rotar(1);
        }
        if (Input.GetKeyDown(KeyCode.E) && planning)
        {
            Rotar(-1);
        }
        if (!seguirUnidad)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.7f);
        }
        if (transform.position.x > 0 && transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x - (transform.position.x % 1) + 0.5f, transform.position.y - (transform.position.y % 1) + 0.5f, transform.position.z);
        }
        else if (transform.position.x < 0 && transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x - (transform.position.x % 1) - 0.5f, transform.position.y - (transform.position.y % 1) + 0.5f, transform.position.z);
        }
        else if (transform.position.x < 0 && transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x - (transform.position.x % 1) - 0.5f, transform.position.y - (transform.position.y % 1) - 0.5f, transform.position.z);
        }
        else if (transform.position.x > 0 && transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x - (transform.position.x % 1) + 0.5f, transform.position.y - (transform.position.y % 1) - 0.5f, transform.position.z);
        }

        /*if (unidad.freelook)
        {
            objeto.gameObject.SetActive(false);
        }
        else
        {
            objeto.gameObject.SetActive(true);
        }*/
    }

    public virtual void SetUp()
    {
        planning = false;
        unidad.planeoInvocacion = false;
        unidad.GetManager().invocaciones.Add(this);
        foreach (Unit unit in objetivos)
        {
            if (unit != null)
            {
                Golpear(unit);
            }
        }
    }
    public virtual void Actualizar()
    {
        if (contarRondas) 
        {
            if(muertosPrevios < manager.muertos)
            {
                int multiplicador = rondasDuracion / manager.unitList.Count;
                rondasDuracion -= (manager.muertos - muertosPrevios) * (multiplicador+1);
                muertosPrevios = manager.muertos;
            }
            rondasDuracion--;

        }
        if (rondasDuracion <= 0)
        {
            
            Destroy(gameObject);
        }
    }

    public virtual void Golpear(Unit objetivo)
    {
    }

    public virtual void Desgolepar(Unit objetivo)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Unit>() && !planning)
        {
            if (collision.gameObject.GetComponent<Unit>().team == team && aliado || collision.gameObject.GetComponent<Unit>().team != team && enemigo)
            {
                objetivos.Add(collision.GetComponent<Unit>());
                if (!unidad.planeoInvocacion)
                {
                    Golpear(collision.gameObject.GetComponent<Unit>());
                }
            }
        }
    }


    public virtual void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Unit>() && !planning)
        {

            bool esta = false;
            foreach (Unit unit in objetivos)
            {
                if (unit != null)
                {
                    if (unit.gameObject == collision.gameObject)
                    {
                        esta = true;
                    }
                }
            }
            if (collision.gameObject.GetComponent<Unit>().team == team && aliado && !esta || collision.gameObject.GetComponent<Unit>().team != team && enemigo && !esta)
            {
                objetivos.Add(collision.GetComponent<Unit>());
                if (!unidad.planeoInvocacion)
                {
                    Golpear(collision.gameObject.GetComponent<Unit>());
                }
            }

        }
        
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Unit>() && !planning)
        {
          
            if (collision.gameObject.GetComponent<Unit>().team == team && aliado  || collision.gameObject.GetComponent<Unit>().team != team && enemigo )
            {

                objetivos.Remove(collision.GetComponent<Unit>());
                if (!unidad.planeoInvocacion)
                {
                    Desgolepar(collision.gameObject.GetComponent<Unit>());
                }
            }
        }
    }

    /*private void OnMouseDrag()
    {
        if(Input.GetMouseButton(0) && !planning && unidad.turno && !seguirUnidad)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -0.1f - cam.transform.position.z));
        }
    }*/
    public void Rotar(int multiplier)
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 45*multiplier);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
