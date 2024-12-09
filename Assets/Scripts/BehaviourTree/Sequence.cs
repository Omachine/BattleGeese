using System.Collections.Generic;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        private int index;
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            // bool anyChildIsRunning = false;

            for (int i = index; i < children.Count; i++)
            {
                switch (children[i].Evaluate())
                {
                    case NodeState.FAILURE:
                        index = 0;
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        index = i;
                        return NodeState.RUNNING;
                        // anyChildIsRunning = true;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            // foreach (Node node in children)
            // {
            //     switch (node.Evaluate())
            //     {
            //         case NodeState.FAILURE:
            //             state = NodeState.FAILURE;
            //             return state;
            //         case NodeState.SUCCESS:
            //             continue;
            //         case NodeState.RUNNING:
            //             anyChildIsRunning = true;
            //             continue;
            //         default:
            //             state = NodeState.SUCCESS;
            //             return state;
            //     }
            // }

            // state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            index = 0;
            state = NodeState.SUCCESS;
            return state;
        }

    }
}