# Конвертация курса в Edx

1. Создайте курс на платформе Edx. Обычно это делают администраторы и дают права на редактирование курса в студии, например, в studio.openedu.ru.
2. Создайте config-файл для конвертора. Для этого в директории с курсом ulearn убедитесь, что файла config.xml ещё нет и выполните команду course convert. 
Откроется редактор с шаблоном файла — заполните там все значения.
3. Скачайте пустой курс из студии (Инструменты — Экспорт) и положите в директорию с конфиг-файлом. Убедитесь, что это единственный tar.gz файл в этой директории.
4. TODO доделать