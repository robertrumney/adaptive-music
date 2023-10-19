# Ultimate Dynamic Game Music

The `UltimateDynamicGameMusic` is a comprehensive Unity MonoBehaviour script designed to manage and seamlessly transition between different music states in a game environment. It provides a robust system for dynamic game music management, allowing for smooth transitions between different music states based on game events or user input.

## Features

- **Dynamic Music Transitions:** Smoothly transition between various music states with customizable fade durations.
- **Weighted Music Entries:** Assign priority weights to different music states to influence playback selection.
- **Volume Configuration:** Easily adjust the music fade speed and maximum volume for playback.
- **Singleton Design:** Ensures only one instance of the music manager exists at any given time.

## Installation

1. Clone this repository or download the `UltimateDynamicGameMusic.cs` script.
2. Attach the script to a GameObject in your Unity project.
3. Add two AudioSource components to the same GameObject for smooth transitions.
4. Configure the music states and associated audio clips via the Unity inspector.

## Usage

### Setting up Music Entries

In the Unity inspector, under the "Music Configuration" header:

"Add music entries by clicking on the '+' button in the `musicEntries` list. For each entry, select a `MusicState`, assign an `AudioClip`, and set its `weight`."

### Transitioning Between Music States

"To change the music state during gameplay, use the following static method:"

```csharp
UltimateDynamicGameMusic.PlayState(MusicState state);
```

For example, to switch to the `Battle` music state, use:

```csharp
UltimateDynamicGameMusic.PlayState(MusicState.Battle);"
```

### Adjusting Music Volume

Use the following method to change the maximum music volume:

```csharp
PlayerPrefs.SetFloat("MusicVolume", desiredVolume);
```

Where `desiredVolume` is a float value between 0 (mute) and 1 (maximum volume).

## License

"This project is licensed under the MIT License. See the `LICENSE.md` file for details."
