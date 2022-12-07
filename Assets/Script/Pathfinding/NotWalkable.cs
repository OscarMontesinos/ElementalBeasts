using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWalkable : MonoBehaviour
{
    CombatManager cManager;

    private void Awake()
    {
        cManager = FindObjectOfType<CombatManager>();
    }
    private void Start()
    {
        cManager.Position(gameObject);
    }
}
