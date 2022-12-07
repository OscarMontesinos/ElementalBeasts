using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorHabilidad : MonoBehaviour
{
    public Unit unit;
    public int team;
    public bool objetivoAliados; 
    public bool objetivoEnemigo; 
    // Start is called before the first frame update
    public virtual void Start()
    {
        
        team = unit.team;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        objetivoAliados = unit.GetManager().aliado;
        objetivoEnemigo = unit.GetManager().enemigo;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            
            if (objetivoAliados && other.gameObject.GetComponent<Unit>().team == team )
            {
                other.gameObject.GetComponent<Unit>().hSelected = true;
            }
            if (objetivoEnemigo && other.gameObject.GetComponent<Unit>().team != team )
            {
                other.gameObject.GetComponent<Unit>().hSelected = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        try
        {
            other.gameObject.GetComponent<Unit>().hSelected = false;
        }
        catch (NullReferenceException)
        {

        }
    }
}
