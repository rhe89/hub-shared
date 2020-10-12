dotnet pack --configuration Release

newestVersion=$(ls -t1 bin/Release/ | head -n 1)

dotnet nuget push "./bin/release/$newestVersion" --source "github" --skip-duplicate