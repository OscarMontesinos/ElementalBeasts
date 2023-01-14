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
                    unidad.sinergiaElemental += item.sinergia * item.cantidad;
                    unidad.fuerza += item.fuerza * item.cantidad; ;
                    unidad.control += item.control * item.cantidad; ;
                    unidad.mHp += item.hp * item.cantidad;
                    unidad.resistenciaFisica += item.rFisica * item.cantidad; ;
                    unidad.resistenciaMagica += item.rMagica * item.cantidad; ;
                    unidad.maxMovementPoints += item.movimiento * item.cantidad; ;
                }
            }
        }
    }

}
