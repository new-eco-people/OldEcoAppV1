------------------------------------- Deploy code

#!/usr/bin/env bash
# redirect stdout/stderr to a file
giturl=".git"
api_image="api:latest"
nginx_image="nginx:latest"
front_image="frontend:latest"

exec > /home/agent/hooks/www.newecopeople.com/output.log 2>&1

cd /home/agent/app/

if ls -1qA . | grep -q .
    then 
else 
    git clone $giturl .
    git checkout develop
fi

sudo docker pull $api_image
sudo docker pull $nginx_image
sudo socker pull $front_image

sudo -E docker-compose up --build -d

-------------------------------------hook.json code

[
  {
    "id": "redeploy-newecopeople",
    "execute-command": "/home/agent/hooks/domain.name/deploy.sh",
    "command-working-directory": "/home/agent/hooks/domain-name/app/",
    "response-message": "Executing deploy script...",
     "trigger-rule":
    {
      "and":
      [
        {
          "match":
          {
            "type": "payload-hash-sha1",
            "secret": "secret",
            "parameter":
            {
              "source": "header",
              "name": "X-Hub-Signature"
            }
          }
        },
        {
          "match":
          {
            "type": "value",
            "value": "refs/heads/master",
            "parameter":
            {
            "source": "header",
              "name": "X-Hub-Signature"
            }
          }
        },
        {
          "match":
          {
            "type": "value",
            "value": "refs/heads/master",
            "parameter":
            {
              "source": "payload",
              "name": "ref"
            }
          }
        }
      ]
    }
  }
]

/home/johndoe/go/bin/webhook -hooks /home/johndoe/hooks/hooks.json -ip "<YOUR-SERVER-IP>" -verbose
