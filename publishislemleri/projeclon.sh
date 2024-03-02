#!/bin/bash




# Log dosyasının adı ve yolu
log_file="C:/Users/samet.turker/source/repos/TURKERBEY/DHBYS/publishislemleri/script_log.txt"

# Betik başladığında mevcut log dosyasını sil
rm -f "$log_file"

# Betik başladığında bir başlangıç mesajı ekle
echo "Betik başlatıldı: $(date)" >> "$log_file"

# Mevcut 'origin' uzak depoyu kaldır
git remote remove origin

git remote add origin https://github.com/TURKERBEY/Versiyon.git
if git rev-parse --verify master &> /dev/null; then
    git push origin --delete master || { echo "Master şubesi silinemedi" >> "$log_file"; exit; }
else
    echo "Şube 'master' zaten mevcut değil."
fi


# Kaynak dosya yolu
source_folder="/C/Users/samet.turker/source/repos/TURKERBEY/DHBYS"

# Hedef dizin
target_folder="/C/Versiyonlar"

# Git'ten projenin son halini çek
echo "Git: Hedef dizine gidiliyor..." >> "$log_file"
cd "$target_folder" || { echo "Hedef dizine gidilemedi" >> "$log_file"; exit; }

# Versiyon numarasını al
echo "Versiyon numarası okunuyor..." >> "$log_file"
version_number=$(<$source_folder"/"version.txt)

# Parçaları ayır
IFS='.' read -ra version_parts <<< "$version_number"

# Son parçayı arttır
last_part=${version_parts[-1]}
((last_part++))

# Parçaları tekrar birleştir
new_version="${version_parts[0]}.${version_parts[1]}.${version_parts[2]}.${version_parts[3]}.$last_part"

echo "$new_version" > "$source_folder/version.txt" || { echo "Versiyon numarası güncellenemedi" >> "$log_file"; exit; }

folder_name="version_$version_number"
mkdir "$folder_name" || { echo "Klasör oluşturulamadı" >> "$log_file"; exit; }

# Kaynak dosyaları hedef klasöre kopyala
echo "Dosyalar kopyalanıyor..." >> "$log_file"
cp -r "$source_folder"/* "$target_folder/$folder_name" || { echo "Dosyalar kopyalanamadı" >> "$log_file"; exit; }

# Hedef dizine git init yapma
echo "Git: Hedef dizine git init yapılıyor..." >> "$log_file"
cd "$target_folder/$folder_name" || { echo "Hedef dizine gidilemedi" >> "$log_file"; exit; }





# Değişiklikleri ekle, commit yap ve GitHub'a gönder
git init || { echo "Git init başarısız" >> "$log_file"; exit; }
git add . || { echo "Dosyalar ekleme başarısız" >> "$log_file"; exit; }
git commit -m "Yeni versiyon $folder_name oluşturuldu" || { echo "Commit başarısız" >> "$log_file"; exit; }
git remote add origin https://github.com/TURKERBEY/Versiyon.git || { echo "Uzak repo ekleme başarısız" >> "$log_file"; exit; }

git push -u origin master || { echo "Master şubesi uzak sunucuya gönderilemedi" >> "$log_file"; exit; }


# Betik sona erdiğinde bir bitiş mesajı ekle
echo "Betik tamamlandı: $(date)" >> "$log_file"
