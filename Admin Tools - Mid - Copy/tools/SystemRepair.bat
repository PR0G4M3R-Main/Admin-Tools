@echo off
echo.
echo Repairing system files... (this may take a while)
sfc /scannow
echo.
echo Repairing system image...
DISM /Online /Cleanup-Image /RestoreHealth
echo.
echo Repairing Disk... (restart required)
echo Y | chkdsk C: /f /r
