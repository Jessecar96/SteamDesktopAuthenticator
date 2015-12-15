# Steam Desktop Authenticator
A beta desktop implementation of Steam's mobile authenticator app.

### Disclaimer: YOU ARE USING THIS PROGRAM AT YOUR OWN RISK! THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND. We (SteamDesktopAuthenticator contributors) provide no support or help in using this program. Using this program puts you at a significant risk of losing your Steam account due to your own neglegence.
**ALWAYS MAKE BACKUPS OF YOUR `maFiles`! If you lose your encryption key or delete `maFiles` by accident AND you didn't save your recocation code, you are screwed.**

IF you lost your `maFiles` OR lost your encryption key, go [here](https://store.steampowered.com/twofactor/manage) and click "Remove Authenticator" then enter your revocation code that you wrote down when you first added your account to SDA.

If you did not follow the directions and did not write your revocation code down, you're well and truly screwed. The only option is beg to [Steam Support](https://support.steampowered.com/) and say you lost your mobile authenticator and the revocation code.

## If you agree to all this risk, here's how to set it up:
- Download & Install [DirectX](https://support.microsoft.com/en-us/kb/179113) if you don't have it installed already.
- Download & Install [Visual C++ Redistributable 2013 (vcredist_x86.exe)](https://www.microsoft.com/en-us/download/details.aspx?id=40784) from the Microsoft website if you don't have it already.
- Download & Install [.NET Framework 4.5.2](http://go.microsoft.com/fwlink/?LinkId=397707) from the Microsoft website if you're using Windows 7. Windows 8 and above should do this automatically for you.
- Visit [the releases page](https://github.com/Jessecar96/SteamDesktopAuthenticator/releases) and download the latest .zip (not the source code one).
- Extract the files somewhere very safe on your computer. If you lose the files you can lose access to your Steam account.
- Run `Steam Desktop Authenticator.exe` and click the button to set up a new account.
- Login to Steam and follow the instructions to set it up. **Note: you still need a phone that can receive SMS, you can get a free SMS enabled phone number from [Google Voice](https://www.google.com/voice) in the US**
- You may be asked to set up encryption, this is to make sure if someone gains access to your computer they can't steal your Steam account from this program. This is optional but highly recommended.
- Select your account from the list to view the current login code, and click `Trade Confirmations` to see pending trade confirmations.
- For your safety, remember to get Steam Guard backup codes! Follow [this link](https://store.steampowered.com/twofactor/manage) and click "Get Backup Codes," then print out that page and save it in a safe place. You can use these codes if you lose access to your authenticator.

[How to update SDA.](https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Updating)

[How to use SDA on multiple computers.](https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Using-SDA-on-multiple-computers)

**All data is stored in the `maFiles` folder where you extracted the program to. This contains your sensitive Steam account details. You should back this up, and NEVER share it with someone else.**
