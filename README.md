# I3E Assignment 1

## 🎮 Game Overview
This game is a simple third-person 3D platformer. The third-person perspective was approved by my lecturer. Collect all of the coins in the level while avoiding environmental hazards.

## 🛠️ Installation Guide
### How to Play (Windows)
1. Download the latest release from https://github.com/CheangWeiCheng/I3E_ASG1.git
2. Extract the ZIP file
3. Run `I3E.exe` from the `Builds` folder

### How to Edit in Unity
1. Download and install Unity from https://unity.com/download
2. Download and install GitHub Desktop from https://github.com/apps/desktop
3. In GitHub Desktop
    a) Go to File -> Clone Repository -> URL
    b) Input the URL https://github.com/CheangWeiCheng/I3E_ASG1.git
    c) Choose your local path
    d) Click Clone.
4. In Unity Hub:
    a) Select "Open" → "Add project from disk"
    b) Navigate to the cloned repository folder
5. Ensure these packages are installed:
    a) Input System (Window > Package Manager)
    b) Cinemachine (for camera controls)

## 🖥️ System Requirements
Platform: Windows

| Component      | Recommended                                   |
|----------------|-----------------------------------------------|
| **OS**         | Windows 11 64-bit                             |
| **Processor**	 | 1200 Mhz, 14 Core(s), 18 Logical Processor(s) |
| **CPU**        | Intel(R) Core(TM) Ultra 5 125H                |
| **GPU**        | Intel® ARC Graphics                           |
| **RAM**        | 16GB                                          |

## 🕹️ Key Controls
| Action       | Keybind           |
|--------------|-------------------|
| Move         | WASD              |
| Look         | Mouse             |
| Jump         | Space             |
| Fire Weapon  | Left Mouse Button |
| Interact     | E                 |

## ⚠️ Known Limitations
### Current Bugs
- Sometimes a door will highlight together with a coin

### Performance Notes
- Loading on startup is quite slow.

## 📚 Asset Credits
### Audio
Coin collect, gunfire, jump, keycard collect, damage, box break:
8 Bits Elements
https://assetstore.unity.com/packages/audio/sound-fx/8-bits-elements-16848

Door open and close:
Door, Cabinets & Lockers (Free)
https://assetstore.unity.com/packages/audio/sound-fx/foley/door-cabinets-lockers-free-257610

Background Music:
Absolutely Free Music
https://assetstore.unity.com/packages/audio/music/absolutely-free-music-4883

## Additional Credits
The scripts PlayerBehaviour.cs, CoinBehaviour.cs and HealthBehaviour.cs were modified from what was provided by my lecturer, Mr Justin Lee, with permission.
Deepseek AI was used at times to optimise the code and fix certain errors. However, it was not used to write the code entirely.