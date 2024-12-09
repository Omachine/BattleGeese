using UnityEngine;
using System;

[Serializable]
public abstract class HatBehaviour : MonoBehaviour
{
    protected Player player;

    private void Awake()
    {
        player = transform.parent.transform.parent.GetComponent<Player>();
    }

    private void Start() => Equip();

    private void OnDestroy() => Unequip();

    protected abstract void Equip();

    protected abstract void Unequip();
}