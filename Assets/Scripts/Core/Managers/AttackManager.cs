using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AttackManager : MonoBehaviour
{
    internal static AttackManager Instance;
    internal bool AllowAttack { get; set; } = true;

    private void Awake()
    {
        Instance = this;
    }
}
