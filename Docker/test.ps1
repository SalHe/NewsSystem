for ($i = 0; $i -lt 101; $i++) {
    Write-Progress -Activity $i -PercentComplete $i -SecondsRemaining (100-$i)
    Start-Sleep 1

}