using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeScreen : ObjetoInvocado
{
    Obe obe;
    float dano;
    public List<DynamicWall> dynamicWalls;
    public override void Start()
    {
        base.Start();
        foreach (Unit unit in manager.unitList)
        {
            if (unit != null)
            {
                if (unit.turno == true)
                {
                    unidad = unit;
                }
            }
        }
        obe = unidad.GetComponent<Obe>();
        rondasDuracion = (obe.hab2Duration * CombatManager.Instance.unitList.Count);
    }
   
    public override void SetUp()
    {
        foreach(DynamicWall wall in dynamicWalls)
        {
            wall.SetNotWalkable();
        }
        base.SetUp();
    }
    public override void Die()
    {
        foreach (DynamicWall wall in dynamicWalls)
        {
            wall.SetWalkable();
        }
        base.Die();
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Unit>() && !planning && obe.turno && collision.gameObject.GetComponent<Unit>().team != team)
        {
            obe.turnoRestante += obe.hab2Turn;
            obe.hab2Cd = 0;
            Destroy(gameObject);
        }
    }
}
