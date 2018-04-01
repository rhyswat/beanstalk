#!/bin/bash

HERE=$(dirname $0)
HERE_FULL=$(realpath $HERE)
DIST=${HERE_FULL}/dist

rm -rf ${DIST}
yarn
dotnet publish src/Beanstalk/Beanstalk.csproj -c Release -o ${DIST}