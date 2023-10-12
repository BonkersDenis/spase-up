using Match3;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    protected GamePiece piece;
  
    private List<GameObject> frozenObjects = new List<GameObject>();
    public bool IsBeingCleared { get; private set; }

    private void Awake()
    {
        piece = GetComponent<GamePiece>();
    }

    public virtual void FreezeObject(GameObject obj)
    {
        frozenObjects.Add(obj);
    }

    public virtual void UnfreezeObject(GameObject obj)
    {
        if (frozenObjects.Contains(obj))
        {
            frozenObjects.Remove(obj);
        }
    }

    public virtual void Clear(GameObject obj = null)
    {
        if (obj != null)
        {
            UnfreezeObject(obj);
        }
        else
        {
            foreach (GameObject go in frozenObjects)
            {
                UnfreezeObject(go);
            }
        }

        IsBeingCleared = true; // Set IsBeingCleared to true when clearing objects
    }
}