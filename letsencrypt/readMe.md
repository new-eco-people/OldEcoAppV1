

set server to allow password for sudo users
nano /etc/ssh/sshd_config and set PasswordAuthentication to yes and Permitrootlogin to no
finally run service ssh restart

1. 

sudo mkdir -p /docker/letsencrypt-docker-nginx/src/letsencrypt/letsencrypt-site

copy docker-compose.yml from letsencrypt folder to 
/docker/letsencrypt-docker-nginx/src/letsencrypt

copy nginx.conf to
/docker/letsencrypt-docker-nginx/src/letsencrypt/


2. sudo into /docker/letsencrypt-docker-nginx/src/letsencrypt
3. type sudo docker-compose up -d to test the app is working

4. to test if everything goes well without risk run this 
sudo docker run -it --rm -v /docker-volumes/etc/letsencrypt:/etc/letsencrypt -v /docker-volumes/var/lib/letsencrypt:/var/lib/letsencrypt -v /docker/letsencrypt-docker-nginx/src/letsencrypt letsencrypt-site:/data/letsencrypt -v "/docker-volumes/var/log/letsencrypt:/var/log/letsencrypt" certbot/certbot certonly --webroot --register-unsafely-without-email --agree-tos --webroot-path=/data letsencrypt --staging -d newecopeople.com -d www.newecopeople.com

5. (Optional) To get more info about the Certificate gotten
    sudo docker run --rm -it --name certbot \
-v /docker-volumes/etc/letsencrypt:/etc/letsencrypt \
-v /docker-volumes/var/lib/letsencrypt:/var/lib/letsencrypt \
-v /docker/letsencrypt-docker-nginx/src/letsencrypt/letsencrypt-site:/data/letsencrypt \
certbot/certbot \
--staging \
certificates

6. After successful testing remove test certificate 
sudo rm -rf /docker-volumes/

7. Run the code for the real certificate

sudo docker run -it --rm \
-v /docker-volumes/etc/letsencrypt:/etc/letsencrypt \
-v /docker-volumes/var/lib/letsencrypt:/var/lib/letsencrypt \
-v /docker/letsencrypt-docker-nginx/src/letsencrypt/letsencrypt-site:/data/letsencrypt \
-v "/docker-volumes/var/log/letsencrypt:/var/log/letsencrypt" \
certbot/certbot \
certonly --webroot \
--email youremail@domain.com --agree-tos --no-eff-email \
--webroot-path=/data/letsencrypt \
-d newecopeople.com -d www.newecopeople.com

8. shut down letsencrypt docker compose 
cd /docker/letsencrypt-docker-nginx/src/letsencrypt
sudo docker-compose down

9. Generate a 2048 bit DH Param file
sudo openssl dhparam -out /docker/letsencrypt-docker-nginx/src/production/dh-param/dhparam-2048.pem 2048
