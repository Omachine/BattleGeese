# Battle Geese Game
## Game Scene Order
MainMenuScene -> Comic Beginning -> Lobby -> Level_1 -> Level_2 -> Level_3 -> Ending Comic

## How the game plays out
The player goes through the levels aquiring weapons to fend for themselves and complete the game
## GDD of the game
[Alpha GDD.pdf](https://github.com/user-attachments/files/18069203/Alpha.GDD.pdf)

## AI
<details>
  <summary>A*</summary>
  
# A\* Path Finding
```
🚩⬛⬛⬛⬜⬜🟥
⬜⬜🟥🟥⬛⬜🟥
⬜⬜⬜🟥⬜⬛🏁
🟥🟥⬜⬜⬜🟥🟥
```
Project done by [Francisco Oliveira Nº25979](https://github.com/FranciscoOliveira7).

In this project I created a pathfinding system for the Battle Geese game.
Since the game has no height changes applied to it and it's using a free movement system, I made a 2 dimensional, matrix shaped grid for a pathfinding system.

# :open_file_folder: Scripts
```
📂pathfinding/
└📄Unit.cs
└📄PathFinding.cs
└📄PathGrid.cs
└📄Heap.cs
└📄PathNode.cs
└📄Line.cs
└📄Path.cs
└📄PathRequestManager.cs
```

# :wrench: In-Game Structure
Enemy object:
- `Unit.cs`
A* grid object:
- `PathGrid.cs`
- `PathFinding.cs`
- `PathRequestManager.cs`

# :gear: Functionality
## Path Node
The pathfinding grid is a matrix of nodes that contains:
- If it's walkable
- World position
- G, H and F costs
- Their parent
- x and y coords from the grid
## Path Grid
Assignable values:
- `unwalkableMask`
- `gridWorldSize`: x & y size *(in units)* of the grid
- `nodeRadius`
- `unwalkablePadding`: used to make sure enemies don't get stuck by walking in the corners of the obstacles

Sets the grid x and y count to by dividing the world size by the node diameter.
```cs
void Start() {...}
```
First it creates a matrix of `PathNode` with the x and y count defined on `Start()`.
Does a `unwalkableMask` sphere collision check with `nodeRadius` + `unwalkablePadding` radius on each world position and creates a `PathNode` with the unwalkable flag if it collides.
The grid is generated a __single time__ after the rooms are generated and does not update.
```cs
void CreateGrid() {...}
```
## Unit
- `minPathUpdateTime`
Assignable values:
- `target`
- `speed`
- `turnDst`
- `turnSpeed`

Coroutine
> 1. Waits `minPathUpdateTime` seconds
> 2. Sends a path request to `PathRequestManager`
> 3. `PathRequestManager` further calls `PathFinding` in a seperate cpu thread to improve performance
> 4. `PathFinding` returns a callback with `pathFound` success flag and a `Path`
> 3. Follows the new received path

</details>












<details>
  <summary>State Machine</summary>
  
# State Machine Overview

Project made by [Gonçalo Moreira Nº 25965](https://github.com/Omachine).

  For this project the use of the State machine was made to make debugging easier, improve readability and for it's reusable logic.
  Can be used in traps where they worked in a sequence of states or change depending on the enviroment.

# :open_file_folder: Scripts
```
📂StateMahine/
└📄BeartrapBaseState.cs
└📄IState.cs
└📄StateMachine.cs
📂Beartrap/
└📄BeartrapStateMachine.cs
└📄Beartrap.cs
```

## Working of the State Machine

The state machine consists of the following components:
- `StateMachine`: The core component that manages the current state and handles state transitions.
- `IState`: An interface that defines the methods each state must implement.
- `BaseState`: An abstract class that implements the `IState` interface and provides a base for specific states.

### StateMachine.cs

This script manages the state transitions and the current state of the object. It contains methods to:
- **initialize the state machine**
- **handle input**
- **update the state**
- **change the state**

### IState.cs

This interface defines the methods that each state must implement:
- **Enter**: Called when the state is entered.
- **Exit**: Called when the state is exited.
- **Update**: Called every frame to update the state.
- **PhysicsUpdate**: Called every physics update.
- **HandleInput**: Handles input for the state.

### BaseState.cs

This abstract class provides a base implementation for the `IState` interface. It contains a reference to the state machine and provides a constructor to initialize it.

## Example: Beartrap

The beartrap is an example of an object that uses the state machine to manage its states. The beartrap can be in one of two states: active or inactive. The state machine handles the transitions between these states and the behavior of the beartrap in each state.

### Beartrap.cs

This script represents the beartrap object in the game. It contains the following key elements:
- `BeartrapStateMachine`: Manages the states of the beartrap.
- `OnCollision`: Event triggered when another collider stays within the beartrap's collider.
- `BoxCollider`: The collider component of the beartrap.
- `CooldowndTime`: The cooldown time before the beartrap can be reactivated.
- `Damage`: The damage dealt by the beartrap.
- `sprites`: Array of sprites representing the beartrap in different states.
- `spriteRenderer`: The sprite renderer component of the beartrap.

```cs
void Start(){...}
```
Initiates the first state for this object wich in this case is going to be the ActiveState
```cs
void Update()
{
    BeartrapStateMachine.Update();
}
```
This will run the state machine update

### BeartrapStateMachine.cs

This script manages the state transitions of the beartrap. It contains:
- **Beartrap**: Reference to the beartrap object.
- **ActiveState**: The active state of the beartrap.
- **InactiveState**: The inactive state of the beartrap.

```cs
 public BeartrapStateMachine(Beartrap beartrap){...}
```
The constructor will reference the main script and instantiate each state of the object

### BeartrapBaseState.cs

This abstract class defines the base state for the beartrap. It implements the `IState` interface and provides a constructor for the state machine.


### BeartrapActiveState.cs

This script defines the behavior of the beartrap when it is active. Key methods include:
- **Enter**: Sets up the state, including subscribing to the `OnCollision` event and setting the sprite.
- **OnCollisionEnter**: Handles collisions, dealing damage to `IDamageable` objects and transitioning to the inactive state.
```cs
stateMachine.ChangeState(stateMachine.InactiveState);
```
- **Exit**: Unsubscribes from the `OnCollision` event.

### BeartrapInactiveState.cs

This script defines the behavior of the beartrap when it is inactive. Key methods include:
- **Enter**: Sets up the state, including resetting the elapsed time and setting the sprite.
- **Update**: Checks if the cooldown time has elapsed and transitions to the active state if it has.
```cs
stateMachine.ChangeState(stateMachine.ActiveState);
```
</details>

<details>
  <summary>Behaviour Tree</summary>

  # Behaviour Tree

Project made by [José Ferreira Nº25958](https://github.com/Berna-97).

The Behaviour Tree was made out of the necessity of having dynamic and interesting enemies. Enemies are similar, so they re-use the same behaviours, making this a more pratical method than a state machine.
It is made out of Nodes, which are selected in the Tree through Sequences and Selectors, and each enemy has its own Tree.

# :open_file_folder: Scripts
```
📂BehaviourTree/
└📄Node.cs
└📄Selector.cs
└📄Sequence.cs
└📄Tree.cs
///////////
└📄AppleBT.cs
└📄BrocolliBT.cs
└📄CarotBT.cs
└📄TaskCheckCollision.cs
└📄TaskAttack.cs
└📄TastGoToTarget.cs
└ ...
```

## The way it works

Each enemy has a Tree associated with it, with Sequences and Selectors, that coordinate Tasks.

### Tree

The Tree is the basis, it contains basic information that is to be shared by different tasks, like the enemy's position or health.
It starts with a root node, and then branches to composite/leaf nodes. The leaf nodes do the actions, like attacking and walking. The composite nodes are all that's between the leaves and the root, tasked with choosing the correct leaf to process.

  Tree.cs:
```
        protected Node _root;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
```

  AppleBT.cs (example of a behavior tree in practice)
```
    protected override Node SetupTree()
    {
        unit = GetComponent<Unit>();
        unit.target = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        EnemyHealthComponent healthComponent = GetComponent<EnemyHealthComponent>();
        Flip = new EnemySpriteFlip(transform);

        Node root = new Selector(new List<Node>
        {
                new Sequence(new List<Node>
                {
                    new WaitTask(attackCooldown),
                    new Sequence(new List<Node>{
                        new TaskCheckDistance(unit, attackRange, CheckType.inside),
                        new CheckTargetInSight(unit),
                        new TaskCheckDistance(unit, 1.5f, CheckType.outside),
                        new DashAttackTask(unit, 0.5f, 1f, dashSpeed, _animator),
                    }),
                })
          //  ),
        });
        
        return root;
    }
```

### Sequence

Sequences perform tasks in order, with each one returning either "Success", "Failure", or "Running".
  - If "Failure" is returned, the entire sequence fails and skips the rest of the tasks.
  - If "Success" is returned, the for cicle continues, and the next task is processed.
  - If "Running" is returned, the task is processed again.
  - If every task succeds, the sequence returns "Success"

    Sequence.cs:
```
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
            index = 0;
            state = NodeState.SUCCESS;
            return state;
```
### Selector

Selectors are similar to Sequences, but with a few differences:
  - "Failure" does not stop the selector, it just goes to the next task.
  - "Success" stops the selector.
  - If all tasks fail, the selector returns "Failure".

  Selector.cs:
 ```
            for (int i = index; i < children.Count; i++)
            {
                switch (children[i].Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        index = 0;
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            index = 0;
            state = NodeState.FAILURE;
            return state;
```

### Node

Nodes are the building blocks of the tree. Tasks, selectors, sequences, and even the root are all nodes.
Nodes are attached in a child-parent fashion, for easy access.

  Node.cs:
```
    protected NodeState state;

    public Node parent;
    protected List<Node> children = new List<Node>();

    private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

    public Node()
    {
        parent = null;
    }
    public Node(List<Node> children)
    {
        foreach (Node child in children)
            _Attach(child);
    }

    private void _Attach(Node node)
    {
        node.parent = this;
        children.Add(node);
    }

    public virtual NodeState Evaluate() => NodeState.FAILURE;
```

### Task

Tasks are the leaf nodes in this behaviour tree. They contain the logic to each action in the enemy, including walking, checking range, attacking adn dashing.


StartFollowingTargetTask.cs (example of a simple task):

```
public class StartFollowingTargetTask : Node
{
    private Transform _target;
    private Unit _unit;
    private Animator _animator;

    public StartFollowingTargetTask(Transform target, Unit unit, Animator animator)
    {
        _target = target;
        _unit = unit;
        _animator = animator;
    }

    public override NodeState Evaluate()
    {
        _animator.SetBool("isWalking", true);
        _unit.target = _target;
        _unit.isStopped = false;
        
        state = NodeState.SUCCESS;
        return state;
    }
}
```

</details>

## Bibliographic References

[Genshin Impact Movement in Unity | Full Video - Movement System](https://youtu.be/kluTqsSUyN0)

[A* Path Finding](https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=1)

[AI Behaviour Trees in Unity](https://www.youtube.com/watch?v=aR6wt5BlE-E)
