using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public Sprite icon;
    public string nombre;
    public bool equipado;
    public int cantidad = 1;
    public float sinergia;
    public float fuerza;
    public float control;
    public float hp;
    public float rFisica;
    public float rMagica;
    public int movimiento;


}
