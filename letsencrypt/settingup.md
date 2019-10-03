
1. Register account with digital ocean (Others will come in the future)
2. Create a droplet with docker found in ditital ocean marketplace
3. Buy a domain from a register such as godaddy, google domains etc
4. Point domain name to digital ocean server using the nameserver, A hostname.com and CNAME for www
5. configure firewall to allow both port 80 and 443
6. Add lets encrypt for ssl certificate
7. Configure CI/CD using GitLab https://medium.com/@sean_bradley/auto-devops-with-gitlab-ci-and-docker-compose-f931233f080f and https://docs.gitlab.com/runner/register/index.html