#!/bin/bash

#========================================================
#creates a new version for the framework.

year=`date +"%y"`
month=`date +"%m"`
day=`date +"%d"`

version=`cut -d ',' -f2 .version`
if [ "$version" == "" ]; then
    version=0
fi
newVersion=`expr $version + 1`
sed -i "s/$version\$/$newVersion/g" .version

export AssemblyVersion="1.$year.$month$day.$newVersion"

#========================================================
#replace FrameworkInfo.CIReplace with FrameworkInfo.CIReplace.template
#and change the version and copyright

#copy the template to target.
cp ../00-Core/Architecture/Framework/FrameworkInfo.CIReplace.template.cs ../00-Core/Architecture/Framework/FrameworkInfo.CIReplace.cs

#replace the palceholders.
sed -i "s/-VERSION-/$AssemblyVersion/g"     ../00-Core/Architecture/Framework/FrameworkInfo.CIReplace.cs
sed -i "s/-YEAR-/$year/g"                   ../00-Core/Architecture/Framework/FrameworkInfo.CIReplace.cs
#uncomment file.
sed -i "s/\/\///g"                          ../00-Core/Architecture/Framework/FrameworkInfo.CIReplace.cs

#========================================================
#runs the nuget-publish to compile and publish packages.

./nuget-publish.sh .
