# Pages & Familia — Sistema de Gestión para Constructora

Sistema web para gestionar una empresa constructora: obras y sus etapas, clientes, empleados, contratistas, proveedores, materiales, compras y gastos.

Proyecto desarrollado por **Cristian Carro** y **Alen Ravel** como parte de la carrera de Analista Programador (CTC, Uruguay).

![Captura principal](docs/captura-principal.png)

---

## ✨ Funcionalidades

- **Obras y etapas**: alta, baja, modificación y seguimiento de cada obra dividida en etapas
- **Clientes**: gestión de clientes y su relación con una o varias obras
- **Empleados**: registro de empleados y asignación a obras
- **Contratistas**: gestión de contratistas y su vínculo con obras
- **Proveedores y materiales**: catálogo de materiales y proveedores
- **Compras y gastos**: registro de compras de materiales con su detalle y gastos por obra
- **Costos**: el costo actual de cada obra se calcula a partir de sus datos, sin guardarse duplicado

---

## 🛠️ Stack

| Capa | Tecnología |
|------|-----------|
| Frontend | Next.js (React) |
| Backend | C# / ASP.NET Core Web API (.NET 10) |
| ORM | Entity Framework Core (migraciones) |
| Base de datos | MySQL |

**Estructura del repo**

```
ConstructoraPages/              → API en ASP.NET Core
constructora-pages-frontend/    → App en Next.js
docs/                           → Capturas de pantalla
```

---

## 🏗️ Arquitectura

**Backend**: API REST con arquitectura en capas:

```
Controller/    → Endpoints REST
Service/       → Lógica de negocio
Repository/    → Acceso a datos
Entity/        → Entidades del dominio
Dto's/         → Objetos de transferencia de datos
Data/          → DbContext de Entity Framework
Migrations/    → Migraciones de la base de datos
```

**Base de datos**: diseñada de forma iterativa. Algunas decisiones de diseño:
- Se resolvió una dependencia circular de claves foráneas entre `Obra` y `EtapaObra`
- Relación muchos a muchos entre obras y clientes mediante `ObraCliente`
- `costo_actual` se calcula en vez de almacenarse, para evitar inconsistencias

---

## 🚀 Cómo correrlo localmente

### Requisitos
- Node.js 18+
- .NET 10 SDK
- MySQL Server 8

### 1. Clonar el repo
```bash
git clone https://github.com/criscarro/ConstructoraPages.git
cd ConstructoraPages
```

### 2. Backend y base de datos
1. En la carpeta del proyecto de la API, copiar `appsettings.Example.json` como `appsettings.json` y completar el connection string:
   ```json
   "DefaultConnection": "Server=localhost;port=3306;Database=ConstructoraPages;user=root;password=TU_CONTRASEÑA;"
   ```
2. Crear la base con las migraciones:
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```
3. Levantar la API:
   ```bash
   dotnet run
   ```
   Queda disponible en `https://localhost:7091`.

### 3. Frontend
```bash
cd constructora-pages-frontend
npm install
npm run dev
```

La app queda disponible en `http://localhost:3000`.

### 4. Datos de ejemplo (opcional)
Con la API corriendo, desde `constructora-pages-frontend`:
```bash
node seed.js
node seedCompra.js
```

---

## 📸 Capturas

| Listado de obras | Detalle de obra |
|---|---|
| ![](docs/obras.png) | ![](docs/detalle.png) |

---

## 📄 Documentación

El proyecto incluye documentación de Ingeniería de Software con historias de usuario (formato Dado / Cuando / Entonces) y requisitos no funcionales medibles.

---

## 👥 Autores

- **Cristian Carro**: [GitHub](https://github.com/criscarro) · [Portfolio](https://criscarro.github.io/Portfolio/)
- **Alen Ravel**: [GitHub](#) <!-- completar -->
