using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Environment : MonoBehaviour
{
    public EnviromentValuesSO enValueSO;

    protected virtual void Start()
    {
        gameObject.name = enValueSO.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EtkilesimeGir(other);
        }
    }
    protected abstract void EtkilesimeGir(Collider player);
}