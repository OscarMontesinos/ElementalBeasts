using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebuffIndicator : MonoBehaviour
{
    TextMeshProUGUI text;

    public virtual void Activate(float value)
    {
        text.text = value.ToString("F0");
    }
    public virtual void Activate(float value, int curse)
    {
        text.text = value.ToString("F0");
    }
}
