# Altur Case Study

Kullanıcılar, Core Identity ile giriş yaparak ürün yönetimi işlemlerini gerçekleştirecek bir API geliştirilecek. Bu API, ürün ekleme, güncelleme, silme ve listeleme işlemlerini içerir. Entity Framework Core ve Identity kullanılarak veritabanı migrasyonu yapılacak ve Mssql veritabanı kullanılacaktır.

Uygulama, **Clean Architecture** kullanılarak düzenlenmiştir.

Normalde **appsettings.json** dosyası .gitignore konulması gerekir. İşlemler hızlı olması için pushladım.


## Features

- Kullanıcı Yönetimi ve Giriş (User Authentication)
- Ürün Ekleme (Create Product)
- Ürün Güncelleme (Update Product)
- Ürün Silme (Delete Product)
- Ürün Listeleme (List Products)


## Tech Stack

**Server:** .Net 6 SDK, MS SQL Server, Postman, Swagger, EntityFramework, Identity


## Badges

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)



## Installation

1. Repository Klonlama

```bash
  git clone https://github.com/yourusername/AlturCase.git <FileName>
```

2. Dependency'leri yükleme

```bash
  dotnet restore
```

3. Ms SQL Server kurulumundan sonra **appsettings.json** dosyasını düzenleme

```json
  "ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=AlturCaseDb;Trusted_Connection=True;"
}
```

4. Nuget Dependency Manager Console açarak database'i güncelleme (Migrationlar repodan çekilebilir ya da tekrar migration yapabilirsiniz)

```powershell
  Update-Database
```

5. Projeyi Build > Build Solution kullanarak build edebilirsiniz ya da

```powershell
  MSBuild
```
6. Projeyi Çalıştırmak için 

```powershell
  dotnet run
```

## Test Through Postman

- Postman'da test için görseldeki commenti kaldırın

  <img width="404" alt="Capture" src="https://github.com/user-attachments/assets/0ee538b9-c4a1-423f-99d7-b633fb0aadf2">

- Postamanda login işlemi yaptıktan sonra console'daki tokeni **ctrl+shift+c** ile kopyalayın.

- Product'taki API'lere erişmek için Postamanda Bearer Auth kısmına kopyaladığınız token'i yapıştırın


## API References

### Auth
#### Register

```http
  POST /api/v1/auth/register
```

| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Email, Password ` | `string` | Kayıt olmak için Email ve Password giriniz.|

#### Login

```http
  POST /api/v1/auth/login
```

| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Email, Password ` | `string` | Giriş yaptıktan sonra Header'a JWT ekler.|

### Product
#### Create
```http
  POST /api/v1/product
```

| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Name, Price, Stock, Description ` | `string, double, int, string` | Login olan kullanıcı product oluşturabilir               |
| **Bearer Auth** | **Type**| **Description** |
| `JWT Token ` | `string` | Header'da token varsa işlem authorize edilir.|

#### Get All
```http
  GET /api/v1/product
```

| Bearer Auth | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `JWT Token ` | `string` | Tüm Productları listeler|

#### Get By Id
```http
  GET /api/v1/product/{id}
```

| Param | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `Guid` | Product Id'ye göre ürün çekilebilir               |
| **Bearer Auth** | **Type**| **Description** |
| `JWT Token ` | `string` | Header'da token varsa işlem authorize edilir.|

#### Update
```http
  PUT /api/v1/product/{id}
```

| Param | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Name, Price, Stock, Description` | `string, double, int, string` | Login olan kullanıcı sadece kendi ürünlerini güncelleyebilir.              |
| **Bearer Auth** | **Type**| **Description** |
| `JWT Token ` | `string` | Header'da token varsa işlem authorize edilir.|

#### DELETE
```http
  DELETE /api/v1/product/{id}
```

| Param | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `Guid` | Login olan kullanıcı sadece kendi ürünlerini silebilir.               |
| **Bearer Auth** | **Type**| **Description** |
| `JWT Token ` | `string` | Header'da token varsa işlem authorize edilir.|



## Authors

- [@OkcuYazilimci](https://github.com/OkcuYazilimci)
