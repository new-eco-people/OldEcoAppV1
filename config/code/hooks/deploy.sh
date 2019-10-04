#!/usr/bin/env bash
# redirect stdout/stderr to a file
giturl=".git"
api_image="newecopeoplev1/old_backend_dev:latest"
nginx_image="newecopeoplev1/old_nginx_dev:latest"
front_image="newecopeoplev1/old_frontend_dev:latest"

exec > /home/agent/hooks/www.newecopeople.com/output.log 2>&1

cd /home/agent/app/

# if ls -1qA . | grep -q .
#     then 
# else 
#     git clone $giturl .
#     git checkout develop
# fi

docker-compose down

docker pull $api_image
docker pull $nginx_image
socker pull $front_image

-E docker-compose up --build -d