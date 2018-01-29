# json schema faker
## install
sudo npm install -g json-schema-faker-cli
## genrate data ex
generate-json schema.json output.json 100
# mongodb
## install on stretch
sudo apt-get update && sudo apt-get upgrade -y
sudo apt-get install mongodb
## start
systemctl start mongodb
## stop
systemctl stop mongodb
## server status 
systemctl status mongodb
## mongo repl
mongo
## create database
use isins
## create collection
db.createCollection('foreign_exchange')

