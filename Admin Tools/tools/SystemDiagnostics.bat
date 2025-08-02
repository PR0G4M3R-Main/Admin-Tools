@echo off
echo Clearing previous CBS log entries...
del /f /q %windir%\Logs\CBS\CBS.log >nul 2>&1
echo.
echo Checking system files... (this may take a while)
sfc /verifyonly
echo.
echo Checking system image...
DISM /Online /Cleanup-Image /ScanHealth
echo.
echo Scanning disk status...
chkdsk C:
