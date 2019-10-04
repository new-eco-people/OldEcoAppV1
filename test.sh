#!/usr/bin/env bash

if ls -1qA . | grep -q .
    then 
else 
    echo "empty"
fi