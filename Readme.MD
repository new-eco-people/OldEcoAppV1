Steps to my application using digital ocean

1. Register at digital ocean

2. Create a dropplet (docker installed from marketplace)  with an ssh (It is very important to add an ssh please)

3. Create another user: 
    https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-18-04

4. Install nginx to server
   https://www.digitalocean.com/community/tutorials/how-to-install-nginx-on-ubuntu-18-04-quickstart

   for testing add
   https://gist.github.com/simlegate/75b18359316cc33d8e20

   configure nginx server block for domain.com at

   nano /etc/nginx/sites-available/domain.com

   server {
        listen 80;
        listen [::]:80;
        root /var/www/html;

        server_name domain.com www.domain.com;

        location / {
                  default_type application/json;
                  return 200 '{"code":"1", "message": "Unknown Error"}';
        }

        location ~ \.php$ {
                include snippets/fastcgi-php.conf;
                fastcgi_pass unix:/var/run/php/php7.2-fpm.sock;
        }

        location ~ /\.ht {
                deny all;
        }
}

5. CI using php
https://www.digitalocean.com/community/tutorials/how-to-install-linux-nginx-mysql-php-lemp-stack-ubuntu-18-04


5. Add Lets encrypt to the server
   https://www.digitalocean.com/community/tutorials/how-to-secure-nginx-with-let-s-encrypt-on-ubuntu-18-04

6. nano ~/.bashrc 
    add at the bottom
    export GITHUB_WEBHOOK_SECRET="web hook key"
    export SENDGRID_APIKEY="send grid password"
    export CLOUDINARY_DETAILS=""
    export GITHUB_WEBHOOK_SECRET=""
    export DOCKER_AUTH_TOKEN=""
    export POSTGRES_CONNECTION_STRING=""

6. mkdir /home/agent/hooks

7. sudo nano /home/agent/hooks/redeploy-github-hook.sh

8. copy content from github folder config/hooks/redeploy-github-hook.sh to /home/agent/hooks/redeploy-github-hook.sh

* sudo visudo and add below User privilege specification
        www-data ALL= NOPASSWD:  /home/agent/hooks/redeploy-github-hook.sh

        chmod +x /home/agent/hooks/redeploy-github-hook.sh
        you may also chmod 755 /home/agent/hooks/redeploy-github-hook.sh
        

9. sudo nano /var/www/html/webhook.php

you may also chown root:root /var/www/html/webhook.php

10. copy content from config/webhook.php to /var/www/html/webhook.php
        make sure to read the parameter at the top of the php file to set desired parameters

        type sudo visudo and add www-data ALL=NOPASSWD: /home/agent/hooks/redeploy-github-hook.sh

11. Make a webhook from github to point to https://domain.com/webhook.php if using https and http://domain.com/webhook.com is using http

12. Make a push to the request to desired branch
        * error logs will be found in /tmp/

13. sudo nano /etc/nginx/sites-available/domain.com and update to 

add the content inserted by lets encrypt

upstream frontend {
    server 0.0.0.0:4200;
}

upstream backend {
    server 0.0.0.0:5000;
}

server {
        listen 80;
        listen [::]:80;
        root /var/www/html;

        server_name newecopeople.com www.newecopeople.com;

        location /api {
                proxy_pass http://backend;
        }

        location ~ \.php$ {
                include snippets/fastcgi-php.conf;
                fastcgi_pass unix:/var/run/php/php7.2-fpm.sock;
        }

        location ~ /\.ht {
                deny all;
        }

        location / {
                rewrite /(.*) /$1 break;
                proxy_pass http://frontend;
        }
}

sudo nginx -t
sudo systemctl restart nginx

sudo nano /tmp/redeploy_docker.txt