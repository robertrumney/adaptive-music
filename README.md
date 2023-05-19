# GameMusic

The GameMusic script is a Unity component that provides music management functionality for your game. It allows you to control the music playback and transition between different music states, such as danger and calm. You can easily integrate this script into your game by following the steps below.

## Features

- Play and manage game music seamlessly.
- Transition between different music states, such as danger and calm.
- Support for death music and optional transition audio.
- Adjustable music fade speed and maximum volume.
- Integration with other game systems, such as the GameProgress and ShopKeeping.

## Setup

1. Create an empty GameObject in your Unity scene.
2. Attach the `GameMusic` script to the GameObject.
3. Assign the required AudioSource components and audio clips in the inspector:
   - `Music1`: The main music audio source.
   - `Music2`: An additional audio source used for transitioning between music states.
   - `DeathMusic`: The audio clip to play when the player dies.
   - `optionalTransition`: An optional audio clip for smooth transitions between music states.
4. Customize the `musicFadeSpeed` and `MaxVolume` properties according to your game's requirements.

## Usage

To trigger the danger state and transition to appropriate music, call `GameMusic.instance.Danger()` from any script in your game when the player is damaged or when you want to indicate a rough situation.

Example usage:

```csharp
// Call the Danger() method from any script to indicate a dangerous or rough situation
GameMusic.instance.Danger();
```

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
