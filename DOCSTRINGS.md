# Docstrings and Comments Export

> Equivalent XML doc-comment content for the Unity C# project.

## BoardManager.cs

### Class: `BoardManager`
```text
Manage the chessboard grid, instantiate alternating tile prefabs, and assign logical coordinates to each tile.
```

### Method: `BoardManager.Start`
```text
Current runtime entry point for the board manager. Board generation is intentionally left disabled in Start.
```

### Method: `BoardManager.GenBoard`
```text
Generate an 8x8 board by instantiating alternating light and dark tiles, positioning them in world space, and assigning logical coordinates to each Tile.
```

## CameraShake.cs

### Class: `CameraShake`
```text
Provide a short camera shake effect when the shotgun fires.
```

### Method: `CameraShake.Awake`
```text
Cache the camera's original local position before any shake effect runs.
```

### Method: `CameraShake.Shake`
```text
Apply a short randomized camera shake, then restore the camera to its original local position.
```

## Enemy.cs

### Class: `Enemy`
```text
Represent an enemy chess piece with movement rules, health, and board position.
```

### Method: `Enemy.Awake`
```text
Cache references, store the original visual state, and prepare movement lookup data.
```

### Method: `Enemy.Start`
```text
Initialize enemy health from its type and cache the tile dimensions used for movement.
```

### Method: `Enemy.Update`
```text
Smoothly interpolate the enemy toward its target world position each frame.
```

### Method: `Enemy.GetEnemyHP`
```text
Return the base HP for the enemy type and scale it by the current round index.
```

### Method: `Enemy.SetPosition`
```text
Update the enemy's logical board coordinates and compute the corresponding target world position.
```

### Method: `Enemy.CanMove`
```text
Check whether the enemy can move to a target tile according to its chess movement rules and the current board occupancy.
```

### Method: `Enemy.TakeDamage`
```text
Reduce enemy health, play the hit effect, and trigger death when health reaches zero.
```

### Method: `Enemy.Die`
```text
Remove the enemy from the board and active enemy list, then notify the round manager when a king is defeated.
```

### Method: `Enemy.HitEffect`
```text
Flash the sprite red, briefly scale it up, and add a small positional recoil when hit.
```

## GameData.cs

### Class: `GameData`
```text
Store persistent run-wide gameplay values such as ammo, pellet count, range, reload time, and fire rate.
```

### Method: `GameData.Awake`
```text
Keep a single persistent instance across scenes and destroy duplicates.
```

### Method: `GameData.ResetRun`
```text
Reset the ammunition values to their default starting state for a new run.
```

## GameManager.cs

### Class: `GameManager`
```text
Control turn flow, enemy spawning, board occupancy, and game-over state.
```

### Method: `GameManager.Awake`
```text
Cache the main gameplay references needed for turn processing and spawning.
```

### Method: `GameManager.Start`
```text
Initialize the match, create the move-tile marker, and prepare board-related values.
```

### Method: `GameManager.Update`
```text
Handle player turns, trigger enemy turns, and process the game-over flow.
```

### Method: `GameManager.GameOver`
```text
Wait briefly, then freeze time when the match ends.
```

### Method: `GameManager.ClearEnemies`
```text
Destroy all active enemies and clear the board occupancy array.
```

### Method: `GameManager.MoveInput`
```text
Read mouse input, highlight valid move tiles, and move the player when a legal tile is clicked.
```

### Method: `GameManager.SpawnEnemy`
```text
Dispatch the enemy formation for the current round index.
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
Search all legal enemy moves, choose the best-scoring move, apply pawn promotion when needed, and trigger game over if the player is captured.
```

## GameUI.cs

### Class: `GameUI`
```text
Display core gameplay information on the user interface.
```

### Method: `GameUI.Awake`
```text
Cache the GameData reference used by the UI.
```

### Method: `GameUI.Update`
```text
Refresh the UI every frame.
```

### Method: `GameUI.UpdateUI`
```text
Update the ammo text to show current ammo and reserve ammo.
```

## ManagentGameScene.cs

### Class: `ManagentGameScene`
```text
Handle the in-game menu button that returns the player to the start scene.
```

### Method: `ManagentGameScene.Awake`
```text
Cache the SceneLoader reference.
```

