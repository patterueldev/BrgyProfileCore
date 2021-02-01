IF NOT EXIST "%USERPROFILE%\Documents\BrgyProfileCore" (
    mkdir "%USERPROFILE%\Documents\BrgyProfileCore"
    copy migrated-app.db "%USERPROFILE%\Documents\BrgyProfileCore\app.db"
)

IF NOT EXIST "C:\BrgyProfileCore" (
    xcopy /s ".\BrgyProfileCore\bin\Debug\netcoreapp3.1" "C:\BrgyProfileCore"
)

@echo off
set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"
echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\Desktop\BrgyProfileCore.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "C:\BrgyProfileCore\BrgyProfileCore.exe" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%
cscript /nologo %SCRIPT%
del %SCRIPT%