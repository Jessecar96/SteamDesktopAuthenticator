<h1 align="center">
  <img  src="https://raw.githubusercontent.com/Jessecar96/SteamDesktopAuthenticator/master/icon.png" height="64" width="64" />
  <br/>
  Steam Desktop Authenticator
</h1>
<p align="center">
  A desktop implementation of Steam's mobile authenticator app.<br/>
  <sup><b>We are not affiliated with Steam or Scrap.TF in any way!</b> This project is run by community volunteers.
</p>
<h3 align="center">
  <a href="https://github.com/Jessecar96/SteamDesktopAuthenticator/releases/latest">Download here</a>
</h3>
<p align="center">
<b>Clicking "Download ZIP" will not work!</b> This project uses git submodules so you must use git to download it properly.<br/>
Using <a href="https://desktop.github.com/">GitHub Desktop</a> is an easy way to do that.
</p>

**DISCLAIMER: We provide no support for you when using Steam Desktop Authenticator! This project is run by community volunteers and is not affiliated with Steam or Scrap.TF. You use this program at your own risk, and accept the responsibility to make backups and prevent unauthorized access to your computer!**

**REMEMBER: Always make backups of your `maFiles` directory! If you lose your encryption key or delete `maFiles` by accident AND you didn't save your revocation code, you are screwed.**

IF you lost your `maFiles` OR lost your encryption key, go [here](https://store.steampowered.com/twofactor/manage) and click "Remove Authenticator" then enter your revocation code that you wrote down when you first added your account to SDA.

If you did not follow the directions and did not write your revocation code down, you're well and truly screwed. The only option is beg to [Steam Support](https://support.steampowered.com/) and say you lost your mobile authenticator and the revocation code.

## Detailed setup instructions
- Download & Install [.NET Framework 4.5.2](http://go.microsoft.com/fwlink/?LinkId=397707) from the Microsoft website if you're using Windows 7. Windows 8 and above should do this automatically for you.
- Visit [the releases page](https://github.com/Jessecar96/SteamDesktopAuthenticator/releases) and download the latest .zip (not the source code one).
- Extract the files somewhere very safe on your computer. If you lose the files you can lose access to your Steam account.
- Run `Steam Desktop Authenticator.exe` and click the button to set up a new account.
- Login to Steam and follow the instructions to set it up. **Note: you still need a mobile phone that can receive SMS.**
- You may be asked to set up encryption, this is to make sure if someone gains access to your computer they can't steal your Steam account from this program. This is optional but highly recommended.
- Select your account from the list to view the current login code, and click `Trade Confirmations` to see pending trade confirmations.
- For your safety, remember to get Steam Guard backup codes! Follow [this link](https://store.steampowered.com/twofactor/manage) and click "Get Backup Codes," then print out that page and save it in a safe place. You can use these codes if you lose access to your authenticator.

[How to update SDA.](https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Updating)

[How to use SDA on multiple computers.](https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Using-SDA-on-multiple-computers)

## Command line options
```
-k [encryption key]
  Set your encryption key when opened
-s
  Auto-minimize to tray when opened
```

## Troubleshooting
- **Trade confirmation list is just white or a blank screen**
 - First open the "Selected Account" menu, then click "Force session refresh". If it still doesn't work after that, open the "Selected Account" menu again, then click "Login again" and login to your Steam account.

If your problem doesn't appear on the list or none of the solutions worked, submit an issue on the issue tracker. When posting logs in an issue, please upload it to some site like [Pastebin](http://www.pastebin.com).

## Building on linux
- First, you will need to install the `mono` and `monodevelop` packages, usually available from your standard package repository.
- Open monodevelop and select File -> Open. Navigate to the folder where you cloned this program to and open the file "Steam Desktop Authenticator/Steam Desktop Authenticator.sln"
- If you initialized submodules correctly, you should see two tree hirarchies on the left side of the screen, one labeled **SteamDesktopAuthenticator** and the other **SteamAuth**. (If you didn't, an error will be displayed; go update them!) For both of them, select "Packages", right click on "Newtonsoft.Json", and click update. Remember to do this for **both SteamDesktopAuthenticator and SteamAuth**
- Select Project->Active Configuration->Release (this will make this application run faster)
- Select Build->Build All. The package should now build successfully.
- The resulting executable and files will be in "Steam Desktop Authenticator/bin/Release"

## Alternate Linux method
- Start by going to the `releases` page of this repo and downloading the zip that contains all the files for the Windows version of SDA.
- Extract the files to a folder somewhere on the computer.
- Make sure you have the `mono` packaged installed. The `monodevelop` package is NOT required for this method.
- Open Terminal to the folder that contains the EXE and run `mono 'Steam Desktop Authenticator.exe'`. This will open the EXE as if it were on Windows through Mono. Since the app is pretty lightweight, this command should work completely, and you should have all features available to you including the ability to import an old maFiles. 
- You can also run it like this: `mono 'path/to/Steam Desktop Authenticator.exe`. Running it like that will enable you to put it in a launcher or shortcut to easily run it from a GUI.
