# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
- Put any unreleased features under here.

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

- Four prototype scenes to playtest.
- Main Menu splash page to navigate between scenes in the current stage of the project.
- 4-directional movement for puzzle prototype system.
- Basic Player movement and interaction system with raycasting in a 3D environment.
- YarnSpinner Dialogue system with successful voice over response.
- Prototyped UI SFX system.
- Created Game State Manager script (not yet implemented).
