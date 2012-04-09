<html>
<body>
<pre>
<h1>Build Log</h1>
<h3>
--------------------Configuration: PaliTextReader - Win32 (WCE ARMV4) Debug--------------------
</h3>
<h3>Command Lines</h3>
Creating temporary file "C:\DOCUME~1\ADMINI~1\LOCALS~1\Temp\RSP364.tmp" with contents
[
/nologo /W3 /Zi /Od /D "DEBUG" /D "ARM" /D "_ARM_" /D "ARMV4" /D UNDER_CE=420 /D _WIN32_WCE=420 /D "WIN32_PLATFORM_PSPC=400" /D "UNICODE" /D "_UNICODE" /FR"ARMV4Dbg/" /Fp"ARMV4Dbg/PaliTextReader.pch" /Yu"stdafx.h" /Fo"ARMV4Dbg/" /Fd"ARMV4Dbg/" /MC /c 
"C:\DOCUMENTS AND SETTINGS\ALL USERS\DOCUMENTS\VISUAL STUDIO PROJECTS\PaliTextReader\PaliTextReaderDlg.cpp"
]
Creating command line "clarm.exe @C:\DOCUME~1\ADMINI~1\LOCALS~1\Temp\RSP364.tmp" 
Creating temporary file "C:\DOCUME~1\ADMINI~1\LOCALS~1\Temp\RSP365.tmp" with contents
[
htmlview.lib /nologo /base:"0x00010000" /stack:0x10000,0x1000 /entry:"wWinMainCRTStartup" /incremental:yes /pdb:"ARMV4Dbg/PaliTextReader.pdb" /debug /out:"ARMV4Dbg/PaliTextReader.exe" /subsystem:windowsce,4.20 /align:"4096" /MACHINE:ARM 
".\ARMV4Dbg\BookmarkDoc.obj"
".\ARMV4Dbg\DictionaryCtl.obj"
".\ARMV4Dbg\IndexReader.obj"
".\ARMV4Dbg\PaliTextReader.obj"
".\ARMV4Dbg\PaliTextReaderDlg.obj"
".\ARMV4Dbg\PreferencesDlg.obj"
".\ARMV4Dbg\SelectBookDlg.obj"
".\ARMV4Dbg\StdAfx.obj"
".\ARMV4Dbg\unzip.obj"
".\ARMV4Dbg\PaliTextReader.res"
]
Creating command line "link.exe @C:\DOCUME~1\ADMINI~1\LOCALS~1\Temp\RSP365.tmp"
<h3>Output Window</h3>
Compiling...
PaliTextReaderDlg.cpp
Linking...
   Creating library ARMV4Dbg/PaliTextReader.lib and object ARMV4Dbg/PaliTextReader.exp
unzip.obj : warning LNK1166: cannot adjust code at offset=0x00001000, rva=0x00009F90
Creating temporary file "C:\DOCUME~1\ADMINI~1\LOCALS~1\Temp\RSP36B.tmp" with contents
[
/nologo /o"ARMV4Dbg/PaliTextReader.bsc" 
".\ARMV4Dbg\StdAfx.sbr"
".\ARMV4Dbg\BookmarkDoc.sbr"
".\ARMV4Dbg\DictionaryCtl.sbr"
".\ARMV4Dbg\IndexReader.sbr"
".\ARMV4Dbg\PaliTextReader.sbr"
".\ARMV4Dbg\PaliTextReaderDlg.sbr"
".\ARMV4Dbg\PreferencesDlg.sbr"
".\ARMV4Dbg\SelectBookDlg.sbr"
".\ARMV4Dbg\unzip.sbr"]
Creating command line "bscmake.exe @C:\DOCUME~1\ADMINI~1\LOCALS~1\Temp\RSP36B.tmp"
Creating browse info file...
<h3>Output Window</h3>




<h3>Results</h3>
PaliTextReader.exe - 0 error(s), 1 warning(s)
</pre>
</body>
</html>
