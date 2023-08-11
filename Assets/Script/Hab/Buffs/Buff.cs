using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public Unit unit;
    public int rounds;
    public BuffType type;
    public enum BuffType
    {
        buff, debuff
    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }
    public virtual void BuffUpdate()
    {
        rounds--;
        if (rounds == 0)
        {
            End();
        }
    }
    public virtual void End()
    {

        Destroy(this);
    }
}
