# I3E Assignment 1

## 🎮 Game Overview
This game is a simple third-person 3D platformer. The third-person perspective was approved by my lecturer, Mr Justin Lee. Collect all of the coins in the level while avoiding environmental hazards.

## 🛠️ Installation Guide
1. Download and install Unity from https://unity.com/download
2. Download and install GitHub Desktop from https://github.com/apps/desktop
3. In GitHub Desktop
    1. Go to File -> Clone Repository -> URL
    2. Input the URL https://github.com/CheangWeiCheng/I3E_ASG1.git
    3. Choose your local path
    4. Click Clone.
4. In Unity Hub:
    1. Select "Open" → "Add project from disk"
    2. Navigate to the cloned repository folder
5. Ensure these packages are installed:
    1. Input System (Window > Package Manager)
    2. Cinemachine (for camera controls)

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
**Coin collect, gunfire, jump, keycard collect, damage, box break:**  
*8 Bits Elements*  
https://assetstore.unity.com/packages/audio/sound-fx/8-bits-elements-16848

**Door open and close:**  
*Door, Cabinets & Lockers (Free)*  
https://assetstore.unity.com/packages/audio/sound-fx/foley/door-cabinets-lockers-free-257610

**Background Music:**  
*Absolutely Free Music*  
https://assetstore.unity.com/packages/audio/music/absolutely-free-music-4883

## Additional Credits
The scripts PlayerBehaviour.cs, CoinBehaviour.cs, HealthBehaviour.cs and BridgeMovementBehaviour.cs were modified from what was provided by my lecturer, Mr Justin Lee.  
Deepseek AI was used at times to optimise the code and fix certain errors. However, it was not used to write the code entirely.  
The ground check uses code from this reddit comment: https://www.reddit.com/r/Unity3D/comments/3c43ua/comment/icllc6a/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button