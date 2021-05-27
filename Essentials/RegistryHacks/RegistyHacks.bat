reg add "HKCU\Control Panel\International" -v sShortDate -t REG_SZ -d "dd/MM/yyyy" -f
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{21EC2020-3AEA-1069-A2DD-08002B30309D}" /f
reg add "HKCR\Directory\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg delete "HKCR\Directory\shell\cmd2" /v "Extended" /f
reg add "HKCR\Directory\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\Directory\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\Directory\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\Directory\Background\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg delete "HKCR\Directory\Background\shell\cmd2" /v "Extended" /f
reg add "HKCR\Directory\Background\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\Directory\Background\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\Directory\Background\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\Drive\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg delete "HKCR\Drive\shell\cmd2" /v "Extended" /f
reg add "HKCR\Drive\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\Drive\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\Drive\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg delete "HKCR\LibraryFolder\Background\shell\cmd2" /v "Extended" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\*\shell\Open with Notepad\command" /ve /t REG_SZ /d "C:\Program Files\Notepad++\notepad++.exe %%1" /f
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{645FF040-5081-101B-9F08-00AA002F954E}" /f
reg add "HKCR\Directory\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg add "HKCR\Directory\shell\cmd2" /v "Extended" /t REG_SZ /d "" /f
reg add "HKCR\Directory\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\Directory\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\Directory\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\Directory\Background\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg add "HKCR\Directory\Background\shell\cmd2" /v "Extended" /t REG_SZ /d "" /f
reg add "HKCR\Directory\Background\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\Directory\Background\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\Directory\Background\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\Drive\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg add "HKCR\Drive\shell\cmd2" /v "Extended" /t REG_SZ /d "" /f
reg add "HKCR\Drive\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\Drive\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\Drive\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2" /ve /t REG_SZ /d "@shell32.dll,-8506" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2" /v "Extended" /t REG_SZ /d "" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2" /v "Icon" /t REG_SZ /d "imageres.dll,-5323" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\LibraryFolder\Background\shell\cmd2\command" /ve /t REG_SZ /d "cmd.exe /s /k pushd \"%%V\"" /f
reg add "HKCR\*\shell\runas" /ve /t REG_SZ /d "Take Ownership" /f
reg add "HKCR\*\shell\runas" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\*\shell\runas\command" /ve /t REG_SZ /d "cmd.exe /c takeown /f \"%%1\" && icacls \"%%1\" /grant administrators:F" /f
reg add "HKCR\*\shell\runas\command" /v "IsolatedCommand" /t REG_SZ /d "cmd.exe /c takeown /f \"%%1\" && icacls \"%%1\" /grant administrators:F" /f
reg add "HKCR\Directory\shell\runas" /ve /t REG_SZ /d "Take Ownership" /f
reg add "HKCR\Directory\shell\runas" /v "NoWorkingDirectory" /t REG_SZ /d "" /f
reg add "HKCR\Directory\shell\runas\command" /ve /t REG_SZ /d "cmd.exe /c takeown /f \"%%1\" /r /d y && icacls \"%%1\" /grant administrators:F /t" /f
reg add "HKCR\Directory\shell\runas\command" /v "IsolatedCommand" /t REG_SZ /d "cmd.exe /c takeown /f \"%%1\" /r /d y && icacls \"%%1\" /grant administrators:F /t" /f
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel" /v "{031E4825-7B94-4dc3-B131-E946B44C8DD5}" /t REG_DWORD /d "0" /f
reg add "HKCR\CLSID\{77708248-f839-436b-8919-527c410f48b9}" /ve /t REG_SZ /d "Registry Editor" /f
reg add "HKCR\CLSID\{77708248-f839-436b-8919-527c410f48b9}" /v "InfoTip" /t REG_SZ /d "Starts the Registry Editor" /f
reg add "HKCR\CLSID\{77708248-f839-436b-8919-527c410f48b9}" /v "System.ControlPanel.Category" /t REG_SZ /d "5" /f
reg add "HKCR\CLSID\{77708248-f839-436b-8919-527c410f48b9}\DefaultIcon" /ve /t REG_SZ /d "%%SYSTEMROOT%%\regedit.exe" /f
reg add "HKCR\CLSID\{77708248-f839-436b-8919-527c410f48b9}\Shell\Open\Command" /ve /t REG_EXPAND_SZ /d "%%SystemRoot%%\regedit.exe" /f
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace\{77708248-f839-436b-8919-527c410f48b9}" /ve /t REG_SZ /d "Add Registry Editor to Control Panel" /f
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer" /v "NoInternetOpenWith" /t REG_DWORD /d "1" /f
reg add "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer" /v "NoInternetOpenWith" /t REG_DWORD /d "1" /f
reg delete "HKCR\Drive\shell\cmd" /v "Extended" /f
reg add "HKCR\Drive\shell\cmd" /f
reg delete "HKCR\Directory\shell\cmd" /v "Extended" /f
reg add "HKCR\Directory\shell\cmd" /f
reg delete "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}" /f
reg delete "HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}" /f
reg add "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer" /v "link" /t REG_BINARY /d "00000000" /f
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Search" /v "BingSearchEnabled" /t REG_DWORD /d "0" /f
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Search" /v "CortanaConsent" /t REG_DWORD /d "0" /f
reg add "HKCR\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}" /v "System.IsPinnedToNameSpaceTree" /t REG_DWORD /d "0" /f
reg add "HKCR\Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}" /v "System.IsPinnedToNameSpaceTree" /t REG_DWORD /d "0" /f

pause