# 📄 Control de Solicitudes de Documentos Académicos (UCNE)

Sistema web desarrollado para el **Departamento de Registro Académico de la Universidad Católica Nordestana (UCNE)**, con el objetivo de **digitalizar, centralizar y optimizar** el proceso de solicitud, seguimiento y emisión de documentos académicos.

Este proyecto fue desarrollado como **proyecto académico** utilizando tecnologías modernas, priorizando la **seguridad**, la **trazabilidad** y una **experiencia de usuario clara e intuitiva**.

---

## 🏫 Contexto del Proyecto

Tradicionalmente, la gestión de solicitudes académicas se realiza de forma manual o parcialmente digital, lo que genera:

- Retrasos en la entrega de documentos  
- Riesgo de pérdida de información  
- Dificultad para el seguimiento de solicitudes  
- Dependencia de archivos físicos  

Este sistema transforma ese proceso en una **plataforma web centralizada**, permitiendo al personal administrativo gestionar todo el flujo de trabajo de forma eficiente y segura.

---

## 🚀 Funcionalidades Principales

### 👨‍🎓 Módulo de Gestión de Estudiantes
- Crear, editar y consultar estudiantes
- Importación masiva desde archivos **CSV**
- Filtros por matrícula, nombre, carrera y estado (egresado / no egresado)

### 📑 Módulo de Gestión de Solicitudes (Núcleo del Sistema)
- Crear solicitudes de documentos académicos
- Seguimiento del estado: **Iniciado → En Proceso → Finalizado**
- Edición progresiva de solicitudes
- Subida de documentos PDF finales
- Historial de trabajo por registrador
- Filtros avanzados y búsqueda
- Opción **“Solo mis documentos”** para cada registrador

### 👥 Administración de Usuarios (Registradores)
- Crear, editar y deshabilitar usuarios
- Control de acceso por roles:
  - **Administrador**
  - **Registrador**

### ⚙️ Configuración y Seguridad
- Opción para **requerir PDF con hash SHA256** al finalizar solicitudes
- Configuración de recibo de pago obligatorio
- Selección de documentos más solicitados (accesos rápidos)
- Rehabilitación de entidades deshabilitadas:
  - Estudiantes
  - Solicitudes
  - Registradores

### 📎 Módulos Adicionales
- Dashboard con estadísticas generales
- Visualización de documentos PDF
- Créditos del proyecto
- Sección de ayuda / guía de uso

---

## 🔐 Seguridad del Sistema

- Autenticación y autorización con **ASP.NET Identity**
- Control de acceso por roles
- Contraseñas almacenadas de forma segura (hash)
- Acceso mediante **HTTPS**
- Generación de **hash SHA256** para documentos PDF
- Registro de acciones por usuario (trazabilidad)

---

## 🛠️ Tecnologías Utilizadas

- **Blazor Server (.NET 10)**
- **ASP.NET Core**
- **Entity Framework Core**
- **PostgreSQL**
- **Cloudflare R2** (almacenamiento de PDFs)
- **Bootstrap + CSS personalizado**
- **ASP.NET Identity**

---

## ⚙️ Configuración del Proyecto (IMPORTANTE)

Antes de ejecutar el sistema, es **obligatorio configurar correctamente** el archivo `appsettings.json`.

---

### 🗄️ Base de Datos (PostgreSQL)

El sistema **requiere obligatoriamente una base de datos PostgreSQL**.

Debes proporcionar una **connection string válida**, ya sea local o en la nube (Neon, Supabase, Railway, etc.).

Ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=TU_HOST;Port=5432;Database=TU_DB;Username=TU_USUARIO;Password=TU_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;"
  }
}
````

> ⚠️ **Nota:**
> El sistema no está diseñado para SQL Server ni SQLite.
> **PostgreSQL es obligatorio.**
Sí 👍, **el contenido está correcto**, pero a nivel de **estilo README profesional** se puede pulir bastante para que:

* Se vea consistente con el resto del documento
* Sea más claro para un profesor o evaluador técnico
* Use mejor jerarquía visual (títulos, listas, bloques de código)

Te dejo **la versión mejorada**, lista para **copiar y pegar**, con el mismo contenido pero mejor presentado 👇

---

## 🗄️ Respaldo (Backup) de la Base de Datos

Como parte de los **requisitos académicos del proyecto**, se incluye un **respaldo completo de la base de datos PostgreSQL**, generado desde **Neon** utilizando herramientas estándar (`pg_dump`).

### 📁 Archivo de respaldo

* **Nombre:** `registro_ucne.sql`
* **Formato:** SQL plano (*Plain SQL*)

### 📦 El respaldo incluye

* Estructura completa de la base de datos

  * Tablas
  * Relaciones
  * Claves primarias y foráneas
* Datos de prueba utilizados por el sistema
* Usuarios, roles y configuraciones iniciales

