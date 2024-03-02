#!/bin/bash

# En son eklenen dosyanın adını al
latest_file=$(ls -t Versiyon/*.txt | head -n 1)

# Başlangıç tarihi olarak en son eklenen dosyanın adını kullan
start_date=$(basename "$latest_file" .txt)

# Bitiş tarihi olarak bugünün tarihini al
end_date=$(date +%F)

# Versiyon klasörünü kontrol et, eğer yoksa oluştur
if [ ! -d "Versiyon" ]; then
    mkdir Versiyon
fi

# Gelen tarih bir gün öncesine alınıyor
start_date=$(date -d "$start_date - 1 day" "+%Y-%m-%d")

# Belirtilen tarih aralığındaki commit'leri kaydet
git log --since="$start_date" --until="$end_date" --pretty=format:"%h - %an, %ar : %s" --encoding=UTF-8 > "Versiyon/$end_date.txt"

echo "Commit'ler başarıyla 'Versiyon/$end_date.txt' dosyasına kaydedildi."
