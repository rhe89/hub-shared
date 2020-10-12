version=$1

csproj_files=($(find * -name *.csproj))

for csproj_file in "${csproj_files[@]}"
do 
    if (grep -q "Project Sdk=\"Microsoft.NET.Sdk\"" "$csproj_file"); then
        project_folder=${csproj_file##*/}
        project_folder=${project_folder%.csproj*}

        cd "$project_folder"
        
        regex='PackageReference Include="([^"]*)" Version="([^"]*)"'
        find . -name "*.*proj" | while read proj
        do
          while read line
          do
            if [[ $line =~ $regex ]]
            then
              name="${BASH_REMATCH[1]}"
              version="${BASH_REMATCH[2]}"
              if [[ $version != *-* ]]
              then
                dotnet add $proj package $name
              fi
            fi
          done < $proj
        done

#        dotnet pack -p:PackageVersion="$version" --configuration Release
#        
#        newestVersion=$(ls -t1 bin/Release/ | head -n 1)
#        
#        dotnet nuget push "./bin/release/$newestVersion" --source "github" --skip-duplicate        
        
        cd ../
    fi
done
