# Copilot instructions for MinuteBattle

## Build / Run / Test / Lint
- No automated test or lint harness is present.
- Project is a Godot 4.5 game. Open GameSource\Content in Godot 4.5 (editor) and use Play Scene (F6) or Play Project (F5) to run.
- CLI: point the Godot executable at GameSource\Content. Example (replace with your Godot binary):
  - "C:\\path\\to\\Godot_v4.5-stable.exe" --path GameSource\Content
- No single-test runner exists. Add a test harness (e.g., gdunit3) if you need unit tests or CI integration.

## High-level architecture
- Godot project root: GameSource\Content (project.godot).
- Core runtime nodes are in Content\godot:
  - game_state: global game flags and timing (autoload singleton GameState).
  - ai: enemy_ai singleton (autoload).
  - card, deck, hand, deck_builder: card-based UI and deck logic.
  - battle, terrain, unit, play_ground: battle scene, unit placement, and movement logic.
  - resources, toast: reusable UI/asset scenes.
- Autoloads (project.godot): GameState, Utils, enemy_ai — treat as singletons available across scenes.
- Assets and data: Content\deck, Content\enemy, Content\hero, Content\maps, Content\sound.

## Key conventions & patterns
- Use GDScript 4.x features: @export, @onready, @tool, @export_storage.
- Editor-only behavior guarded with Engine.is_editor_hint() (cards/units show previews in editor).
- Card scenes contain a preview Unit node. Keep the Unit child when creating new card scenes to preserve editor previews.
- Movement scaling is centralized in GameState (MOVEMENT_MULTIPLIER, MOVEMENT_DURATION). Adjusting these affects visuals and physics globally.
- Signals decouple UI and game logic (examples: Hand.card_selected, Unit.unit_clicked). Follow existing naming conventions and connect signals in _ready or child-entered handlers.
- Terrain uses TileMapLayer helpers (map_to_local / SPAWN_COORDINATES). Use those for consistent positioning.
- Node/scene edits are preferred over hard-coded constants; many values are exposed via exported variables for editor tuning.

## Where to look for common tasks
- Add a card: Content\godot\card\card.tscn + assets in Content\deck.
- Add unit behavior: Content\godot\unit\unit.gd and hook up in battle.gd (_unit_connect and terrain child handlers).
- Modify rules: Content\godot\game_state.gd and related CardGame logic referenced in GameSource\readme.txt.

## CI / Assistant config
- No existing AI-assistant config files (CLAUDE.md, AGENTS.md, .cursorrules, etc.) detected. If adding CI, put workflow YAML in .github\workflows and add steps that call a test runner or custom scripts.

## Notes for Copilot sessions
- Primary workspace: GameSource\Content (GDScript & scenes). Keep changes scene-driven and editor-friendly.
- Prefer editing exported variables and scenes rather than inlining values in scripts.
- Respect Engine.is_editor_hint() to avoid breaking editor previews.

---
Created by Copilot CLI guidance script. If you want this file adjusted or to add MCP server configs (Playwright, etc.), say which services to configure.
