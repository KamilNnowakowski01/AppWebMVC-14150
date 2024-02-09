# Projekt Zaliczeniowy - Zarządzanie Danymi o Złożonych Komputerach

Autor: Kamil Nowakowski  
Indeks: 14150

## Opis

Aplikacja internetowa stworzona w środowisku ASP.NET służąca do zarządzania danymi o złożonych komputerach. Projekt składa się z czterech głównych modułów: `AppDataBase`, `AppModel`, `AppWebAPI`, i `AppWebMVC`.

## Linki do Repozytoriów GitHub

- [AppWebMVC](https://github.com/KamilNnowakowski01/AppWebMVC-14150)
- [AppDataBase](https://github.com/KamilNnowakowski01/AppDataBase-14150)
- [AppWebAPI](https://github.com/KamilNnowakowski01/AppWebAPI-14150)
- [AppModel](https://github.com/KamilNnowakowski01/AppModel-14150)

## Proces Uruchomienia

1. Sklonuj repozytorium.
2. Zainstaluj wymagane zależności do projektów.
3. Skonfiguruj pliki `appsettings.json`, ustawiając odpowiednie ścieżki w sekcjach `DefaultConnection` i `ApplicationDbContextConnection`.
4. Wykonaj migracje i aktualizacje dla bazy danych w module `AppDataBase` używając komend `add-migration initm1` oraz `update-database`.
5. Uruchom aplikację.

## Dane Zainicjalizowanych Użytkowników

- **Administrator**  
  Login: admin@admin.com  
  Hasło: Admin123#

- **Użytkownik**  
  Login: user@user.com  
  Hasło: User123#

## Realizacja Wymagań

- Utrwalanie danych w bazie za pomocą Entity Framework (SQLite lub MSQL) w osobnym module z inicjacją przykładowymi danymi.
- Modele z relacjami między sobą, np. `Computer` zawiera `Graphics` oraz jest powiązany z `Producer`.
- Moduł Identity do logowania użytkowników z rolami administratora i zwykłego użytkownika.
- Formularze do realizacji operacji CRUD oraz widoki z listami obiektów z dostępem dla administratora.
- Kontroler WebAPI do udostępniania wybranych danych, np. pobieranie komputerów z danej kategorii.
- Dodany slajder z biblioteki `splidejs.com` oraz dodatkowe style Bootstrap dla pola formularza wielokrotnego wyboru.

## Lista Wykorzystanych Technologii

- ASP.NET Core z Target Framework `.NET 8.0`
- Entity Framework Core
- Identity Framework
- SQL Server oraz SQLite
- Bootstrap
- SplideJS dla slidera

Projekt zawiera szczegółowe informacje na temat konfiguracji, wymagań oraz instrukcji uruchomienia w poszczególnych repozytoriach.
