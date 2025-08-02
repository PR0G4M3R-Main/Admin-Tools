@echo off
echo Creating System Restore Point...
powershell -command "Checkpoint-Computer -Description 'SystemBackup Tool' -RestorePointType 'MODIFY_SETTINGS'"

echo Backing up key folders...
xcopy "%USERPROFILE%\Documents" "%SystemDrive%\Backup\Documents" /E /I /Y
xcopy "%USERPROFILE%\Pictures" "%SystemDrive%\Backup\Pictures" /E /I /Y

echo Backup complete. Folders saved to %SystemDrive%\Backup