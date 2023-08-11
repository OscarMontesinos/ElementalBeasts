using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWalkableWall : MonoBehaviour
{
    public CombatManager cManager;
    private void Awake()
    {
    }
    private void Start()
    {
        cManager = CombatManager.Instance;
        cManager.Position(gameObject);
    }
}
