# Zuma's Revenge! x86 Port (WP7 Source)

A native x86 port of **Zuma's Revenge!**, reconstructed and adapted from the decompiled Windows Phone 7 (.xap game) source code. 

## Prerequisites
Before building or running the project, make sure you have the following installed:
* Windows 10 or higher
* Visual Studio 2022+ (with the **.NET desktop development**, monoGame nuGet package (desktop.GL/DX))
* .NET 6.0 or .NET 8.0 (recommended 6.0)
All game files and resources located on ```/Content``` folder:

## Bugs
* The game window opens in fullscreen mode; switching to windowed mode and changing the window resolution are not fully implemented.
* The aiming crosshair when shooting from the frog is slightly bugged.
* The settings menu does not open; a DEBUG menu was added, but I couldn't get it to function properly.
* It is impossible to continue the game after restarting the application; the "Continue" button simply cannot be pressed.
* Although support for mouse controls and the `Esc` key was added for navigation, it still emulates touch screen inputs, making button presses impossible in certain scenarios.
* There are still many minor bugs related to the `SexyAppFramework` game engine.

## License & Legal (Disclaimer)
This project is strictly for **educational and preservation purposes only**. 
* This is a non-commercial, open-source effort.
* All rights to "Zuma" and "Zuma's Revenge!" belong to **PopCap Games** and **Electronic Arts (EA)**. 
* No copyright infringement is intended. You must own a legitimate copy of the game to utilize this port.

Please open an issue if you want to fix something, because I am out of options; PopCap's proprietary engine is very difficult for me to port, and several code fragments were written by AI, so I cannot guarantee their quality.
