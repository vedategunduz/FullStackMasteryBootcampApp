# FullStackMasteryBootcampApp

Bu proje, FullStack Mastery Bootcamp'ı kapsamında geliştirilmiş bir uygulamadır. Proje, ASP.NET Core, Entity Framework, ve SQLite kullanarak basit bir film yönetim uygulaması sunmaktadır.

## Kurulum

### Gereksinimler
Projeyi çalıştırmadan önce aşağıdaki araçların bilgisayarınızda yüklü olması gerekir:

- **.NET SDK**: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
- **SQLite**: [https://www.sqlite.org/download.html](https://www.sqlite.org/download.html)
- **Git**: [https://git-scm.com/downloads](https://git-scm.com/downloads) (Proje GitHub'a yüklendiği için Git yüklü olmalıdır)

### Adımlar

1. **Projeyi Klonlayın**
   İlk olarak, projeyi kendi bilgisayarınıza klonlayın:
   
   ```bash
   git clone https://github.com/vedategunduz/FullStackMasteryBootcampApp.git

2. **Klasöre erişin**
   ```bash
   cd FullStackMasteryBootcampApp
3. **NuGet Paketlerini Yükleyin**
   
   Proje için gerekli NuGet paketlerini yüklemek için şu komutu çalıştırın:
   ```bash
   dotnet restore
4. **Veritabanı Migrations (Yapılandırma)**

   SQLite veritabanını kurmak için migration işlemi yapın:
   ```bash
   dotnet ef database update
5. **Projeyi Başlatın**

   ```bash
   dotnet run
