using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockable
{
    bool IsLocked { get; }
    Vector3 Position { get; }

    Vector2[] GetVertices();
    void OnPickup();
    void OnLock();

    void OnLockAttempt();

    
}
