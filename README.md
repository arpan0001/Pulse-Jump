# Pulse Jump

Pulse Jump

# Project Overview

Pulse Jump is a 2.5D portrait-oriented endless runner developed in Unity.

The core gameplay mechanic is based on timing a pulse. The player must activate the pulse at the right moment to pass through incoming barriers. Successfully passing a barrier increases the score, while failing a barrier triggers Game Over.

The game features procedural track generation, object pooling, dynamic difficulty, score and distance tracking, pause/resume, Game Over and restart systems, audio, particle effects, shaders, and destructible barrier effects.

The architecture is designed to keep major gameplay systems separated, making the project easier to maintain, optimize, debug, and extend with additional barriers, levels, effects, and gameplay mechanics


# Project Setup Instructions

1. Requirements

The project requires Unity Hub and a Unity Editor version compatible with the project's ProjectSettings. For Android development, Android Build Support, including the Android SDK, NDK, and OpenJDK, must be installed and configured through Unity. A Windows or macOS system capable of running Unity is also required.

2. Opening the Project

Open Unity Hub and select Add Project. Select the root folder of the Pulse Jump project and open it using the Unity Editor version configured for the project. Allow Unity to import and compile the project assets. Once the project is loaded, open the main scene from Assets/Scenes/ and select the Gameplay scene if it is not already open.


# How to Run the Project

Unity Editor
Open the project in Unity and load the Gameplay scene. Press Play to start the game. The game initially waits for the player to start, with the Tap To Play UI displayed. Tap or click the screen to begin gameplay.

# Build and Run Instructions

1. Android Build

Open File → Build Profiles and select Android. Add the required scenes to the build scene list and make sure the Gameplay scene is included. Then open Edit → Project Settings → Player and configure the Android settings.
Recommended settings are Android as the platform, Portrait orientation, ARM64 architecture, and IL2CPP as the scripting backend. Connect an Android device with USB debugging enabled, or choose a location for the APK. Select Build or Build and Run, choose the APK location, and install the generated APK on the Android device.

2. Device Testing
The final build should be tested on a physical Android device rather than only in the Unity Editor. Testing should cover touch input, portrait orientation, game start, pulse interaction, barrier pass/fail, pause/resume, Game Over, restart, track recycling, performance, VFX, and audio.


# Technology, Engine and Frameworks

The project is developed using Unity as the game engine and C# as the programming language. It is a 3D/2.5D mobile game designed primarily for portrait-oriented gameplay.

Unity's rendering and material systems are used for the game's visuals, while Unity UI and TextMeshPro are used for score, distance, instructions, buttons, and other interface elements.

The project uses Unity Physics, including colliders, triggers, and rigidbodies, for player-barrier detection, pulse evaluation zones, and destructible barrier effects.
A custom AudioManager handles background music, barrier pass/fail sounds, and UI/gameplay audio. Unity Particle Systems and shaders/material effects are used for pulse effects, barrier effects, destruction effects, and other gameplay feedback.

The project also uses custom object pooling to efficiently reuse track segments and gameplay objects. The main pooling systems include TrackPool, TrackSegment, TrackRecycler, and reusable barriers. This reduces unnecessary runtime Instantiate() and Destroy() operations and helps maintain stable performance during endless gameplay.

# Project Architecture
The project follows a modular architecture, with gameplay responsibilities separated into independent systems. This makes the project easier to maintain, debug, and extend.
1. High-Level Architecture
Game
│
├── Game Systems
│   ├── GameStartController
│   ├── GameOverController
│   ├── GameStatistics
│   └── DifficultyManager
│
├── Player
│   └── PulseController
│
├── Generation
│   ├── TrackGenerator
│   ├── TrackPool
│   ├── TrackSegment
│   └── TrackRecycler
│
├── Obstacles
│   ├── BarrierController
│   ├── BarrierLibrary
│   └── BarrierDefinition
│
├── UI
│   ├── PauseController
│   ├── GameStart UI
│   └── GameOver UI
│
├── Audio
│   └── AudioManager
│
└── VFX
    └── GameVFXManager

1. Track Generation

The track uses procedural recycling instead of continuously creating new track objects. TrackPool provides reusable track segments, while TrackRecycler detects when a segment moves behind the camera and asks TrackGenerator to reposition it at the end of the track. This creates the appearance of an endless track while keeping the number of active objects limited.

2. Barrier System

BarrierController handles barrier evaluation. When the player enters the PulseCheckZone, it checks whether the player is currently pulsing. A successful pulse allows the player to pass and increases the score; otherwise, the game enters the Game Over state.

Barriers are designed to work with the procedural/pooling system, so gameplay references are resolved at runtime rather than requiring direct references from the barrier prefab.

3. Player Pulse System

PulseController manages the player's pulse state. When activated, the player expands to the configured pulse scale, holds briefly, and then returns to the normal scale. The barrier uses the pulse state to determine whether the player successfully passes.

4. Difficulty System

DifficultyManager controls difficulty based on elapsed gameplay time. The game progresses through Easy, Medium, Hard, and Very Hard stages, with difficulty affecting world speed, obstacle probability, and safe-segment requirements.

5. Game State

Game states are managed by separate controllers to keep the start, gameplay, pause, and Game Over logic independent.

6. Object Pooling

Object pooling is used to reduce runtime allocations and avoid repeatedly creating and destroying gameplay objects. Objects are created once, stored in a pool, reused during gameplay, and returned to the pool when no longer needed.

The main pooling systems include TrackPool, TrackSegment, TrackRecycler, and reusable barriers. This approach is particularly important for the endless-runner system because track segments are continuously recycled as the player progresses.

# Assumptions Made During Development

The game is designed primarily for portrait-oriented mobile devices.

The track is effectively endless through procedural recycling.

Barriers are evaluated through a dedicated trigger/evaluation zone.

Score increases when barriers are successfully passed.

The project prioritizes stable gameplay and mobile performance over highly complex physical simulation.


# Known Limitations

1. Device Performance

Performance can vary depending on:

Android device hardware
GPU
CPU
Screen resolution
Particle complexity
Rendering settings

2. Procedural Content Variety

The procedural generation system uses a predefined set of barrier/track configurations. It is not an unlimited procedural level-generation algorithm.



# AI Tools and AI Coding Assistants

AI tools were used as development assistance during the project.

Claude

Usage:

Unity C# scripting assistance.
Reviewing gameplay architecture.
Improving game-state handling.
Reviewing performance and optimization considerations.
Preparing project documentation and README content.



