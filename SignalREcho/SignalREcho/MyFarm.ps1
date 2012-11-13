$iisExpressExe = '"c:\Program Files (x86)\IIS Express\iisexpress.exe"'
#$iisExpressExe = "iisexpress"
$ports = @(8090, 8091)
$path = (Resolve-path .)

foreach($port in $ports)
{
	Write-Host $path
	Write-host "Starting site on port: $port"
	$params = "/port:$port /path:$path"
	$command = "$iisExpressExe $params"
#	$scriptBlock = {
	cmd /c start cmd /k "$command"
#	}
#	Start-job $scriptBlock
}

Get-Job
