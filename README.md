# Shoot it... In Space!

Shoot it... In Space! is a top down shooter in... you guessed it... space. Make sure to defeat as many enemies you can while avoiding their fire. Beware of the boss they can take a few hits and you'll never know when they will spawn.

## Getting Started

These instructions will get you a copy of the game up and running on your local machine. See [design document](DESIGN.md) for specifics on how the game was developed

### Running

For the versions available, see the [releases on this repository](https://github.com/kekivelez/cs50FinalProject/releases) and download the build.zip folder under the latest release (Currently 1.0).

To run, unzip the folder and run SpaceShooter.exe

### Building from source
Note: The version of unity may affect your ability to open the project and view/build the project and modify them using the inspector. Some familiarity with Unity is epected to be able to build from source.

* Download Unity (preferably version 2018.3.7f1)
* Clone or download the source files from this repository.
* Open Unity and navigate to File > Open Project and select the folder you previously cloned (if a zip was downloaded make sure to extract it)
* Tweak any desired values of any prefab using the Unity inspector (optional)
* Go to File > Build Settings, in the new window specify the platform (be aware the game was designed with the `PC, Mac & Linux Standalone` option, performance or ability to build with other options is not guranteed), hit build and select the folder you want the build files to go towards.

Note: When building from source for the first time, there is a chance that some prefabs are not linked with the necesary classes (ex. the player prefab may not be linked with the laser prefab). Should this be the case the project will not build, and hitting the play button on the unity interface is likely to fail. Take note of the unity console and link the necesary prefabs using the inspector.

## Built With

* [Unity](https://docs.unity3d.com/Manual/index.html) - The Game Engine, Built on version 2018.3.7f1
* [Visual Studio 2019 Community](https://visualstudio.microsoft.com/downloads/) - IDE
* [Assets](ASSETS.md)

## Controls

WASD for movement:

    W to go up

    S to go down

    A to go left

    D to go right

Spacebar to fire (hold for continuous fire)

## Authors

* **Cristian Velez** - [Github](https://github.com/kekivelez)

## Acknowledgements
More stackoverflow posts than I can remember (Bits, pieces, and ideas from various posts)

Unity Wiki and forums (same as with stack overflow)

[Unity learn](https://unity.com/learn) tutorials