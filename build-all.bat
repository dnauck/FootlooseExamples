@echo off
cls
Tools\NAnt\NAnt.exe all -buildfile:FootlooseExamples.build -D:codemetrics.output.type=HtmlFile -nologo -logfile:nant-build-all.log.txt %*
echo %time% %date%
pause