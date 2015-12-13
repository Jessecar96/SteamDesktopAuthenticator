# Steam Desktop Authenticator
A desktop implementation of Steam's mobile authenticator app.

**Be warned! Using this desktop app in place of Steam's mobile app defeats the entire purpose of another device acting as your authenticator. You assume all responsiblity for keeping your computer secure. If a 3rd party gains access to your  authenticator data they will gain full instant access to your Steam account and all its items!**

## Setup Instructions:
- Download & Install [Visual C++ Redistributable 2013 (vcredist_x86.exe)](https://www.microsoft.com/en-us/download/details.aspx?id=40784) from the Microsoft website if you don't have it already.
- Download & Install [.NET Framework 4.5.2](http://go.microsoft.com/fwlink/?LinkId=397707) from the Microsoft website if you're using Windows 7. Windows 8 and above should do this automatically for you.
- Visit [the releases page](https://github.com/Jessecar96/SteamDesktopAuthenticator/releases) and download the latest zip (not the source code one).
- Extract the files somewhere very safe on your computer. If you lose the files you can lose access to your Steam account.
- Run `Steam Desktop Authenticator.exe` and click the button to setup a new account.
- Login to Steam and follow the instructions to set it up. **Note: you still need a phone that can receive SMS, you can get a free SMS enabled phone number from [Google Voice](https://www.google.com/voice) in the US**
- You may be asked to setup encryption, this is to make sure if someone gains access to your computer they can't steal your Steam account from this program. This is optional but heavily reccomended.
- Select your account from the list to view the current login code, and click `Trade Confirmations` to see pending trade confirmations.
- For your saftey, remember to get Steam Guard backup codes! Go to https://store.steampowered.com/twofactor/manage and click "
Get Backup Codes", then print out that page and save it in a safe place. You can use these codes if you lose access to your auenticator.

[How to update SDA](https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Updating)

[How to use SDA on multiple computers](https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Using-SDA-on-multiple-computers)


**All data is stored in the `maFiles` folder where you extracted the program to. This contains your sensitive Steam account details. You should back this up, and NEVER share it with someone else.**

**When updating, make sure to save your `maFiles` folder. You can just replace all the exe and dll files over the old ones and you'll be fine.**

## Features:
- Generate login codes and confirm trades on multiple Steam accounts with ease.
- Enable Steam's mobile auth on new accounts.
- Encryption of sensitive account details.

## Coming soon:
- Notifications when you have a trade waiting to be confirmed.
- Import config file from a rooted Android device so you don't have to remove and re-add the desktop authenticator.
