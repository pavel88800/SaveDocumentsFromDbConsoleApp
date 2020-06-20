# SaveDocumentsFromDbConsoleApp

Данное приложение отвечает за выгрузку файлов из БД. 
В приложении используется подход Database-First. таким образом мы можем сразу подключиться к уже существующей БД и работать с ней.

Данное приложение - прототип. Приложение не уверсально. То есть нельзя выкачивать данные из любой БД, т.к. структура БД везде разная.

Допустим у нас есть 3 основные таблицы:
1) таблица с договорами
2) таблица с папками
3) таблица с документами.

#Внимание!!!
Программа не учитывает вложенность большую вложенность папок

Помимо этого есть таблицы:
1) Связь договоров и папок 
2) Связь договоров и папок с файлами.
 
Принцип работы.
  1) делаем запросы с join'ами для получения связи документов и договоров
  2) Создаем папки
    2.1) Создаем папку с наименование договора/
    2.2) Создаем папки с наименованиями папок, которые записаны в БД
3) Сохраняем файлы в созданные папки.

Необходимо установить EFCore в ваше приложение.
Для использования подхода Database-First, нам надо создать модели БД.
Для этого в Консоли диспечера пакетов вводим комманду:
Scaffold-DbContext "connection_string" Microsoft.EntityFrameworkCore.SqlServer
После этого EntityFrameworkCore - автоматичски создаст модели и контекст БД. 

