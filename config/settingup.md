
1. Register account with digital ocean (Others will come in the future)
2. Create a droplet with docker found in ditital ocean marketplace
3. Add SSH keys
3. Buy a domain from a register such as godaddy, google domains etc
4. Point domain name to digital ocean server using the nameserver, A hostname.com and CNAME for www
5. configure firewall to allow both port 80 and 443
*  ssh to the server by typing ssh -i path/to/private-key root@ipaddress
6. If you get "WARNING: UNPROTECTED PRIVATE KEY FILE!" then run on the local sudo chmod 600 ~/path/to/id_rsa and sudo chmod 600 ~/path/to/id_rsa.pub
    6.1. If you still have error then type = sudo chmod 644 ~/.ssh/known_hosts
7. Create user and give sudo privilidge
    https://www.digitalocean.com/community/tutorials/how-to-create-a-sudo-user-on-ubuntu-quickstart
    https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-18-04
    https://www.digitalocean.com/community/tutorials/how-to-add-delete-and-grant-sudo-privileges-to-users-on-a-debian-vps
    su - username

6. Add lets encrypt for ssl certificate
    https://www.humankode.com/ssl/how-to-set-up-free-ssl-certificates-from-lets-encrypt-using-docker-and-nginx

    sudo openssl dhparam -out /docker/letsencrypt-docker-nginx/src/production/dh-param/dhparam-2048.pem 2048



7. Configure CI/CD using GitLab https://medium.com/@sean_bradley/auto-devops-with-gitlab-ci-and-docker-compose-f931233f080f and https://docs.gitlab.com/runner/register/index.html
    Or try using webhooks https://github.com/adnanh/webhook and https://willbrowning.me/setting-up-automatic-deployment-and-builds-using-webhooks/
    https://github.com/staticfloat/docker-webhook
    https://github.com/schickling/docker-hook - also install pyton on root

    https://hub.docker.com/r/nilbus/docker-hook/
    docker run \
  --name docker-hook \
  -p 8555:8555 \
  -v /var/run/docker.sock:/var/run/docker.sock \
  -v /home/username/deploy.sh:/deploy.sh \
  nilbus/docker-hook -t key -c sh /deploy.sh



For gitlab runner after making it a sudoer type

sudo usermod -a -G sudo gitlab-runner
