using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour {

    public string Name { get; private set; }
    private bool nameSet = false;

    public void SetName(string name)
    {
        if (nameSet)
            return;

        Name = name;
        nameSet = true;
    }

    public void PoolObject()
    {
        ObjectPool.Instance.PoolObject(this);
    }

}
