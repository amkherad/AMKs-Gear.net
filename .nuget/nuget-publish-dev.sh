#!/bin/bash

year=`date +"%y"`
month=`date +"%m"`
day=`date +"%d"`

version=`cut -d ',' -f2 .version`
if [ "$version" == "" ]; then
    version=0
fi
newVersion=`expr $version + 1`
sed -i "s/$version\$/$newVersion/g" .version

newVersion=1

export AssemblyVersion="1.$newVersion.$year$month$day"

./nuget-publish.sh .
