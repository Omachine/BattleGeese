<details>
  <summary>A*</summary>
# A\* Path Finding
```
🚩⬛⬛⬛⬜⬜🟥
⬜⬜🟥🟥⬛⬜🟥
⬜⬜⬜🟥⬜⬛🏁
🟥🟥⬜⬜⬜🟥🟥
```
Project done by [Francisco Oliveira](https://github.com/FranciscoOliveira7).

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
- `LayerMask unwalkableMask;`
- `Vector2 gridWorldSize;`
- `float nodeRadius;`
- `float unwalkablePadding;` (used to make sure enemies don't get stuck by walking in the corners of the obstacles)

Sets the grid x and y count to by dividing the world size by the node diameter.
```cs
void Start() {...}
```

First it creates a grid with the x and y count defined on `Start()`.
Does a sphere collision check with `nodeRadius` + `unwalkablePadding` on each world position and creates a `PathNode` with the unwalkable flag if so.

```cs
void CreateGrid() {...}
```
</details>












<details>
  <summary>State Machine</summary>
# State Machine Overview

Project made by [Gonçalo Moreira](https://github.com/Omachine).

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
## Bibliographic References

[Genshin Impact Movement in Unity | Full Video - Movement System](https://youtu.be/kluTqsSUyN0)


