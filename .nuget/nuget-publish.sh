#!/bin/bash

tempPath=$1
source=$2

cwd=$(dirname $0)
prog_name=$(basename $0)
SCRIPTPATH="$( cd "$(dirname "$0")" ; pwd -P )"

#if [ "$tempPath" == "" ];then
#    echo Error: Empty temp path.
#    exit 1
#fi

if [ "$tempPath" == "." ] || [ "$tempPath" == "" ];then
    tempPath=$SCRIPTPATH
fi

#!param $1 project path
#!param $2 project name
#!param $3 *.csproj file path   (i.e. project1.csproj)
#!param $4 *.nuspec file path   (i.e. project1.nuspec)
#!param $5 temp path            (i.e. ./.nuget)
nugetPack() {

    #dotnet build --output "$5/.build/"  --configuration Release $3
    dotnet pack --output "$5/.pack/" --configuration Release $3
}

#!param $1 temp path            (i.e. ./.nuget)
#!param $2 source path          (i.e. http://repo.com/)
nugetAddToSource() {

    rm -r "$1/.repo/"

exit 0
    for nupkg in "$1/.pack/*";do
        #nuget add "$nupkg" -source "$1/.repo/"
        echo $nupkg

        if [ "$2" != "" ];then
            nuget add "$nupkg" -source "$2"
        fi
    done;
}

# loop & print a folder recusively,
#!param $1 root path
nugetRecursivePack() {
    echo "$1/.build/*"
    exit 0
    rm -rf "$1/.build/*"
    rm -rf "$1/.pack/*"

    for i in "$1"/*;do
        if [ -d "$i" ]; then
            nugetRecursivePack "$i"
        elif [ -f "$i" ]; then
            if grep -qe 'csproj$' << EOF
$i
EOF
            then
                path=$(dirname "$i")
                filename=$(basename "$i")
                fname="${filename%.*}"
                check="$path/$fname.nuspec"
                if [ -f "$check" ]; then
                    nugetPack "$path" "$fname" "$i" "$check" "$tempPath"
                fi
            fi
        fi
    done
}

nugetRecursivePack ..
#nugetAddToSource "$tempPath" "$source" 

exit 0