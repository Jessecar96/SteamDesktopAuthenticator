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

**DISCLAIMER: We provide no support for you when using Steam Desktop Authenticator! This project is run by community volunteers and is not affiliated with Steam or Scrap.TF. You use this program at your own risk, and accept the responsibility to make backups and prevent unauthorized access to your computer!**

**REMEMBER: Always make backups of your `maFiles` directory! If you lose your encryption key or delete `maFiles` by accident AND you didn't save your revocation code, you are screwed.**

IF you lost your `maFiles` OR lost your encryption key, go [here](https://store.steampowered.com/twofactor/manage) and click "Remove Authenticator" then enter your revocation code that you wrote down when you first added your account to SDA.

If you did not follow the directions and did not write your revocation code down, you're well and truly screwed. The only option is beg to [Steam Support](https://support.steampowered.com/) and say you lost your mobile authenticator and the revocation code.

## Detailed setup instructions:
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

## Troubleshooting
- **Trade confirmation list is completely blank**
 - Refresh your account's session via the main form

If your problem doesn't appear on the list or none of the solutions worked, submit an issue on the issue tracker. When posting logs in an issue, please upload it to some site like [Pastebin](http://www.pastebin.com).
