#!/data/data/com.termux/files/usr/bin/bash
pkg update -y && pkg upgrade -y
pkg install git wget unzip openjdk-17 dotnet -y
echo "天工开物·环境就绪"
