using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public Unit unidad;

    public List<Item> items;

    public bool ItemList;

    private void Awake()
    {
        if (!ItemList)
        {
            unidad = GetComponent<Unit>();
            foreach (Item item in items)
            {
                if (item.equipado)
                {
                    unidad.sinergiaElemental += item.sinergia;
                    unidad.fuerza += item.fuerza;
                    unidad.control += item.control;
                    unidad.mHp += item.hp;
                    unidad.resistenciaFisica += item.rFisica;
                    unidad.resistenciaMagica += item.rMagica;
                    unidad.maxMovementPoints += item.movimiento;
                }
            }
        }
    }

}
