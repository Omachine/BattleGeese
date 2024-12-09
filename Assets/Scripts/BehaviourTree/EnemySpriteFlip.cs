using BehaviourTree;
using UnityEngine;

public class EnemySpriteFlip
{
    private SpriteRenderer _spriteRenderer;
    private Unit _unit;

    public EnemySpriteFlip(Transform transform)
    {
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _unit = transform.GetComponent<Unit>();
    }

    public void Update()
    {
        if (_unit.rb.velocity.magnitude < 0.1f) return;
        
        _spriteRenderer.flipX = _unit.rb.velocity.x > 0f;
    }
}
