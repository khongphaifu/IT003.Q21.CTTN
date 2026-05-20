# Docstrings and Comments Export

> Equivalent XML doc-comment content for the Unity C# project.

## BoardManager.cs

### Class: `BoardManager`
```text
Manage the chessboard grid, instantiate tile prefabs, and assign board coordinates.
```

### Method: `BoardManager.Start`
```text
Runtime initialization hook for the board manager. Board generation is currently disabled in Start.
```

### Method: `BoardManager.GenBoard`
```text
Generate an 8x8 chessboard by instantiating alternating light and dark tiles, positioning them in world space, and assigning logical coordinates to each Tile.
```

## CameraShake.cs

### Class: `CameraShake`
```text
Handle short camera shake feedback when actions such as firing occur.
```

### Method: `CameraShake.Awake`
```text
Cache the camera's original local position before shake effects are applied.
```

### Method: `CameraShake.Shake`
```text
Apply a brief randomized shake around the camera's original local position, then restore it after the effect ends.
```

## Enemy.cs

### Class: `Enemy`
```text
Represent a chess piece enemy with movement rules, health, board position, and hit feedback.
```

### Method: `Enemy.Awake`
```text
Cache renderer, board manager, game manager, player, and round manager references, and store the initial visual state.
```

### Method: `Enemy.Start`
```text
Initialize the enemy's maximum and current health based on its piece type and cache tile sizing information.
```

### Method: `Enemy.Update`
```text
Smoothly interpolate the enemy toward its target world position each frame.
```

### Method: `Enemy.GetEnemyHP`
```text
Calculate enemy health from piece type and round-based difficulty scaling.
```

### Method: `Enemy.SetPosition`
```text
Update the enemy's board coordinates and recompute its target world position.
```

### Method: `Enemy.CanMove`
```text
Check whether a target tile is legal for the enemy based on chess movement rules, board bounds, and occupancy.
```

### Method: `Enemy.TakeDamage`
```text
Reduce enemy health, trigger hit feedback, and destroy the enemy when health reaches zero.
```

### Method: `Enemy.Die`
```text
Remove the enemy from the board and enemy list, then trigger round progression if the king is defeated.
```

### Method: `Enemy.HitEffect`
```text
Flash the enemy red, scale it slightly, offset it briefly, then restore the original appearance.
```

## GameData.cs

### Class: `GameData`
```text
Store persistent run data such as ammo, shotgun stats, and current progression across scenes.
```

### Method: `GameData.Awake`
```text
Implement the singleton pattern and persist the game data object across scene changes.
```

### Method: `GameData.ResetRun`
```text
Reset the current run's ammo values and ammo bag values to their starting state.
```

## GameManager.cs

### Class: `GameManager`
```text
Control turn flow, enemy spawning, board occupancy, and game state.
```

### Method: `GameManager.Awake`
```text
Cache references to the round manager, board manager, player, shotgun weapon, and game data.
```

### Method: `GameManager.Start`
```text
Initialize the game state, spawn the movable tile marker, and prepare board sizing data.
```

### Method: `GameManager.Update`
```text
Process player turns, enemy turns, shooting/reloading states, and game over logic.
```

### Method: `GameManager.GameOver`
```text
Delay briefly before freezing the game when the match ends.
```

### Method: `GameManager.ClearEnemies`
```text
Destroy all enemies currently tracked on the board and clear the enemy list.
```

### Method: `GameManager.MoveInput`
```text
Read mouse input, highlight valid tiles, and move the player when the user clicks a legal destination.
```

### Method: `GameManager.SpawnEnemy`
```text
Spawn the enemy formation for the current round index.
```

