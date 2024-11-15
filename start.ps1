##Starts up the applications
Start-Process "dotnet" -ArgumentList "run --project ./Webgoat.Net --launch-profile WebApplication1"
start chrome "https://localhost:5001"