@echo off

rem Set the proper path
setlocal
%~d0
cd %~dp0

set datastore=%~dp0
if exist Uninstall.exe set datastore=%APPDATA%\Prosody

set PROSODY_CFGDIR=%datastore%
set PROSODY_DATADIR=%datastore%\data
set PROSODY_SRCDIR=%~dp0\src\
set PROSODY_PLUGINDIR=%~dp0\plugins\

if not exist src\prosody goto no-prosody
if not exist "%PROSODY_CFGDIR%" goto make-folder
if not exist "%PROSODY_CFGDIR%\prosody.cfg.lua" goto install-config
goto run

:make-folder
mkdir "%PROSODY_CFGDIR%"
:install-config
copy src\prosody.cfg.lua.dist "%PROSODY_CFGDIR%\prosody.cfg.lua" > NUL
if exist "%PROSODY_CFGDIR%\certs" goto run
mkdir "%PROSODY_CFGDIR%\certs"
copy certs\localhost.* "%PROSODY_CFGDIR%\certs" > NUL

:run
start notepad "%PROSODY_CFGDIR%\prosody.cfg.lua"
goto exit

:no-prosody
echo File 'prosody' not found.
echo Current directory: %~dp0
pause

:exit
