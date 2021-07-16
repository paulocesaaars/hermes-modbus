# hermes-modbus
Sistema de integração de dispositivo Modbus TCP para broker MQTT.

# Docker build 
docker build -f Api-Dockerfile -t deviothermesmodbustcpapi .

# Docker-compose
docker-compose -f docker-compose.yml up --build

# Others
--restart unless-stopped -it -d

# Gerar certificado local
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Paula@123
dotnet dev-certs https --trust
