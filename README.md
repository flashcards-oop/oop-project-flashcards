# Flashcards
[![Build Status](https://travis-ci.com/flashcards-oop/oop-project-flashcards.svg?branch=master)](https://travis-ci.com/flashcards-oop/oop-project-flashcards)

Сервер и клиент приложения для запоминания терминов и их определений. Сервер представляет из себя REST API с возможностью 
хранения карточек и коллекций, генерации и проверки тестов. Клиент посылает запросы в формате JSON и предоставляет CLI-интерфейс
для взаимодействия с приложением.

## Структура проекта

Классы предметной области представлены в проекте [Flashcards](https://github.com/flashcards-oop/oop-project-flashcards/tree/master/Flashcards).
Он содержит карточки, коллекции, генераторы для разных типов вопросов.
Веб-сервер размещён в проекте [FlashcardsApi](https://github.com/flashcards-oop/oop-project-flashcards/tree/master/FlashcardsApi), клиент - 
[FlashcardsClient](https://github.com/flashcards-oop/oop-project-flashcards/tree/master/FlashcardsClient).

## Точки расширения

#### Типы заданий
Можно добавить новый тип, реализовав интерфейсы `IQuestion`, `IAnswer` и `IExerciseGenerator`.
Также понадобится добавить новый класс вопроса в клиенте, пополнив словарь [`QuestionHandler`](https://github.com/flashcards-oop/oop-project-flashcards/blob/master/FlashcardsClient/ExerciseHandler.cs).

#### Фильтры
Добавить новый способ фильтрации можно путём реализации интерфейса `IFilterConfigurator`.

## DI
DI-контейнеры собираются в файле [`Startup.cs`](https://github.com/flashcards-oop/oop-project-flashcards/blob/master/FlashcardsApi/Startup.cs)
сервера и [`Program.cs`](https://github.com/flashcards-oop/oop-project-flashcards/blob/master/FlashcardsClient/Program.cs) клиента.

## Ссылка на сервис
https://oop-flashcards.tk
