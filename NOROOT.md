# Import authenticator files from an un-rooted Android phone
## Requirements
* [Java JDK](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html)
* [APKTool](https://ibotpeaches.github.io/Apktool/install)

## Steps

1. Download the official Steam APK from http://store.steampowered.com/mobile (`download version 2.2.1` below the three buttons) and rename it to `steam.apk`.
2. Copy the Steam apk and the APKTools executables to a new folder.
2. Open a command prompt (Win+R, write `cmd` and press enter) and run:

      `apktool d steam.apk`
      
3. Wait for it to finish (don't close the command prompt).
4. Navigate to the steam folder that was just created, you should have a file named `AndroidManifest.xml`.
5. Open it with notepad and replace `android:allowBackup="false"` to `android:allowBackup="true"` (I recommend using the search and replace function). Save it.
6. In the command prompt, run

      `apktool b steam -o modded.apk`
      
7. Then run

      `keytool -genkey -v -keystore resign.keystore -alias alias_name -keyalg RSA -keysize 2048 -validity 10000`
      
    This will ask you for you name and a few other things, write whatever you want there, it doesn't matter. Make sure to remember the password you chose for the key store.

8. Now run

      `jarsigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore resign.keystore steam.apk alias_name`
      
    Write the password you chose when prompted to.
    
9. Time to get your phone! Connect it to ADB (follow until step 6 from [this guide](https://github.com/Jessecar96/SteamDesktopAuthenticator/wiki/Importing-account-from-an-Android-phone))
10. On the command prompt, run

      `adb shell "pm uninstall -k com.valvesoftware.android.steam.community"`
      
11. Copy the file `modded.apk` from your computer to your phone (you can run `adb push modded.apk /sdcard/modded.apk`)
12. Install the apk file on your phone using a file explorer (make sure to enable `Unknown Sources`)
13. Open SDA and import the account as usual.
14. Done!
