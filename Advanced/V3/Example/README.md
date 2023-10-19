# Boss Fight Music Manager

Manage dynamic music transitions during a boss fight using the `UltimateDynamicGameMusic` asset. This script provides an immersive audio experience by adapting the music based on the player's interaction and the boss's status.

## Features

- **Proximity-based Transitions:** Starts the boss fight music as the player approaches the boss.
- **Dynamic Responses:** Transitions to victory music when the boss is defeated.
- **Resets Music State:** Returns to a more relaxed music state once the player moves away from the defeated boss.

## Prerequisites

- Ensure you have the `UltimateDynamicGameMusic` asset installed and set up in your project.
- A BossHealth script or equivalent to monitor the boss's health status.

## Installation

1. Clone this repository or download the `BossFightManager.cs` script.
2. Attach the script to a suitable GameObject in your Unity scene, preferably the player character or the main camera.
3. Link the boss's health component to the `bossHealth` field in the inspector.

## Usage

### Setting up Boss Health

Ensure your boss character has a script/component (e.g., `BossHealth`) that tracks its health. The `BossFightManager` script references this to determine when the boss is defeated.

### Music Transitions

"The script automatically handles music transitions based on player proximity and boss health:

- "Approaching the boss starts the `Danger` music state."
- "Defeating the boss triggers the `Victory` music state."
- "Moving away from the defeated boss returns to the `Chill` music state."

Ensure the respective music states and clips are set up in the `UltimateDynamicGameMusic` asset.
