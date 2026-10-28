# TestAI — примеры телефонной книги на C#

Учебная коллекция: одна и та же задача — показать телефонную книгу в консоли — решена
пять раз с разной степенью детализации. Один вариант, [Deepseek_Harness](Deepseek_Harness/README.md),
доведён до полноценного проекта .NET 10 с параметрами командной строки и JSON-хранилищем;
остальные четыре — компактные одиночные файлы, созданные в диалогах с ИИ-ассистентами
(Alice, DeepSeek, GigaChat, Kodik).

## Состав репозитория

| Папка | Что демонстрирует | Формат | Запуск |
| --- | --- | --- | --- |
| [`Deepseek_Harness`](Deepseek_Harness/README.md) | Таблица записей с итогами и разбивкой по категориям, сортировка, свой JSON-файл, коды возврата | Проект `net10.0` | `dotnet run --project Deepseek_Harness` |
| [`AliceAI`](AliceAI/README.md) | Класс `Contact` (имя, телефон, email), список из трёх записей, пауза по нажатию клавиши | Один файл `Program.cs` | `dotnet run AliceAI/Program.cs` |
| [`DeepSeek`](DeepSeek/README.md) | Два варианта: `edition_1` — класс и список с табличным выводом, `edition_2` — чтение из `phonebook.json` | Два файла `Program.cs` | `dotnet run DeepSeek/edition_1/Program.cs` |
| [`GigaChat`](GigaChat/README.md) | Интерактивное меню: просмотр, добавление, поиск по имени, выход | Один файл `Program.cs` | `dotnet run GigaChat/Program.cs` |
| [`KodikChat`](KodikChat/README.md) | Минимальный пример: три контакта и вывод в цикле `foreach` | Один файл `Program.cs` | `dotnet run KodikChat/Program.cs` |

У четырёх «одиночных» вариантов намеренно нет `.csproj`: это компактные примеры кода,
а не самостоятельные решения. Полноценный проект в репозитории один — `Deepseek_Harness`.

## Требования

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) или новее (`dotnet --version`).
- Windows, Linux или macOS. Вывод в UTF-8: кириллица и псевдографика корректно
  отображаются в Windows Terminal, PowerShell 7 и современных консолях.

## Быстрый старт

Полноценный проект — телефонная книга в виде таблицы:

```bash
dotnet run --project Deepseek_Harness
```

Сортировка по имени или категории, произвольный файл данных и справка:

```bash
dotnet run --project Deepseek_Harness -- --sort category
dotnet run --project Deepseek_Harness -- --file /path/to/contacts.json
dotnet run --project Deepseek_Harness -- --help
```

Release-сборка:

```bash
dotnet build Deepseek_Harness -c Release
./Deepseek_Harness/bin/Release/net10.0/PhoneBookViewer --sort name
```

## Одиночные примеры

Начиная с .NET 10 файл `*.cs` можно запускать напрямую, без файла проекта:

```bash
dotnet run AliceAI/Program.cs          # список контактов
dotnet run DeepSeek/edition_1/Program.cs
dotnet run KodikChat/Program.cs
```

Особенности:

- **`AliceAI`** в конце ждёт нажатия клавиши (`Console.ReadKey`), поэтому нужен
  интерактивный терминал: при перенаправленном вводе программа выведет список, а затем
  завершится исключением.
- **`DeepSeek/edition_2`** читает `phonebook.json` из текущего каталога. Положите файл
  рядом или запускайте из его папки; при отсутствии файла программа сообщит об этом и выйдет.
- **`GigaChat`** работает в режиме диалога: пункты меню вводятся с клавиатуры.

Альтернатива для любого примера — создать консольный проект и вставить код в `Program.cs`:

```bash
dotnet new console -n PhoneBookDemo
cd PhoneBookDemo
# заменить Program.cs содержимым нужного примера
dotnet run
```

## Структура репозитория

```
TestAI/
├── .editorconfig                 — стиль кода (C#: отступ 4, правила именования)
├── .gitattributes                — нормализация переводов строк, diff для .cs/.md
├── .gitignore                    — артефакты сборки и файлы IDE
├── LICENSE                       — MIT
├── README.md                     — этот файл
├── AliceAI/
│   ├── Program.cs
│   └── README.md
├── DeepSeek/
│   ├── README.md
│   ├── edition_1/Program.cs
│   └── edition_2/Program.cs
├── Deepseek_Harness/
│   ├── PhoneBookViewer.csproj    — проект net10.0
│   ├── Program.cs                — точка входа
│   ├── CliOptions.cs             — разбор аргументов
│   ├── Models/PhoneBookEntry.cs
│   ├── Services/PhoneBookRepository.cs
│   ├── Ui/TableRenderer.cs
│   ├── data/phonebook.json       — демонстрационные данные
│   └── README.md
├── GigaChat/
│   ├── Program.cs
│   └── README.md
└── KodikChat/
    ├── Program.cs
    └── README.md
```

## Соглашения репозитория

- **Стиль кода** задаётся `.editorconfig` (создан шаблоном `dotnet new editorconfig`):
  отступ 4 пробела для C#, 2 — для XML/JSON, пробелы вместо табов.
- **Переводы строк** нормализуются `.gitattributes`: в репозитории хранится LF,
  рабочая копия на Windows может использовать CRLF.
- **Артефакты сборки** (`bin/`, `obj/`, `.vs/`) не версионируются — см. `.gitignore`.
- Коммиты подписываются GPG-ключом.

## Зеркала

Репозиторий публикуется в трёх местах:

- GitHub — <https://github.com/StarIT-AnSt/TestAI>
- GitFlic — <https://gitflic.ru/project/starit-anst/testai>
- GitVerse — <https://gitverse.ru/starit/TestAI>

## Источники примеров

- `AliceAI` — диалог с Yandex Alice: <https://alice.yandex.ru/?share=da7dd70d-fc76-8f54-b06b-465c1ec55e4f>
- `GigaChat` — публикация в GigaChat: <https://giga.chat/link/gcsywhiVEC>
- `DeepSeek`, `KodikChat`, `Deepseek_Harness` — ссылки на диалоги не сохранились.

## Лицензия

Проект распространяется по лицензии [MIT](LICENSE). Copyright (c) 2026 Starinin Andrey.