### Method: `GameManager.CreateEnemy`
```text
Instantiate an enemy prefab of the requested type, place it on the board, and register it in the enemy grid.
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

## GameUI.cs

### Class: `GameUI`
```text
Display the current ammo status in the user interface.
```

### Method: `GameUI.Awake`
```text
Cache the persistent game data reference for UI updates.
```

### Method: `GameUI.Update`
```text
Refresh the on-screen UI every frame.
```

### Method: `GameUI.UpdateUI`
```text
Update the ammo label to reflect the current magazine and ammo bag values.
```

## ManagentGameScene.cs

### Class: `ManagentGameScene`
```text
Handle GameScene menu navigation back to the start scene.
```

### Method: `ManagentGameScene.Awake`
```text
Cache a reference to the scene loader.
```

### Method: `ManagentGameScene.OnMenuButton`
```text
Load the start scene when the menu button is pressed.
```

## Player.cs

### Class: `Player`
```text
Represent the player character and store its board position and movement state.
```

### Method: `Player.Awake`
```text
Initialize the player's ammo values and cache the board manager reference.
```

### Method: `Player.Start`
```text
Find the starting tile and place the player at the initial board position.
```

### Method: `Player.Update`
```text
Smoothly interpolate the player toward the target world position each frame.
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
Store the data required to describe one combat round and its enemy composition.
```

### Class: `PieceSpawnData`
```text
Describe how many pieces of one type should spawn in a round and what their base health is.
```

## RoundManager.cs

### Class: `RoundManager`
```text
Control round progression, upgrade flow, and transition to the next stage.
```

### Method: `RoundManager.Awake`
```text
Cache the game manager reference before round flow begins.
```

### Method: `RoundManager.Start`
```text
Start the first round and hide the win panel at runtime.
```

### Method: `RoundManager.StartRound`
```text
Reset the current round state, clear existing enemies, and spawn the enemy formation for the active round.
```

### Method: `RoundManager.OnEnemyKingKilled`
```text
Begin the delayed transition to the upgrade screen when the enemy king dies.
```

### Method: `RoundManager.ShowUpgradeDelay`
```text
Wait briefly for the defeat effect, then pause the game and show the upgrade UI.
```

### Method: `RoundManager.OnUpgradeChosen`
```text
Advance to the next round, restore time, and restart combat after an upgrade is selected.
```

## SceneLoader.cs

### Class: `SceneLoader`
```text
Provide simple helpers for switching between the project's scenes.
```

### Method: `SceneLoader.LoadStartScene`
```text
Load the start scene and ensure time is running.
```

### Method: `SceneLoader.LoadGameScene`
```text
Load the main game scene and ensure time is running.
```

### Method: `SceneLoader.LoadUpgradeScene`
```text
Load the upgrade scene and ensure time is running.
```

## StartSceneManagent.cs

### Class: `StartMenu`
```text
Manage the start menu flow, including starting a run or quitting the application.
```

### Method: `StartMenu.Awake`
```text
Cache the scene loader reference used by the start menu.
```

### Method: `StartMenu.Start`
```text
Reset the current run data when the start menu is shown.
```

### Method: `StartMenu.OnStartButton`
```text
Reset the run state and begin the game scene when the start button is pressed.
```

### Method: `StartMenu.OnQuitButton`
```text
Quit the application when the quit button is pressed.
```

## Tile.cs

### Class: `Tile`
```text
Store the logical and world coordinates for one board tile.
```

### Method: `Tile.Start`
```text
Placeholder lifecycle method for tile initialization.
```

## UpgradeCardUI.cs

### Class: `UpgradeCardUI`
```text
Bind upgrade data to a single UI card and wire up its selection button.
```

### Method: `UpgradeCardUI.Setup`
```text
Populate the card icon, title, and description, then assign the click callback for selecting the upgrade.
```

## UpgradeData.cs

### Class: `UpgradeData`
```text
Define a single upgrade option with its type, values, icon, and description.
```

### Enum: `UpgradeType`
```text
List the available upgrade categories that can be applied to the player.
```

## UpgradeManager.cs

### Class: `UpgradeManager`
```text
Generate upgrade choices, display them on cards, and apply the selected upgrade to the persistent game data.
```

### Method: `UpgradeManager.Awake`
```text
Cache the round manager reference before the upgrade UI is used.
```

### Method: `UpgradeManager.Start`
```text
Hide the upgrade UI at startup.
```

### Method: `UpgradeManager.ShowUpgradeUI`
```text
Pause the game, show the upgrade panel, and generate the available upgrade choices.
```

### Method: `UpgradeManager.HideUpgradeUI`
```text
Hide the upgrade panel and resume the game.
```

### Method: `UpgradeManager.GenerateChoices`
```text
Randomly choose upgrades from the available pool and assign them to the visible upgrade cards.
```

### Method: `UpgradeManager.ApplyUpgrade`
```text
Apply the selected upgrade to the persistent run data and continue to the next round.
```
