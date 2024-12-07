# Beartrap State Machine

## Overview

This project implements a state machine for a beartrap in Unity. The beartrap can be in one of two states: active or inactive. The state machine handles the transitions between these states and the behavior of the beartrap in each state.

## Components

### Beartrap.cs

This script represents the beartrap object in the game. It contains the following key elements:
- **BeartrapStateMachine**: Manages the states of the beartrap.
- **OnCollision**: Event triggered when another collider stays within the beartrap's collider.
- **BoxCollider**: The collider component of the beartrap.
- **CooldowndTime**: The cooldown time before the beartrap can be reactivated.
- **Damage**: The damage dealt by the beartrap.
- **sprites**: Array of sprites representing the beartrap in different states.
- **spriteRenderer**: The sprite renderer component of the beartrap.

### BeartrapStateMachine.cs

This script manages the state transitions of the beartrap. It contains:
- **ReusableData**: Data that can be reused across states.
- **Beartrap**: Reference to the beartrap object.
- **ActiveState**: The active state of the beartrap.
- **InactiveState**: The inactive state of the beartrap.

### BeartrapBaseState.cs

This abstract class defines the base state for the beartrap. It implements the `IState` interface and provides a constructor to initialize the state machine.

### BeartrapActiveState.cs

This script defines the behavior of the beartrap when it is active. Key methods include:
- **Enter**: Sets up the state, including subscribing to the `OnCollision` event and setting the sprite.
- **OnCollisionEnter**: Handles collisions, dealing damage to `IDamageable` objects and transitioning to the inactive state.
- **Exit**: Unsubscribes from the `OnCollision` event.

### BeartrapInactiveState.cs

This script defines the behavior of the beartrap when it is inactive. Key methods include:
- **Enter**: Sets up the state, including resetting the elapsed time and setting the sprite.
- **Update**: Checks if the cooldown time has elapsed and transitions to the active state if it has.

### IState.cs

This interface defines the methods that each state must implement:
- **Enter**: Called when the state is entered.
- **Exit**: Called when the state is exited.
- **Update**: Called every frame to update the state.
- **PhysicsUpdate**: Called every physics update.
- **HandleInput**: Handles input for the state.

## State Transitions

The beartrap starts in the active state. When a collision is detected, it deals damage and transitions to the inactive state. After the cooldown time has elapsed, it transitions back to the active state.

## Bibliographic References

[Insert bibliographic references here]

