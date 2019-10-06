#!/bin/bash

giturl="https://github.com/new-eco-people/OldEcoAppV1.git"
api_image="newecopeoplev1/old_backend_dev:latest"
nginx_image="newecopeoplev1/old_nginx_dev:latest"
front_image="newecopeoplev1/old_frontend_dev:latest"

app_path="/home/agent/app"

if [ ! -d $app_path  ]
then
    mkdir $app_path
fi

cd $app_path

if [ ! ls -1qA . | grep -q .]
then 
    git clone $giturl .
fi

git checkout develop

git pull

sudo docker-compose down

sudo docker pull $api_image
sudo docker pull $nginx_image
sudo socker pull $front_image

sudo -E docker-compose up --build -d
