using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWalkableWall : MonoBehaviour
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
