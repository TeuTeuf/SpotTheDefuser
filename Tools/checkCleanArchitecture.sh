#!/usr/bin/env bash

readonly DIRECTORY_DOMAIN="Assets/Scripts/Main/Domain/"
readonly DIRECTORY_USECASES="Assets/Scripts/Main/UseCases/"

readonly USING_USECASES="using Main.UseCases"
readonly USING_INFRASTRUCTURE="using Main.Infrastructure"

check_using() {
    if [ $(find $1 -name '*.cs' -exec cat {} \; | grep -E "$2" -c) -eq 0 ]
    then
        echo "OK!"
    else
        echo "Fail!"
        find $1 -name '*.cs' -exec grep -E "$2" {} -H \;
        exit 1
    fi
}

echo "Check no UseCase or Infrastructure usage in Domain:"
check_using "$DIRECTORY_DOMAIN" "$USING_USECASES|$USING_INFRASTRUCTURE"

echo "Check no Infrastructure usage in UseCases:"
check_using "$DIRECTORY_USECASES" "$USING_INFRASTRUCTURE"