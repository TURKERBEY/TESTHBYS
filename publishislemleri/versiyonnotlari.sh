#!/bin/bash

# Belirtilen dosya klasöründen en son eklenen not defterini bul
latest_file=$(ls -t "C:/Users/samet.turker/source/repos/TURKERBEY/DHBYS/DHBYS/Versiyon"/*.txt | head -n 1)

# Dosyanın adını al
file_name=$(basename "$latest_file")

# Dosyanın içeriğini oku
latest_file2=$(<"C:/Users/samet.turker/source/repos/TURKERBEY/DHBYS/version.txt")
folder_name=$(basename "$latest_file2")

# Yeni klasör adını oluştur
target_directory="C:/Users/samet.turker/source/repos/TURKERBEY/DHBYS/DHBYS/wwwroot/app-assets/VersiyonNotlari/$folder_name"
mkdir -p "$target_directory" || exit

# Dosyayı yeni klasöre kopyala
cp "$latest_file" "$target_directory/$file_name" || exit

echo "Dosya başarıyla kopyalandı ve yeni klasöre taşındı."
