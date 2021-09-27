# Get the ID and security principal of the current user account
$myWindowsID = [System.Security.Principal.WindowsIdentity]::GetCurrent();
$myWindowsPrincipal = New-Object System.Security.Principal.WindowsPrincipal($myWindowsID);

# Get the security principal for the administrator role
$adminRole = [System.Security.Principal.WindowsBuiltInRole]::Administrator;

# Check to see if we are currently running as an administrator
if ($myWindowsPrincipal.IsInRole($adminRole))
{
    # We are running as an administrator, so change the title and background colour to indicate this
    $Host.UI.RawUI.WindowTitle = $myInvocation.MyCommand.Definition + "(Elevated)";
    $Host.UI.RawUI.BackgroundColor = "DarkBlue";
    Clear-Host;
}
else {
    # We are not running as an administrator, so relaunch as administrator

    # Create a new process object that starts PowerShell
    $newProcess = New-Object System.Diagnostics.ProcessStartInfo "PowerShell";

    # Specify the current script path and name as a parameter with added scope and support for scripts with spaces in it's path
    $newProcess.Arguments = "& '" + $script:MyInvocation.MyCommand.Path + "'"

    # Indicate that the process should be elevated
    $newProcess.Verb = "runas";

    # Start the new process
    $result = [System.Diagnostics.Process]::Start($newProcess);

    # Exit from the current, unelevated, process
    Exit;
}

# Run your code that needs to be elevated here...
Set-ExecutionPolicy Bypass -Scope Process -Force;
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072;

Write-Host 
@"
The following script will perform the following tasks:
- Install Chocolatey and SQLITE
- Run .sql script to create development sqlite database
"@

$permission = Read-Host -Prompt "`nAnswer [y]es to continue, or [n]o to stop execution"
while("y","n" -notcontains $permission) {
	Write-Host "Invalid user input '$permission'. Please use (y) for yes, and (n) for no";
    $permission = Read-Host;
}

if ($permission -eq 'n') {
    Write-Host "Exiting...";
    Read-Host "Press ENTER to close window"ù;
    Stop-Process -id $PID;
}

$chocoInstalled = choco -v;
if ($chocoInstalled) {
    choco upgrade chocolatey -y -f
} else {
    Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'));
}

$sqliteInstalled = sqlite3 -version;
if ($sqliteInstalled) {
    choco upgrade sqlite -y -f
} else {
    choco install sqlite -y
}

Write-Output "Chocolatey and SQLITE was installed succesfully";
Read-Host -Prompt "Press any key to close...";