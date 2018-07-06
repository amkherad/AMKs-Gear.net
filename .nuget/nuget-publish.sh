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
#!param $4 temp path            (i.e. ./.nuget)
nugetPack() {

    #dotnet build --output "$4/.build/"  --configuration Release $3
    dotnet pack --output "$4/.pack/" --configuration Release $3  --no-dependencies
}

#!param $1 temp path            (i.e. ./.nuget)
#!param $2 source path          (i.e. http://repo.com/)
nugetAddToSource() {

    rm -r "$1/.repo/*"
    PACK_PATH="$1/.pack/*"
    for nupkg in $PACK_PATH;do
        if [ "$2" != "" ];then
            nuget add "$nupkg" -source "$2"
        fi

        nuget add "$nupkg" -source "$1/.repo/"
    done;
}

# loop & print a folder recusively,
#!param $1 root path
nugetRecursivePack() {
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
                check="$path/$fname.ignore-pack"
                if [ ! -f "$check" ]; then
                    nugetPack "$path" "$fname" "$i" "$tempPath"
                fi
            fi
        fi
    done
}

nugetRecursivePack ..
nugetAddToSource "$tempPath" "$source" 

exit 0
