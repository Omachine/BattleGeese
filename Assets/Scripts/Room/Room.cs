using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public bool _startActive;
    [HideInInspector] public bool isInBattle = false;
    public event Action<bool> OnLock;
    private GameObject enemies;
    private EnemySpawner[] _enemySpawners;
    private bool isCleared = true;
    private PathGrid pathGrid;

    private void Start()
    {
        _enemySpawners = GetComponentsInChildren<EnemySpawner>();

        enemies = transform.Find("Enemies").gameObject;
        isCleared = CountEnemiesAlive() == 0; // Set isCleared if there are no enemies
        EnemyHealthComponent.OnEnemyDeath += OnEnemyDeath;
        pathGrid = FindObjectOfType<PathGrid>();
        if (pathGrid != null) pathGrid.OnGridGenerated += OnGridGenerated;
    }

    private void OnGridGenerated()
    {
        if (!_startActive) Deactivate();
        pathGrid.OnGridGenerated -= OnGridGenerated;
    }

    private void OnEnemyDeath()
    {
        // If the room is not active, just do nothing
        if (!isInBattle) return;

        if (CountEnemiesAlive() == 1) UnlockRoom();
    }

    [ContextMenu("test count")]
    private int CountEnemiesAlive() {
        Debug.Log("EnemiesAlive: " + enemies.transform.childCount);
        return enemies.transform.childCount;
    }

    private void UnlockRoom()
    {
        Debug.Log("All enemies destroyed!");
        isInBattle = false;
        isCleared = true;
        OnLock?.Invoke(false);
    }

    public void StartBattle()
    {
        if (isCleared || isInBattle) return;

        isInBattle = true;

        OnLock?.Invoke(true);

        StartCoroutine(nameof(SpawnEnemies));
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (var enemySpawner in _enemySpawners)
        {
            enemySpawner.Spawn();
            yield return new WaitForSeconds(0.4f);
        }
    }

// I guess
    public void Activate() => gameObject.SetActive(true);
    public void Deactivate() => gameObject.SetActive(false);
}
