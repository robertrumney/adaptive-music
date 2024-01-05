# GameMusic

The GameMusic script is a Unity component that provides music management functionality for your game. It allows you to control the music playback and transition between different music states, such as danger and calm. You can easily integrate this script into your game by following the steps below.

## Features

- Play and manage game music seamlessly.
- Transition between different music states, such as danger and calm.
- Support for death music and optional transition audio.
- Adjustable music fade speed and maximum volume.
- Advanced versions available for more complicated setups.

## Setup

1. Create an empty GameObject in your Unity scene.
2. Attach the `GameMusic` script to the GameObject.
3. Assign the required AudioSource components and audio clips in the inspector:
   - `Music1`: The main music audio source.
   - `Music2`: An additional audio source used for transitioning between music states.
   - `DeathMusic`: The audio clip to play when the player dies.
   - `optionalTransition`: An optional audio clip for smooth transitions between music states.
4. Customize the `musicFadeSpeed`, `dangerMusicLength` and `MaxVolume` properties according to your game's requirements.

## Usage

To trigger the danger state and transition to appropriate music, call `GameMusic.Danger()` from any script in your game when the player is damaged or when you want to indicate a rough situation.

Example usage:

```csharp
// Call the Danger() method from any script to indicate a dangerous or rough situation
GameMusic.Danger();

// Call the Death() method from any script to indicate player death
GameMusic.Death();

// Call the ForceChill() method from any script to indicate a victory or calm state
GameMusic.ForceChill();
```

When not using the `ForceChill()` method the music will fade back to a chill state if no danger is present after the specified amount of time in the `dangerMusicLength` variable.

## Examples

Three distinct examples are available in the "Examples" folder of this repository for your reference. Should you encounter any difficulties, don't hesitate to consult these examples for guidance!

Note: The `GameMusic` script must be attached to a GameObject in the scene. Ensure you have a reference to the `GameMusic` script in your other scripts or use `GameMusic.instance` to access its public methods.

## Additional Functionality

The `GameMusic` script provides additional functionality that you can utilize:

- The `Death()` method allows you to trigger a specific death music when the player dies.
- The `SetMaxVolume(float x)` method enables you to adjust the maximum volume of the music dynamically.
- The `ForceChill()` method helps you force the music to transition to a calm state.

## Contributions

Contributions to the `GameMusic` script are welcome! If you find any issues or have suggestions for improvements, please feel free to open an issue or submit a pull request.

## License

The `GameMusic` script is released under the MIT License.

## Credits

`GameMusic` script is developed by [Robert Rumney](https://github.com/robertrumney).
