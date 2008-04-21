; NSIS installer script for CheckSumTool
; $Id$

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "CheckSum Tool"
!define PRODUCT_VERSION "0.5.0"
!define PRODUCT_PUBLISHER "Ixonos Plc"
!define PRODUCT_WEB_SITE "http://checksumtool.sourceforge.net/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\CheckSumTool.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; Installer executable must not contain spaces (SourceForge doesn't allow them)
!define INSTALLER_EXECUTABLE "CheckSumTool"

; Required DotNet version
; If required version is not found, error is shown and installation aborted
; (this is better than force dotnet installation)
!define DOT_MAJOR "2"
!define DOT_MINOR "0"
!define DOT_MINOR_MINOR "50727"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "..\..\Documents\COPYING"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\CheckSumTool.exe"
!define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\Readme.txt"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "..\..\Build\${INSTALLER_EXECUTABLE}-${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\CheckSum Tool"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

; Version resource information for installer executable
; ProductVersion needs four numbers separated by dots
VIProductVersion "${PRODUCT_VERSION}.0"
VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "${PRODUCT_NAME}"
;VIAddVersionKey /LANG=${LANG_ENGLISH} "Comments" "A test comment"
VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "${PRODUCT_PUBLISHER}"
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "© 2007-2008 Ixonos Plc, Kimmo Varis"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "${PRODUCT_NAME} Installer"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${PRODUCT_VERSION}"


; At first, check that required dotnet version is installed
Function .onInit
  call IsDotNetInstalledAdv
FunctionEnd

Section "Core Files" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "..\..\Build\Release\CheckSumTool.exe"
  CreateDirectory "$SMPROGRAMS\CheckSum Tool"
  CreateShortCut "$SMPROGRAMS\CheckSum Tool\CheckSum Tool.lnk" "$INSTDIR\CheckSumTool.exe"
  CreateShortCut "$DESKTOP\CheckSum Tool.lnk" "$INSTDIR\CheckSumTool.exe"
  File "..\..\Documents\COPYING"
  File "..\..\Documents\Readme.txt"
  File "..\..\Src\config.xml"
SectionEnd

Section "User Manual" SEC02
  SetOutPath "$INSTDIR\Manual"
  SetOverwrite ifnewer
  File "..\..\Build\Release\Manual\Manual.html"
  SetOutPath "$INSTDIR\Manual\Images"
  CreateDirectory "$INSTDIR\Manual\Images"
  File "..\..\Build\Release\Manual\Images\MainWindow1.png"
SectionEnd

Section "Docs" SEC03
  SetOutPath "$INSTDIR\Docs"
  SetOverwrite ifnewer
  File "..\..\Documents\ChangeLog.txt"
  File "..\..\Documents\Contributors.txt"
SectionEnd

Section -AdditionalIcons
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\CheckSum Tool\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\CheckSum Tool\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\CheckSumTool.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\CheckSumTool.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\Readme.txt"
  Delete "$INSTDIR\COPYING"
  Delete "$INSTDIR\CheckSumTool.exe"
  Delete "$INSTDIR\config.xml"

  Delete "$INSTDIR\Manual\Manual.html"
  Delete "$INSTDIR\Manual\Images\MainWindow1.png"
  RMDir "$INSTDIR\Manual\Images"
  RMDir "$INSTDIR\Manual"

  Delete "$SMPROGRAMS\CheckSum Tool\Uninstall.lnk"
  Delete "$SMPROGRAMS\CheckSum Tool\Website.lnk"
  Delete "$DESKTOP\CheckSum Tool.lnk"
  Delete "$SMPROGRAMS\CheckSum Tool\CheckSum Tool.lnk"

  RMDir "$SMPROGRAMS\CheckSum Tool"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd

; Function below copied from NSIS web site:
; http://nsis.sourceforge.net/How_to_insure_a_required_version_of_.NETFramework_is_installed

; Usage
; Define in your script two constants:
;   DOT_MAJOR "(Major framework version)"
;   DOT_MINOR "{Minor framework version)"
;   DOT_MINOR_MINOR "{Minor framework version - last number after the second dot)"
;
; Call IsDotNetInstalledAdv
; This function will abort the installation if the required version
; or higher version of the .NETFramework is not installed.  Place it in
; either your .onInit function or your first install section before
; other code.
Function IsDotNetInstalledAdv
   Push $0
   Push $1
   Push $2
   Push $3
   Push $4
   Push $5

  StrCpy $0 "0"
  StrCpy $1 "SOFTWARE\Microsoft\.NETFramework" ;registry entry to look in.
  StrCpy $2 0

  StartEnum:
    ;Enumerate the versions installed.
    EnumRegKey $3 HKLM "$1\policy" $2

    ;If we don't find any versions installed, it's not here.
    StrCmp $3 "" noDotNet notEmpty

    ;We found something.
    notEmpty:
      ;Find out if the RegKey starts with 'v'.
      ;If it doesn't, goto the next key.
      StrCpy $4 $3 1 0
      StrCmp $4 "v" +1 goNext
      StrCpy $4 $3 1 1

      ;It starts with 'v'.  Now check to see how the installed major version
      ;relates to our required major version.
      ;If it's equal check the minor version, if it's greater,
      ;we found a good RegKey.
      IntCmp $4 ${DOT_MAJOR} +1 goNext yesDotNetReg
      ;Check the minor version.  If it's equal or greater to our requested
      ;version then we're good.
      StrCpy $4 $3 1 3
      IntCmp $4 ${DOT_MINOR} +1 goNext yesDotNetReg

      ;detect sub-version - e.g. 2.0.50727
      ;takes a value of the registry subkey - it contains the small version number
      EnumRegValue $5 HKLM "$1\policy\$3" 0

      IntCmpU $5 ${DOT_MINOR_MINOR} yesDotNetReg goNext yesDotNetReg

    goNext:
      ;Go to the next RegKey.
      IntOp $2 $2 + 1
      goto StartEnum

  yesDotNetReg:
    ;Now that we've found a good RegKey, let's make sure it's actually
    ;installed by getting the install path and checking to see if the
    ;mscorlib.dll exists.
    EnumRegValue $2 HKLM "$1\policy\$3" 0
    ;$2 should equal whatever comes after the major and minor versions
    ;(ie, v1.1.4322)
    StrCmp $2 "" noDotNet
    ReadRegStr $4 HKLM $1 "InstallRoot"
    ;Hopefully the install root isn't empty.
    StrCmp $4 "" noDotNet
    ;build the actuall directory path to mscorlib.dll.
    StrCpy $4 "$4$3.$2\mscorlib.dll"
    IfFileExists $4 yesDotNet noDotNet

  noDotNet:
    ;Nope, something went wrong along the way.  Looks like the
    ;proper .NETFramework isn't installed.

    ;Uncomment the following line to make this function throw a message box right away
     MessageBox MB_OK "You must have v${DOT_MAJOR}.${DOT_MINOR}.${DOT_MINOR_MINOR} or greater of the .NETFramework installed.  Aborting!"
     Abort
     StrCpy $0 0
     Goto done

     yesDotNet:
    ;Everything checks out.  Go on with the rest of the installation.
    StrCpy $0 1

   done:
     Pop $4
     Pop $3
     Pop $2
     Pop $1
     Exch $0
 FunctionEnd