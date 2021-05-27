rem powersaver
powercfg -duplicatescheme a1841308-3541-4fab-bc81-f71556f20b4a
rem high performance
powercfg -duplicatescheme 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c
rem ultimate performance
powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61
python -m pip install --upgrade pip
pip install numpy
pip install pandas
pip install matplotlib
rem matplotlib.pyplot
pip install seaborn

:OpenSites
cls
color 9
@echo get apps and drivers:
start microsoft-edge:https://www.microsoft.com/he-il/p/spotify-music/9ncbcszsjrsb?activetab=pivot:overviewtab
start microsoft-edge:https://www.microsoft.com/en-us/p/hp-smart/9wzdncrfhwlh?activetab=pivot:overviewtab
start microsoft-edge:https://www.microsoft.com/he-il/p/netflix/9wzdncrfj3tj?activetab=pivot:overviewtab
start microsoft-edge:https://www.microsoft.com/en-us/p/microsoft-to-do-lists-tasks-reminders/9nblggh5r558?activetab=pivot:overviewtab
start microsoft-edge:https://www.microsoft.com/en-us/p/eartrumpet/9nblggh516xp?activetab=pivot:overviewtab
start microsoft-edge:https://www.microsoft.com/en-us/p/hevc-video-extensions-from-device-manufacturer/9n4wgh0z6vhq?activetab=pivot:overviewtab
start microsoft-edge:https://www.microsoft.com/en-us/p/scrble-ink/9n5cf2mn39lv
start microsoft-edge:https://www.microsoft.com/he-il/p/microsoft-remote-desktop/9wzdncrfj3ps?activetab=pivot:overviewtab
start microsoft-edge:https://www.nvidia.com/en-gb/geforce/drivers/


rem @echo do you want to copy the windows.iso and rufus? 
rem (Y\y, N\n)
rem set /P UserInput=char UserInput: %=%
rem if /I "%UserInput%" == "y" goto CopyWindows
rem if /I "%UserInput%" == "n" goto knownDrivers


rem xcopy "\\alws34cloud\Alon\Programs\Operating_Systems\Windows\windows_10\2004\Windows_10_2004_13.8.2020" "%userprofile%\desktop\Windows_iso" /i /e /y
rem xcopy "\\alws34cloud\Alon\Programs\Operating_Systems\Windows\Rufus_USB-tool" "%userprofile%\desktop\Windows_iso\RUFUS" /i /e /y
rem explorer %userprofile%\desktop\Windows_iso


rem @echo will be copied into the computer: ADB, putty 
rem xcopy "\\alws34cloud\Alon\Programs\programs\platform-tools_r29.0.6-windows" "%userprofile%\desktop\ADB" /i /e /y rem copy ADB to Computer
rem xcopy "\\alws34cloud\Alon\Programs\programs\Putty" "%userprofile%\desktop" /i /e /y rem copy putty to Computer
rem xcopy "\\alws34cloud\Alon\Programs\programs\everything\Everything.ini" "%userprofile%\AppData\Roaming\Everything" /i /e /y rem copy "everything" settings  to Computer

cls
color 20
@echo DONE!
@echo Restart is required!
@echo how much time do you want befor restarting? (recommended 10 sec)
set /P UserInput=time UserInput: %=%
TIMEOUT /T %UserInput% /NOBREAK
shutdown.exe /r /t 00