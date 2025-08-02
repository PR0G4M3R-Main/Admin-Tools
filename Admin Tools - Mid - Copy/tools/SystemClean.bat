@echo off

echo Clearing Microsoft Store cache...
wsreset.exe

echo Waiting for Microsoft Store to open...
timeout /t 5 >nul

echo Closing Microsoft Store...
taskkill /F /IM WinStore.App.exe >nul 2>&1

echo Clearing Windows Update cache...
net stop wuauserv
net stop bits
del /f /s /q %windir%\SoftwareDistribution\Download\*.*

echo Starting Windows Update services...
net start wuauserv
net start bits

echo Flushing DNS cache...
ipconfig /flushdns

echo Flushing ARP cache...
arp -d

echo Resetting Winsock...
netsh winsock reset

echo Cleaning temp files...
for %%F in (%TEMP%\*.*) do (
  del /f /q "%%F" >nul 2>&1
)

echo Cleaning Windows Update leftovers...
Dism /Online /Cleanup-Image /StartComponentCleanup

echo Removing device driver packages...
pnputil /enum-drivers > drivers.txt
for /F "tokens=1 delims=:" %%d in ('findstr /C:"Published Name" drivers.txt') do (
    pnputil /delete-driver %%d /uninstall /force
)

echo Removing drivers.txt...
del /f /q "drivers.txt"

echo Clearing all temp folders...
del /q/f/s "%TEMP%\*"
del /q/f/s "%SystemRoot%\Temp\*"

echo All done! Reboot for best results.