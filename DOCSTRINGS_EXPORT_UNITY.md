# Docstrings and Comments Export

> Equivalent XML doc-comment content for the Unity C# project.

## BoardManager.cs

### Class: `BoardManager`
```text
Manage the chessboard grid, instantiate tile prefabs, and assign board coordinates.
```

### Method: `BoardManager.Start`
```text
Initialize the board manager at runtime. The current implementation keeps board generation disabled in Start.
```

### Method: `BoardManager.GenBoard`
```text
Generate an 8x8 chessboard by instantiating alternating light and dark tiles, positioning them in world space, and assigning logical coordinates to each Tile.
```

## CameraShake.cs

### Class: `CameraShake`
```text
Handle camera shake and recoil feedback when the shotgun fires.
```

### Method: `CameraShake.Awake`
```text
Cache the shotgun weapon reference before the shake effect is used.
```

### Method: `CameraShake.Shake`
```text
Apply a short randomized camera shake, then settle the camera with a recoil offset opposite to the weapon aim direction.
```

## Enemy.cs

### Class: `Enemy`
```text
Represent an enemy chess piece with movement rules, health, and board position.
```

### Method: `Enemy.Awake`
```text
Cache references to the board manager, game manager, and player.
```

### Method: `Enemy.Start`
```text
Initialize the enemy health based on its type and cache the tile size used for movement.
```

### Method: `Enemy.Update`
```text
Smoothly interpolate the enemy toward its target position each frame.
```

### Method: `Enemy.SetPosition`
```text
Update the enemy's logical board coordinates and compute the target world position.
```

### Method: `Enemy.CanMove`
```text
Check whether the enemy can move to a target tile according to its chess movement rules and board occupancy.
```

### Method: `Enemy.TakeDamage`
```text
Reduce enemy health, destroy the object on death, and remove it from the board and active enemy list.
```

## EnemyStats.cs

### Class: `EnemyStats`
```text
Rotate and flip the enemy weapon to aim toward the cursor.
```

### Method: `EnemyStats.Awake`
```text
Cache the main camera reference.
```

### Method: `EnemyStats.Update`
```text
Aim the weapon pivot toward the cursor and flip the weapon sprite when the cursor is on the left side.
```

## GameManager.cs

### Class: `GameManager`
```text
Control turn flow, enemy spawning, board occupancy, and game-over state.
```

### Method: `GameManager.Awake`
```text
Cache the board manager, player, and shotgun weapon references.
```

### Method: `GameManager.Start`
```text
Initialize the game state, create the movable tile marker, and spawn the initial enemies.
```

### Method: `GameManager.Update`
```text
Process player turns, enemy turns, and the game-over flow.
```

### Method: `GameManager.GameOver`
```text
Delay briefly before freezing the game time when the match ends.
```

### Method: `GameManager.MoveInput`
```text
Read mouse input, highlight valid move tiles, and move the player when the user clicks a legal tile.
```

### Method: `GameManager.SpawnEnemy`
```text
Spawn the initial enemy formation at predefined board positions.
```

### Method: `GameManager.CreateEnemy`
```text
Instantiate an enemy prefab of the requested type and register it on the board.
```

### Method: `GameManager.EvaluateMove`
```text
Score a candidate enemy move so the AI can prefer moves that approach or capture the player.
```

### Method: `GameManager.MoveEnemy`
```text
Update the board state and move an enemy to a new grid position.
```

### Method: `GameManager.EnemyTurn`
```text
Search all legal enemy moves, choose the best-scoring move, apply promotion when needed, and trigger game over if the player is captured.
```

## Player.cs

### Class: `Player`
```text
Represent the player character, movement rules, and shotgun-related statistics.
```

### Method: `Player.Awake`
```text
Initialize ammo values and cache the board manager reference.
```

### Method: `Player.Start`
```text
Generate the board, find the starting tile, and place the player at the initial position.
```

### Method: `Player.Update`
```text
Smoothly interpolate the player toward the target position each frame.
```

### Method: `Player.SetPosition`
```text
Update the player's logical coordinates and target world position.
```

### Method: `Player.CanMove`
```text
Check whether the player can move to a target tile using king-like movement rules.
```

## ShotgunWeapon.cs

### Class: `ShotgunWeapon`
```text
Handle shotgun input, shooting, reload logic, raycast hits, and shot visual effects.
```

### Method: `ShotgunWeapon.Awake`
```text
Cache the camera, player, game manager, camera shake, and animator references.
```

### Method: `ShotgunWeapon.Update`
```text
Reset the shooting state when the fire cooldown ends.
```

### Method: `ShotgunWeapon.Shotgun`
```text
Read player input for firing or reloading and dispatch the corresponding action.
```

### Method: `ShotgunWeapon.Shoot`
```text
Consume ammo, start the shooting animation state, fire pellets, and trigger camera recoil.
```

### Method: `ShotgunWeapon.GetAimDirection`
```text
Compute the normalized direction from the muzzle to the mouse cursor.
```

### Method: `ShotgunWeapon.FireShotgun`
```text
Fire multiple pellets with random spread, apply raycast damage to enemies, and spawn impact or line effects.
```

### Method: `ShotgunWeapon.Reload`
```text
Reload the shotgun one shell at a time until the magazine is full or the ammo bag is empty.
```

### Method: `ShotgunWeapon.ShowShotLine`
```text
Display a short-lived line renderer to visualize a single pellet trajectory.
```

## Tile.cs

### Class: `Tile`
```text
Store the logical and world coordinates for one board tile.
```

### Method: `Tile.Start`
```text
Current placeholder lifecycle method for the tile object.
```
