#!/data/data/com.termux/files/usr/bin/bash
cd ~/TianGongKaiWu
git pull origin main
git add .
read -p "提交说明: " msg
git commit -m "$msg"
git push origin main
echo "已推送，GitHub 正在造化 APK..."
