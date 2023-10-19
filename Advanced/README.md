# Advanced Game Music System

A flexible and user-friendly Unity music management system that allows for seamless transitions between different music states using fade effects. This system is designed to be extensible, allowing you to easily add or modify music states as your game grows or changes.

## Table of Contents

- [Features](#features)
- [Setup](#setup)
- [Customization](#customization)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Multiple Music States**: Supports a variety of predefined music states, including Chill, Danger, Battle, Victory, and Death.
- **Seamless Transitions**: Smooth fade transition effects between different music states.
- **User-friendly Interface**: Easily assign music clips for each state via the Unity inspector.
- **Optimized**: Utilizes only two `AudioSource` components for all transitions, optimizing performance.

## Usage

To transition between music states in your game:

```csharp
AdvancedGameMusic.instance.ChangeMusicState(AdvancedGameMusic.MusicState.Danger);
```

## Setup

1. Clone or download this repository to your Unity project.
2. Add the `AdvancedGameMusic` script to a new or existing GameObject in your scene.
3. Attach two `AudioSource` components to the same GameObject. These will be used for the fade transitions.
4. In the Unity inspector, locate the `AdvancedGameMusic` script component and expand the "Music Entries" section.
5. Assign music clips for each desired state.

## Customization

To add or modify music states:

1. Open the `AdvancedGameMusic` script.
2. Locate the `MusicState` enum and add or modify the states as required.
3. In the Unity inspector, you'll now see your new states available for assignment in the "Music Entries" section.
