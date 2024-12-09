using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    public void Spawn() {
        if (_enemy != null) Instantiate(_enemy, transform.position, Quaternion.identity, transform.parent);
        
        else Debug.Log("U forgot to set the enemy my guy");
        
        Destroy(gameObject);
    }
}
