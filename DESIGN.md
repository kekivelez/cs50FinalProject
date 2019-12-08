# Initial Setup:
These initial steps provide a baseline to get started on a project and allow a basic mockup to be formed of how you want your game to look.
* Create a unity project.
* Create a canvas as a prefab and set the appropriate scaling of screen size.
* Add the background as game object/prefab.
* Add the player and enemy sprites as game objects/prefabs.
* Tweak player, sprite, camera, and background settings to establish a desired scaling.

# Player:
This section discusses the functionality asociated with the player, including the player script (`Player.cs`), which contains all of the logic that will allow the user to control and shoot the player controlled ship.  

## Movement:
* In the player script define a method (called `Move()` in this project) to allow movement. This method will be called by `Update()`.
* Movement requires 2 things a position and a rate of change. 
* In the movement method a variable called `delta(X/Y)` is created, this variable will use the `GetAxis()` method and multiply that by both `Time.deltaTime` and a value to offset that change (this offset will affect the speed of movement, thus it is labeled `moveSpeed`).
* The other variable will be the `new(X/Y)Pos` which will be the result of adding the current position of the player with `delta(X/Y)`.

## Map Boundary Check:
By default there is no concept of a boundary, this needs to be defined else the player's ship may wander beyond the limits of the play field.
* Create new properties to define the boundaries/edges. These are called `(x/y)Max` and `(x/y)Min`.
* In the player script define a method to establish the boundaries of the game world (so the player's movement is limited to the play field) this method is called `DefineMoveBoundaries` in this project and will update the aforementioned porperties. 
* The value will be set respective to the game camera using the x/y properties of the `ViewportToworldPoint()` method (whose parameter will be a `Vector3` initialized with the x/y coordinate of the edge) +/- a padding (the origin of the sprite is its center, the padding allows the sprite to not clip into the edge). 
* To finilize we update the player movement method to use `Mathf.Clamp()` to clamp the movement to `(x/y)(Max/Min)`.

## Player Shooting:
* Add a sprite to represent the projectile.
* Create a new game object/prefab of the newly imported sprite.
* Add a `RigidBody` component to the prefab.
* In the player script create a new method (`Fire()` in this project) to allow the player to fire a projectile.
* The game allows a continuos fire mode while leaving the fire key pressed, to do this a coroutine is required.
* In the fire method first verify if the key associated with firing is pressed (using `Inptus.GetButtonDown("Fire1")`), and if true, call the coroutine. To stop the firing action verify if the key was let go of and stop the coroutine.
* Inside this coroutine create a new instance of the projectile prefab as a game object (stored in a previously defined Serailazed GameObject variable) in an infnite loop.
* In unity open the player prefab in the inspector and there should be a field with the name of the projectile GameObject (In this project the projectile is named `LaserPrefab` so the inspector shows a field labled "Laser Prefab").
* Set the velocity property of the rigidbody component from the laser instance previosly created by assigning a new `Vector2` with a value of y (this project uses a value named/stored in `ProjectileSpeed`).
* The laser geme-objects must be destroyed after they leave the screen, see section labled `Destroy Objects` for details on this functionality.

# Destroy Objects:
The game needs a way to destroy any objects that leave the screen (i.e. player/enemy lasers that miss). This is done with a GameObject which is known as (and will be referenced as such henceforth) a shredder.
* Add a GameObject to represent the shredder.
* Add a `BoxCollider2d` component to the shredder and place (the shredder) offscreen. Mark the field `Is Trigger` in the unity inspector to treat collisions as a trigger.
* Add a `CapsuleCollider2D` to the projectile (capsule since it matches closely with projectile shape)
* Add a script (`Shredder.cs`) for the shredder game object, this script contains an event listener for the `Boxcollider2d` to destroy GameObjects (the GameObject that collides with it).
        
# Enemy:    
There are 5 components to this section, these are:

## Enemy
Contains the data of a particular enemy, these include:
* The sprite.
* How many hitpoints.
* How will they shoot (what pattern/how fast).
* How many points they grant on death.
* Any Fx/Animations the enemies may have.

A script (`Enemy.cs`) is created with the fields mentioned above. This script also contains the logic that determines when and how the enemies fire. The firing patters of the enemies are governed by a timer, the length of this timer is determined via random number generation, bounded by a min time and a max time (0.2 and 3 seconds respectively).

## Wave Config 
Holds specific data related to a particular wave like:
* The waypoint data of each path.
* Which enemy(ies) spawn(s) during that wave.
* How fast the wave spawns.
* How many enemies will the wave have.
* How fast will those enemies travel.

This class is defined as a `Scriptable Object`, the tag `[CreateAssetMenu(menuName = "Enemy Wave Config")]` allows us to use the unity editor to create new instances of this object.

The script (`WaveConfig.cs`) is composed of serializable fields and properties to return these fields.(Note, If a property is defined as a serialize field that property does not show up in the inspector, this seems to be a limitation of Unity, so this project serializes the class fields and defines properties that return the values of these class fields for public access.)

## Enemy Pathing 
The path the enemies take is based on a series of waypoints defined on the play space.

Add a GameObject to represent a path. Nested under that game object add N more GameObjects to represent waypoints (Where N is the desired number of waypoints).

A script (`EnemyPathing.cs`) is used to define how an enemy moves along the defined waypoints.

#### Create a script (EnemyPathing.cs)
The algorithm for moving through waypoint is as follows:

    At last waypoint?
    true:
        MoveTowards() target waypoint
        Reached target?
        true:
            Increment target waypoint
    false:
        Destroy/despawn enemy


## Enemy Spawner
The enemy spawner class is used as a controller class, it uses both the `Enemy.cs` and the `Wave Config.cs` to:
* Determine in which order each Wave Config is going to instantiate/execute and spawns it.
* Spawns each enemy inside the appropriate wave.

The script (`EnemySpawner.cs`) contains two co-routines:
* One that instantiates a new enemy and sets it's wave configuration so that it has access to it's speed and waypoint data 
* One to spawn multiple waves in sequence after a wave finishes spawning

## Boss Wave
The boss wave consists of 3 larger sized enemies with a bigger health pool, different pathing, and different projectiles then regular enemies. They have a chance to spawn after at least 2 waves have spawned. Aside from the logic used to determin when the boss is spawned, the rest of the implementation of these units mimic that of a standard enemy as they are an instance of the enemy class.

The Exact formula for spawning a boss wave is:
```cs
bool spawnBoss = randomGen.Next(0, 10) <= 3 ? true : false;
if (spawnBoss) { yield return StartCoroutine(SpawnEnemiesInWave(waveConfigs[bossWave])); }
```

# Dealing Damage:
This section handles the aspect of dealing damage. It applies the aspect of taking damage to the relevant prefabs (player or enemy) by adding an aditional script to any prefabs that deal damage (ex. player/enemy laser)
* Add prefabs for each enemy projectile and assign them a `RigidBody` component
* Create a hitbox for both the player and enemy prefabs
* In the unity inspector assign the appropriate layers to each prefab to avoid firendly objects from calculating collisions on friendly units
* A script (`DamageDealer.cs`) defines how much damage an object that is determined to deal damage will deal and what happens on hit.

# Score
As part of the core game loop there should be a reward for defeating enemies. The incentive for this project is obtaining a high score. As mentioned in the `Enemy` section each enemy object contains a value of how many points that enemy is worth. To be able to keep score the concept of a `Game Session` is created to keep track of the score.
* Create a script (`GameSession.cs`) that will contain the current score and 2 public methods (one to update the score after each enemy kill, and one to reset it after a game over).
* There should only be 1 `Game Session` instance at any given point in the game, so a `Singleton` of the `Game Session` class is created.
* On the game canvas create a text component for displaying the current score.
* Create a script (`ScoreDisplay.cs`) and asociate it to the text component.
* Using the `Update()` method, update the value of the text component with the current score.

# Polish:
The following items contribute to the overall feel of the game, these items exist outside of the core game loop, but provide atmosphere for the game.
## Scrolling Background:
* Add a `Quad` 3D GameObject and fit it to the play space.
* Add the background as a material in the unity inspector.
* Create a script (`BackgroundScroller.cs`).
* In the update method get the Renderer component associated with the Quad GameObject. This object contains a property that modifies the offset of a texture asociated with said renderer. By programatically modifying this offset the texture scrolls at a predefined speed.

## Stars:
* A particle system can be used to create the effect of stars giving the playspace a bit of extra pop.
* In Unity add a particle system and use the visual inspector to modify various properties (i.e. Color, density, speed, etc) until the desired effect is obtained
    
## Death Explosions (VFX):
* Create a new material in the unity editor using a sprite sheet with the desired explosion particles
* Create a particle system that uses the material and modify its properties in the unity inspector (i.e. particle density, speed, shape, etc) until the desired effect is obtained
* In the enemy script (`Enemy.cs`), add the newly created particle system to the section where the game determines the enemy has died.
* Once some time has elapsed destroy the GameObject of the aforementioned particle system

## Music:
* Add the music files to the assets folder so they are accesible within the project
* Create an `Audio Listener` component on the camera by dragging the audio file onto the camera prefab
* The music will restart on each scene by default, to avoid that a singleton will need to be created so that the state of the music persists throughout the game. A script (`GameMusic.cs`) is created that will instantiate a singleton of the game music object and when a scene transitions it will verify if there is already music playing (denothed by the existance of game music object). If music is already playing it will destroy the new object that is attempting to initialize.

## Main Menu and Game Over
This section is together since both these scenes are similar. While strictly not required to play the game, it provides a sense of agency and allows for a break before starting the game/retrying.
* Create a new Unity `Scene` and assign it the prefabs necesary to have it look like the game scene (these are the camera, canvas, starfields, and background).
* Create an `EventSystem` GameObject if one was not already created.
* Create a GameObject (`level`) and acompanying script (`Level.cs`) that will keep track of the game state and handle transitions between scenes.
* Add the desired text to the canvas. In the case of `Start` and `Quit` (or `Play Again` and `Main Menu` in the case of `Game Over` scene), overlay a button on top of the text and associate a method from the `Level.cs` script (or whatever script contains a method for the desired functionality) that will service the `OnClick` event.

## Boss Projectile Spin
The boss projectiles not only have a different sprite then regular enemies they also spin. This was added purely as an aesthetic feature and they are functionally equivalent to the other projectiles in the game.

To add this spin a script (`Spinner.cs`) was created and applied to the boss's projectile alongside the `DamageDealer.cs` script. `Spinner.cs` applies a constant value (180 deg, can be modified in the inspector) multiplied by `Time.deltaTime` to the transform property of the projectile.

## Seeing Player Health
Much like with `Score` player health is a text component on the canvas who's value is updated by a script (`HealthDisplay.cs`). This script gets the current health from the `Player` class and sets the health text component to that value every frame (using the `Update()` method).

# Glosarry of terms:
* variable(ValueA/ValueB)Name: Nomenclature used through out this document to indicate 2 or more variables that share the same name but only vary on a specific parameter (i.e. `(x/y)Position` reffers to 2 variables 1 called `xPosition` and one called `yPosition`).

* #.##f (ex. 3.14f): In C# any number with a decimal is interpreted as a `double`. Since C# does not provide any implicit conversions from `double` to `float`, when initializing a number of type `float`, the number needs to be either suffixed with the letter f or cast to `float`. The convention used throughout this project is to suffix the numbers with f.

* Prefab: A Unity feature that allows you to store a GameObject with all its components, property values, and child GameObjects as a reusable Asset. Think of the prefab as template for GameObjects

* GameObject: Every object in a game is a GameObject, from characters and collectible items to lights, cameras and special effects (to name a few).

* Sprite Renderer: The Sprite Renderer component renders the Sprite and controls how it visually appears in a Scene

* Start method: The Start method will be called by Unity before gameplay begins (ie, before the Update function is called for the first time) and is an ideal place to do any initialization.

* Update method: The Update method is the place to put code that will handle the frame update for the GameObject.

* SerializeField: An attribute that can be given to private fields to expose them to the Unity inspector.

* Unity Inspector Window: displays detailed information about the currently selected GameObject, including all attached components and their properties, and allows you to modify the functionality of GameObjects in your Scene.

* Time.deltaTime property: The completion time in seconds since the last frame. Is used to ensure that any timers are frame independent.

* RigidBody component: Adding a Rigidbody component to an object will put its motion under the control of Unity's physics engine.

* Coroutine: A method that can be suspended (yield its execution/processor time) until a condition is met

* Scriptable Object: A class you can derive from if you want to create objects that don't need to be attached to game objects.

* Hitbox: A bounding shape used to detect collision between 2 or more objects

* Layers in the inspector: The inspector contains a dropdown box that allows you to assign layers to each individual gameobject. These layers serve as a way to interact with Unity's default collision handling. This collision handling is done via a collision matrix, using this matrix it can be specified which items should/ should not collide with others based on the layer they are on

* [Singleton](https://en.wikipedia.org/wiki/Singleton_pattern): also known as the singleton pattern, is a software design pattern that restricts the instantiation of a class to one "single" instance. This is useful when exactly one object is needed to coordinate actions across the system.

* EventSystem object: Allows for event listeners to be handeled/serviced when an event triggers
