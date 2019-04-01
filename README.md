# MyEvernote Uygulaması

Eğitim Amaçlı Çalışma
=========================
> - Authentication & Authorized Yönetiminin olduğu,
> - Note oluşturma. Notlara yorum yazma ve notu beğenme işlemleri.
> - Note ile ilgili CRUD işlemleri
> - Oluşturulan notların, oluşturan kişi tarafından takibi.
> - Button Helper Kullanımı


----------
### Öngereklilikler

> - Visual Studio 2017
> - Sql Server 2014 Local Db
> - .Net Framework ^4.5

 ----------

### Repository'yi indirdikten sonra

> **1)** Solution'ı sağ tıklayıp **Restore Nuget Packages**'i tıklayınız

----------

> **2)** *Nuget Package Manager Console*'dan Default Project'i MyEvernote.DataAccessLayer yaptıktan sonra "**update-database**" komutunu çalıştırınız.
> > **2-a)** Hata vermesi durumunda "**Rebuilt Solution**" yapıp projeyi kapatıp tekrar açabilirsiniz.

## Kullanılan Teknolojiler ##

 - NTier Project Pattern
 - Repository Entity Pattern
 - ASP.Net MVC 5
 - EntityFramework Code First
 - AAA (Authentication Authorization and Accounting)
 - Bootstrap,Jquery,Ajax
