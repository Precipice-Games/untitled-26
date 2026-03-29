# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
* Put any unreleased features here.

## [v1.0.2] - 2026-03-29

### Added
* Added a reset button to puzzles in puzzle mode.
* Added the ability to exit puzzles from puzzle mode back into exploration mode.
* Added Oasis Island scene (not yet in build settings, but present in the project folder).
* Created PuzzleFeedback.cs with a prototype "error flashing" system on tiles attempting to move into occupied cells (WIP).

### Fixed
* Fixed the ice tile recursion logic to prevent the Player from skipping over a gap.
* Resolved game-breaking issue in puzzle mode by converting singletons for puzzle components into private, individualized references.
* Temporarily adjusted tile textures and lighting for greater visibility in puzzle mode (hotfix).

### Changed
* Connected island completion status system to YarnSpinner InMemoryVariableStorage.
* Refactored the PlayerFixedMovement logic and tile type handling for a more modular approach.
* Updated some properties with more Odin Inspector features to improve the debugging options in several classes.

## [v1.0.1] - 2026-03-24

### Added
* Added Puzzle2.prefab to the Ice Island.
* Added orientation handling to the Player class so that the Player faces upwards during Puzzle mode.
* Added UI Actions to Puzzle action map on the PlayerControls.inputactions asset.
* Created the "Ground" layer mask.
* Added a death barrier/void to the Ice Island.

### Fixed
* Fixed infinite jumping bug by adding a raycast-based ground checking system.
* Fixed a graphical issue to ensure the surrounding edges of Skye's sprite within the 3D environment was transparent.
* Fixed the speed of the Player's idle animation by making it frame-independent.

### Changed
* Changed the StartArea and SnowIsland_v1 prefab object to be within the Ground layer mask.
* Restored missing code from the puzzle completion status system within the Island scripts (WIP).

## [v1.0.0] - 2026-03-12

### Added
* Added baseline ice tile "sliding" mechanic.
* Added volume sliders that work with music & SFX to fade playback in and out.
* Added neon start and end waypoints for Player in puzzle mode.
* Added dynamic tile texturing for ice tiles and normal tiles.
* Added height map to tiles for further depth.
* Added Penguin character sprite into Ice_Island.unity scene.

### Fixed
* Fixed an issue to disable the interaction prompt even while paused.

### Removed
* Removed old prototype scenes from build settings, leaving only the main menu and the Ice Island.
* Removed the splash page feature.
* Disabled the casting of shadows from tiles.

## [v0.4.3-prototype.1] - 2026-03-11

### Added
* Added animated idling sprite for Skye.
* Added basic landscape collision for the Ice Island.
* Added code for different tile types through an Enum (WIP).
* Added more controls to the Options menu.
* Added 'ESC' to the input action map for pausing.

### Fixed
* Fixed a bug where the Player was able to move the start and end tiles (this should be disabled).

### Changed
* Improved visual accessibility by adding a black outline to the interaction prompt UI pop-up.
* Restricted the interaction prompt to Exploration mode (WIP).
* Resized the Options menu to fit in the screen.

## [v0.4.2-prototype.1] - 2026-03-09

### Added
* Added the ice island island model.
* Added limited movement card usages into puzzle mode.

### Changed
* Updated the puzzle informational system to properly handle completion statuses.

## [v0.4.1-prototype.1] - 2026-03-08

### Added
* Added IslandManager.prefab & Island Scripts (WIP).

### Fixed
* Temporarily patched a bug in which the onPuzzleTrigger() wasn't switching to Puzzle state.
* Found and resolved syntactical bug regarding the playerMoved invocation in PlayerFixedMovement.cs.

### Changed
* Configured the puzzle camera & UI to be more aesthetically pleasing.
     * Also made the UI not block the actual tiles in the scene.
* Changed the puzzle camera to be disabled by default due to initialization sequencing with the GameStateManager.

## [v0.4.0-prototype.3] - 2026-03-07

### Added
* Added dialogue for Judith NPC (WIP).
* Added configuration for the end tile to trigger puzzle completion.
* Added the 3D mountain asset into the Ice_Island.unity scene.
* Added script for persistent variable storage for dialogue system.

