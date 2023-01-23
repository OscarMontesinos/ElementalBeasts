using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public Unit unidad;
    public List<string> bufos;
    public List<int> rondasBufos;
    int buscador;

   

   
    private void Start()
    {
        unidad = GetComponent<Unit>();
    }
   
    public void Actualizar()
    {
        /*buscador = 0;
        while (buscador > rondasBufos.Count)
        {
            rondasBufos[buscador]--;
            if (rondasBufos[buscador] == 0)
            {
                bufos.Remove(bufos[buscador]);
                rondasBufos.Remove(rondasBufos[buscador]);
            }
        }*/
       

    }

    public void AñadirBufo(string nombre, int rondas)
    {
        foreach (Unit unit in unidad.manager.unitList)
        {
            if (unit.turno)
            {
                bool verificador = false;
                buscador = 0;
                foreach (string bufo in bufos)
                {
                    if (bufo == nombre)
                    {
                        verificador = true;
                    }
                    if (!verificador)
                    {
                        buscador++;
                    }
                }
                if (verificador)
                {
                    rondasBufos[buscador] = rondas;
                }
                else
                {
                    bufos.Add(nombre);
                    rondasBufos.Add(rondas);
                }

            }


        }

    }
    public void EliminarDebufo(int posicion)
    {
        bufos.Remove(bufos[posicion]);
        rondasBufos.Remove(rondasBufos[posicion]);
    }

   /*public void PavoExh(float escaladoDeb, bool aplicar)
    {
        if (aplicar)
        {
            pavoExhValor = escaladoDeb;
        }
        buscador = 0;
        foreach (string nombre in bufos)
        {
            if (nombre == "pavoExh")
            {
                if (rondasBufos[buscador] > 0)
                {
                    pavoExh = true;
                    rondasBufos[buscador]--;
                    if (aplicar)
                    {
                        unidad.pot -= escaladoDeb;
                    }

                }
                else
                {
                    pavoExh = false;
                    EliminarDebufo(buscador);
                    unidad.pot += pavoExhValor;
                    return;
                }
            }
            buscador++;


        }
    }*/





}
