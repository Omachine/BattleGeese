using UnityEngine;

namespace BehaviourTree
{
    public enum CheckType
    {
        inside,
        outside,
    }
    public class TaskCheckDistance : Node
    {
        private Unit _unit;
        private float _distanceThreshold;
        private CheckType _checkType;

        public TaskCheckDistance(Unit unit, float distanceThreshold, CheckType checkType = CheckType.inside)
        {
            _unit = unit;
            _distanceThreshold = distanceThreshold;
            _checkType = checkType;
        }

        public override NodeState Evaluate()
        {
            float distanceToTarget = Vector3.Distance(_unit.transform.position, _unit.target.position);
            
            if (_checkType == CheckType.inside)
                return distanceToTarget > _distanceThreshold ? NodeState.FAILURE : NodeState.SUCCESS;
            return distanceToTarget > _distanceThreshold ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}
