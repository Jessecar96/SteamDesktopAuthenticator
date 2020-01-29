REM Remove un-needed CEF files
del /q "Steam Desktop Authenticator\bin\Release\devtools_resources.pak"
del /q "Steam Desktop Authenticator\bin\Release\cef_extensions.pak"
del /q "Steam Desktop Authenticator\bin\Release\pdf.dll"
del /q "Steam Desktop Authenticator\bin\Release\ffmpegsumo.dll"
del /q "Steam Desktop Authenticator\bin\Release\d3dcompiler_43.dll"
del /q "Steam Desktop Authenticator\bin\Release\widevinecdmadapter.dll"

REM Remove CEF directories
RD /S /Q "Steam Desktop Authenticator\bin\Release\locales\"
RD /S /Q "Steam Desktop Authenticator\bin\Release\swiftshader\"

REM Remove CEF debug files
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.BrowserSubprocess.Core.pdb"
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.BrowserSubprocess.pdb"
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.Core.pdb"
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.pdb"
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.WinForms.pdb"

REM Remove XML files
del /q "Steam Desktop Authenticator\bin\Release\Newtonsoft.Json.xml"
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.xml"
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.WinForms.xml"
del /q "Steam Desktop Authenticator\bin\Release\CefSharp.Core.xml"
del /q "Steam Desktop Authenticator\bin\Release\CommandLine.xml"

REM Remove ClickOnce app
del /q "Steam Desktop Authenticator\bin\Release\Steam Desktop Authenticator.application"
RD /S /Q "Steam Desktop Authenticator\bin\Release\app.publish\"