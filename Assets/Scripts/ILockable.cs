using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockable
{
    public bool IsInBin { get; }
    public bool IsLocked { get; }
    public Vector3 Position { get; }

    public Collider2D Collider { get; }
    public void OnLock();

   public void OnLockAttempt();

    public void OnLockFailedAttempt();


}
