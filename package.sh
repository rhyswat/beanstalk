#!/bin/bash

DIST=dist

rm -f site.zip package.zip

(cd ${DIST} && zip -r - .) > site.zip
zip package.zip site.zip aws-windows-deployment-manifest.json