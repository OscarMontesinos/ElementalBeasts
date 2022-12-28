using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ward : MonoBehaviour
{
    public int rounds;
    public int team;
    public Unit owner;
    public bool DontDestroyOnOwnerNull;

    public virtual void Update()
    {
       
        if (owner == null&& !DontDestroyOnOwnerNull)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>()&& collision.GetComponent<Unit>().team!=team)
        {
            collision.GetComponent<Unit>().revealed++;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() && collision.GetComponent<Unit>().team != team)
        {
            collision.GetComponent<Unit>().revealed--;
        }
    }
}