### Fixed
* Fixed an error where the quit button in MainMenu.unity wasn't working.

### Changed
* Renamed & refactored tile-related scripts for puzzle system.
* Changed the MainMenu.unity scene to load up Ice_Island.unity instead of DialogueTest_1.unity.

## [v0.4.0-prototype.2] - 2026-03-07

### Added
* Added Mana Data to Puzzle System.
* Connected the Mana data to a counter in the puzzle UI.
* Added UI interaction prompt when the Player's raycast hits an interactable object.

### Fixed
* Fixed an error with a duplicated event system in Ice_Island.unity.
* Fixed an issue where the Player was able to move a Player-occupied tile.

## [v0.4.0-prototype.1] - 2026-03-05

### Added
* Added fixed Player movement to navigate across tiles in puzzle mode.
* Created the PhysicsManager that integrates with the current GameStateManager system.
* Added kinematics handling for the Player character.
* Created the first puzzle prefab on the Ice Island.
* Created PuzzleInformation.cs script to harbor important information about each puzzle prefab.
* Added settings menu section to the pause menu UI.
* Added Figma UI volume sliders in the settings menu (WIP).

### Changed
* Finalized the Music Manager system.
* Enhanced InteractablePillar.cs to package and send PuzzleInformation data during event firing.
* Updated puzzle trigger event handling in the GameStateManager system.
* Changed puzzle UI and camera handling to be dynamically assigned.
* Changed the default scene to MainMenu.unity.

## [v0.3.0-prototype.1] - 2026-03-02

### Added
* Added the first half of the greyboxed ice island.
* Added a functional game state manager to the ice island.

### Fixed
* Fixed an issue where the cursor was unavailable for use in the SplashPage.unity scene.

### Removed
* Removed an extra audio listener component that was causing warnings in the editor.

## [v0.2.1-prototype.1] - 2026-03-01

### Added
* Added Dialogue state to Game State Manager.
* Created a lock on the camera during dialogue sequences to allow for proper UI interaction.

### Fixed
* Fixed an issue where the Player was not able to progress through the intro dialogue sequence.

## [v0.2.0-prototype.2] - 2026-02-26

### Added
* Added a polished game loop between DialogueTest_1.unity and TileMoveExperiment.unity
* Functional Game State Manager system with 4 states: Exploration, Paused, Puzzle, Menu.
* Prefabs for Player, Game State Manager, Audio Manager, and Music Manager.
* Added intro sequence dialogue (WIP).
* Proper pause menu toggling.

### Fixed
* Fixed an issue where the movement interaction scene appeared to be frozen.
* Fixed an issue plaguing addressable assets schemas used by YarnSpinner in local builds.
* Fixed an issue where the action maps weren't switching properly.

### Changed
* Enhanced Figma UIs in the MainMenu.unity scene.

### Removed
* Removed the MovementInteraction_1.unity scene button from the splash page screen.

## [v0.2.0-prototype.1] - 2026-02-23

### Added
* Two new prototype scenes to playtest.
* UI Main Menu prototypes from Figma.
* Proof-of-concept "Bridge" experiment.
* Selectable tiles that can be highlighted when chosen.
* Grid system for puzzle mechanic.
* Mockup of a game state switch from Exploration mode to Puzzle mode.
* Early implementation of Game State Manager.
* Grey boxing Ice Island.
* Music Manager system with looping tracks.

### Changed
* TileMoveExperiment.unity upgraded from one tile to a full layout with different tiles to select and move.
     * New grid system prevents tiles from colliding with one another.
* SplashPage.unity has become the new landing page for early releases.
     * MainMenu.unity under construction.
* Enhanced Game State Manager system and scripts.

## [v0.1.0-prototype.1] - 2026-02-15

### Added
* Four prototype scenes to playtest.
* Main Menu splash page to navigate between scenes in the current stage of the project.
* 4-directional movement for puzzle prototype system.
* Basic Player movement and interaction system with raycasting in a 3D environment.
* YarnSpinner Dialogue system with successful voice over response.
* Prototyped UI SFX system.
* Created Game State Manager script (not yet implemented).