Este archivo permite **reconstruir completamente la base de datos**, incluso si la instancia original en Neon es eliminada.

---

### 🔄 Restaurar la base de datos

Para restaurar el respaldo en una nueva base de datos PostgreSQL, ejecuta el siguiente comando:

```bash
psql -h TU_HOST -U TU_USUARIO -d TU_DATABASE -f registro_ucne.sql
```

### ⚠️ Importante

* Debes **crear previamente una base de datos vacía** en PostgreSQL antes de ejecutar el comando.
* Asegúrate de que el usuario tenga permisos para:

  * Crear tablas
  * Insertar datos
  * Crear relaciones

---

### 🎓 Nota Académica

La inclusión de este respaldo garantiza que:

* El proyecto puede ser evaluado **sin depender de servicios externos activos**
* La base de datos puede ser restaurada para:

  * Revisión
  * Pruebas
  * Exportación
* Se cumple con el requisito de **acceso temporal a la base de datos**, incluso si el alojamiento remoto deja de estar disponible

---

### ☁️ Almacenamiento de Archivos (Cloudflare R2)

Los documentos PDF se almacenan utilizando **Cloudflare R2**.

Debes completar manualmente esta sección en `appsettings.json`:

```json
"R2": {
  "AccountId": "",
  "AccessKey": "",
  "SecretKey": "",
  "BucketName": "",
  "PublicBaseUrl": ""
}
```

Descripción de campos:

* **AccountId:** ID de la cuenta Cloudflare
* **AccessKey:** Clave de acceso al bucket
* **SecretKey:** Clave secreta
* **BucketName:** Nombre del bucket
* **PublicBaseUrl:** URL pública base para visualizar los PDFs

> ⚠️ **Seguridad:**
> Nunca subas credenciales reales al repositorio.
> Usa `appsettings.Development.json` o variables de entorno.

---

## 👤 Credenciales por Defecto

Al iniciar el proyecto por primera vez, se crea automáticamente un usuario administrador:

* **Usuario:** `admin`
* **Contraseña:** `Admin123*`
* **Rol:** Administrador

> ⚠️ **Recomendación:** cambiar la contraseña al primer inicio en un entorno productivo.

---

## 📦 Almacenamiento de Archivos

Los documentos académicos (PDF) se almacenan en la nube utilizando **Cloudflare R2**, lo que permite:

* Bajo costo de almacenamiento
* Sin costos por tráfico de salida
* Escalabilidad
* Integridad del documento mediante hash SHA256

---

## ⚠️ Limitaciones Conocidas y Aspectos a Mejorar

Aunque el sistema es funcional, presenta algunas **limitaciones conocidas**, propias de un proyecto académico:

* **Importación CSV (Dirección):**
  El campo *Dirección* no admite comas (`,`) dentro del archivo CSV, ya que estas se interpretan como separadores de columnas.

* **Gestión de pagos:**
  Actualmente no se permite adjuntar el **recibo de pago** del estudiante.
  En versiones futuras sería ideal permitir subir este comprobante.

* **Notificaciones a estudiantes:**
  El sistema no incluye notificaciones automáticas.
  Podría mejorarse para notificar:

  * Cambios de estado
  * Disponibilidad del documento
  * Enlaces seguros al PDF

Estas limitaciones representan **oportunidades claras de mejora**.

---

## 🔮 Proyección y Continuidad del Proyecto

Este proyecto fue concebido como una **base sólida y extensible**, diseñada para:

* Continuación por **estudiantes**
* Evolución por **desarrolladores profesionales**
* Futuro uso institucional en la UCNE

Actualmente **no se considera completamente listo para producción**, pero sí un **prototipo académico avanzado**, con arquitectura moderna, buenas prácticas y potencial real de implementación.

---

## 👨‍💻 Equipo de Desarrollo

Proyecto desarrollado por estudiantes de la **Universidad Católica Nordestana (UCNE)**:

* **Adonis Mercado Hidalgo** – UI/UX y desarrollo general
* **James Jesús de Peña Rodríguez** – Backend y arquitectura
* **Jorge Ariel Moya De Peña** – Base de datos y apoyo técnico
* **Juan Pablo Guillén Zorrilla** – Desarrollo y análisis
* **James Enmanuel Ureña Paulino** – Apoyo en desarrollo
* **Luis Ángel Gabriel Morillo** – Funcionalidades y documentación

---

## 📄 Licencia y Uso

Este sistema fue desarrollado como **proyecto académico** para uso interno del Departamento de Registro Académico de la UCNE.

El uso del sistema está regulado por un **Contrato de Licencia de Usuario Final (EULA)** incluido en la documentación del proyecto.

---

## 📌 Notas Finales 

- El sistema está diseñado para ser **escalable y mantenible** 
- Puede servir como base para futuros proyectos académicos o profesionales 
- Representa una solución moderna, segura y de bajo costo para la institución 