### Method: `ManagentGameScene.OnMenuButton`
```text
Load the start scene when the menu button is pressed.
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
Place the player at the starting tile and cache the initial target position.
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

## RoundData.cs

### Class: `RoundData`
```text
Store the data needed to describe one round, including spawn entries and HP scaling.
```

### Method: `PieceSpawnData`
```text
Describe one piece spawn entry with type, count, and base HP.
```

### Method: `RoundData`
```text
Hold the round index, HP multiplier, and the list of pieces to spawn.
```

## RoundManager.cs

### Class: `RoundManager`
```text
Manage round progression, upgrade timing, and the win condition.
```

### Method: `RoundManager.Awake`
```text
Cache the GameManager reference.
```

### Method: `RoundManager.Start`
```text
Start the first round and hide the win panel.
```

### Method: `RoundManager.StartRound`
```text
Reset the run state, clear enemies, and spawn the enemies for the current round.
```

### Method: `RoundManager.OnEnemyKingKilled`
```text
Begin the upgrade flow after the enemy king is defeated.
```

### Method: `RoundManager.ShowUpgradeDelay`
```text
Wait briefly before freezing time and opening the upgrade panel.
```

### Method: `RoundManager.OnUpgradeChosen`
```text
Advance to the next round and restart gameplay after an upgrade is selected.
```

## SceneLoader.cs

### Class: `SceneLoader`
```text
Load the different scenes used by the game while restoring normal time scale first.
```

### Method: `SceneLoader.LoadStartScene`
```text
Load the start menu scene.
```

### Method: `SceneLoader.LoadGameScene`
```text
Load the gameplay scene.
```

### Method: `SceneLoader.LoadUpgradeScene`
```text
Load the upgrade selection scene.
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
Reload the shotgun one shell at a time until the magazine is full or the reserve ammo is empty.
```

### Method: `ShotgunWeapon.ShowShotLine`
```text
Display a short-lived line renderer to visualize a single pellet trajectory.
```

## StartSceneManagent.cs

### Class: `StartMenu`
```text
Handle the start menu buttons and initialize a new run from the main menu.
```

### Method: `StartMenu.Awake`
```text
Cache the SceneLoader reference.
```

### Method: `StartMenu.Start`
```text
Reset the persistent game state when the menu opens.
```

### Method: `StartMenu.OnStartButton`
```text
Reset the run and load the gameplay scene.
```

### Method: `StartMenu.OnQuitButton`
```text
Quit the application.
```

## Tile.cs

### Class: `Tile`
```text
Store the logical and world coordinates for one board tile.
```

### Method: `Tile.Start`
```text
Placeholder lifecycle method for the tile object.
```

## UpgradeCardUI.cs

### Class: `UpgradeCardUI`
```text
Bind upgrade data to a single UI card and connect its selection button.
```

### Method: `UpgradeCardUI.Setup`
```text
Assign the icon, title, description, and click handler for an upgrade card.
```

## UpgradeData.cs

### Class: `UpgradeData`
```text
Store the data that describes a single upgrade option.
```

### Method: `UpgradeType`
```text
Enumerate the supported upgrade categories.
```

### Method: `UpgradeData`
```text
Hold the upgrade name, description, icon, type, and numeric value.
```

## UpgradeManager.cs

### Class: `UpgradeManager`
```text
Control the upgrade selection panel and apply the chosen upgrade to the current run.
```

### Method: `UpgradeManager.Awake`
```text
Cache the round manager reference.
```

### Method: `UpgradeManager.Start`
```text
Hide the upgrade panel at startup.
```

### Method: `UpgradeManager.ShowUpgradeUI`
```text
Pause the game and open the upgrade panel.
```

### Method: `UpgradeManager.HideUpgradeUI`
```text
Close the upgrade panel and resume time.
```

### Method: `UpgradeManager.GenerateChoices`
```text
Pick random upgrade cards from the available pool and populate the UI.
```

### Method: `UpgradeManager.ApplyUpgrade`
```text
Apply the selected upgrade to GameData, then continue to the next round.
```

## WeaponAim.cs

### Class: `EnemyStats`
```text
Rotate and flip the enemy weapon so it aims toward the cursor.
```

### Method: `EnemyStats.Awake`
```text
Cache the main camera reference.
```

### Method: `EnemyStats.Update`
```text
Aim the weapon pivot toward the cursor and flip the weapon sprite when the cursor is on the left side.
```
