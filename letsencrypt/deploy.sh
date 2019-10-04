#!/bin/bash

cd /letsencrypt
docker-compose build --pull && docker-compose up --build --remove-orphans -d