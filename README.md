# 🏪 Shopy-Stock
## API de gestíon de pedidos y productos.

---

## 🛠️ Tecnologías utilizadas

<table align="center">
  <tr>
    <td width="120" align="center" style="padding:10px; border:2px solid #512BD4; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dotnetcore/dotnetcore-original.svg" width="50" alt="ASP.NET" />
      <br><strong style="color:white;">ASP.NET</strong>
    </td>
    <td width="120" align="center" style="padding:10px; border:2px solid #239120; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" width="50" />
      <br><strong style="color:white;">C#</strong>
    </td>
    <td width="120" align="center" style="padding:10px; border:2px solid #CC2927; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" width="50" />
      <br><strong style="color:white;">SQL Server</strong>
    <td width="120" align="center" style="padding:10px; border:2px solid #2496ED; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-original.svg" width="50" />
      <br><strong style="color:white;">Docker</strong>
    </td>
    </td>
  </tr>  
</table>

<table align="center">
  <tr>
    <td width="120" align="center" style="padding:10px; border:2px solid #3178C6; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/entityframeworkcore/entityframeworkcore-original.svg" width="50"/>
      <br><strong style="color:white;">Entity Framework Core</strong>
    </td>
    <td width="120" align="center" style="padding:10px; border:2px solid #61DAFB; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/swagger/swagger-original.svg" width="50"/>
      <br><strong style="color:white;">Swagger</strong>
    </td>
    <td width="120" align="center" style="padding:10px; border:2px solid #61DAFB; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://api.nuget.org/v3-flatcontainer/fluentvalidation/12.1.1/icon" width="50" />
      <br><strong style="color:white;">FluentValidation</strong>      
    </td>
    <td width="120" align="center" style="padding:10px; border:2px solid #61DAFB; border-radius:10px; background-color:#1e1e1e;">
      <img src="https://cloud.githubusercontent.com/assets/5763993/26522718/d16f3e42-4330-11e7-9b78-f8c7402624e7.png" width="50" />
      <br><strong style="color:white;">Mapster</strong>      
    </td>
  </tr>
</table>

---

# ⚙️ Decisiones de arquitectura

### Arquitectura del proyecto

Para la realizacion de este proyecto se utilizo **Clean Architecture**, esto debido a las ventajas que este nos brinda:

- **Descoplamiento**: Esta arquitectura por su nivel de desacoplamiento, nos facilita configuraciones vitales a futuro, como cambio de ORM, algun apartado de la logica de negocio, etc.
- **Escalabilidad**: Se pueden realizar bastantes mejoras a futuro por el punto anterior, al tener las capas de application y domain aislados, esto nos permite mejorar procesos sin la necesidad de alterar todo el codigo.
- **Pruebas**: Debido a los puntos anteriores es posible hacer pruebas unitarias mas facilmente, sin la necesidad de usar bases de datos reales.

### Capas utilizadas

#### Domain
Nucleo del negocio que contiene las entidades, reglas de negocio puras y contratos (interfaces) sin dependencias externas.
#### Application
Capa de casos de uso que maneja la logica de la aplicacion, DTOs mapeados con Mapster, interfaces y las validaciones mediante FluentValidation.
#### Infraestructure
Capa encargada de los detalles tecnicos y de persistencia, incluyendo Entity Framework Core, DbContext, repositorios, UnitOfWork y la conexión a la base de datos.
#### Presentation(API)
Capa mas externa que expone los endpoints HTTP a través de controladores, gestiona el middleware global de errores y configura la inyección de dependencias.

### Patrones de diseño

Para mejorar el desarrollo futuro y experiencia de desarrollo se utilizaron los siguientes patrones:
 
- **Repositios y UnitOfWork**: Utilizados en Infraestructure para abstraer el acceso a la base de datos mediante Entity Framework.
- **Inyeccion de dependencias**: Usada en la capa de Presentacion(API), permite que las capas internas expongan la logica de negocio y se implementen correctamente en las capas externas. 
- **Patron Middleware**: Utilizado en la capa de Presentacion(API) para estandarizar los errores para el frontend.
- **Patron Validator**: Utilizado en Application para la validacion de datos ingresados por los usuarios mediante FluentValidation.
- **Patron DTO**: Usado en Application y Presentacion para facilitar los datos para los usuarios de la API, para lograrlo se utilizo Mapster ya que este facilita mucho esta tarea.

### Conexion a la Base de Datos

El proyecto actualmente cuenta con un Docker Compose que se encarga de desplegar tanto la API, como una imagen de SQL Server 22, la cadena de conexion actual cuanta con este formato:
```
Server=sqlserver,1433;Database=ShopyStockDb;User Id=sa;Password=Contraseña;TrustServerCertificate=True;
``` 

La misma se sube desde el DockerCompose y la contraseña la carga desde un `.env`. Aca mismo esta un `.env.example` con la key requerida para el despliegue.

---
# 🚀 Despliegue

### 🏗️ Nativo en tu pc
#### Requisitos:

- Contar con el **SDK** de **.NET 8** instalado.
- Contar con **Microsoft SQL Server** instalado.
- (Preferiblemente) Contar con **SQL Server Management Studio** instalado.
- Tener una copia del proyecto.

#### Pasos:

1. Ir al **appsettings.json** del proyecto y agregar la cadena de conexion:
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=PruertoDelServidor;Database=NombreDeLaBaseDeDatos;User Id=sa;Password=Contraseña;TrustServerCertificate=True;"
  }
```
2. Cambiar los campos **Server**, **Database**, **Password** por los correspondientes.
3. Ejecutar el siguiente comando en la carpeta con el codigo del proyecto, [src](src).
```bash
dotnet run --project ShopyStock.API/ShopyStock.API.csproj
```

Eso es todo, el propio codigo en el program de la **API** se encarga de ejecutar las migraciones para la base de datos en el **ConnectionString**, se configuro de esta manera ya que el **compose.yml** no cuenta con configuracion para guardar los cambios, si quieres remover esos cambios, solo borra esta linea del **program.cs**:
```bash
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StockDbContext>();
    await dbContext.Database.MigrateAsync();
}
```
Si quieres tener los datos de las migraciones igual tranquilo, estos estan en [init.sql](init.sql), solo copialos y ejecutalo en el **SSMS**.

### 🐳 Mediante Docker

#### Requisitos:
- Tener Docker instalado en tu maquina.
- Y una copia del proyecto.

#### Pasos:

Para el despliegue del proyecto se deben seguir los siguientes pasos luego de hacer la copia del proyecto:

1. Crear un `.env` en la carpeta raiz del proyecto.
2. Copiar las variables de entorno de prueba del `.env.example` del proyecto.
3. Cambiar la contraseña que se usara para la base de datos y la cadena de conexion por la que consideres.
4. Ejecutar el siguiente comando en la carpeta raiz del proyecto:
```bash
docker compose up --build -d
```

#### Cosas a tomar en cuenta:

Los puertos configurados por defecto para el despliegue son:

- SQL Server: 1433
- API: 5000

El proyecto cuenta con un `.http` para las pruebas iniciales llamado **Peticiones.http**, este se encuentra en la carpeta [docs](./docs), el mismo esta configurado para que se ejecuten en esos puertos, asi que si cambias los puertos del **compose.yml**, debes cambiar la ruta de la **API** en el **Peticiones.http**.

Si prefieres probar manualmente la **API**