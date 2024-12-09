using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] public WeaponDataSO _data;

    private List<WeaponComponent> componentsAlreadyOnWeapon = new();
    private List<WeaponComponent> componentsAddedToWeapon = new();
    private List<Type> componentDependencies = new();

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        if (_data != null)
        {
            GenerateWeapon(_data);
        }
    }

    [ContextMenu("Test pcik")]
    private void Testfwqf()
    {
        _data = PlayerInfo.playerWeapon1;
        GenerateWeapon(_data);
    }

    [ContextMenu("Test Generate")]
    private void TestGeneration()
    {
        if (_data != null)
        {
            GenerateWeapon(_data);
            _weapon.Equip();
        }
    }

    public void GenerateWeapon(WeaponDataSO data)
    {
        if (data == null)
        {
            Debug.LogWarning("Weapon data is not assigned.");
            return;
        }

        if (_weapon == null)
        {
            Debug.LogWarning("Weapon is not assigned.");
            return;
        }

        _weapon.SetData(data);

        componentsAlreadyOnWeapon.Clear();
        componentsAddedToWeapon.Clear();
        componentDependencies.Clear();

        componentsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
        componentDependencies = data.GetAllDependencies();

        foreach (var dependency in componentDependencies)
        {
            if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency.GetType()))
                continue;

            var weaponComponent =
                componentsAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

            if (weaponComponent == null)
            {
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
            }

            weaponComponent.Init();
            componentsAddedToWeapon.Add(weaponComponent);
        }

        var componentsToRemove = componentsAlreadyOnWeapon.Except(componentsAddedToWeapon);

        foreach (var weaponComponent in componentsToRemove)
        {
            Destroy(weaponComponent);
        }
        
        _animator.runtimeAnimatorController = data.AnimatorController;
    }
}
