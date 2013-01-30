$iisExpressExe = '"c:\Program Files (x86)\IIS Express\iisexpress.exe"'
$redisExe = '"c:\redis\redis-server.exe"'
#$iisExpressExe = "iisexpress"
$ports = @(8091,8092)
$path = (Resolve-path .)
Write-host "Starting redis"
cmd /c start cmd /k "$redisExe"
Start-Sleep -m 2000
foreach($port in $ports)
{
	Write-Host $path
	Write-host "Starting site on port: $port"
	$params = "/port:$port /path:$path"
	$command = "$iisExpressExe $params"
	cmd /c start cmd /k "$command"
	Start-Sleep -m 1000
}

Get-Job
