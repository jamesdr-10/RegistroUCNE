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

## 💻 Requisitos del Sistema

### Servidor
- .NET 10 SDK / Runtime
- PostgreSQL 14+
- Windows Server o Linux
- 4 GB RAM mínimo (8 GB recomendado)

### Cliente
- Navegador web moderno (Chrome, Edge, Firefox)
- No requiere instalación local

---

## 👤 Credenciales por Defecto

Al iniciar el proyecto por primera vez, se crea automáticamente un usuario administrador:

- **Usuario:** `admin`  
- **Contraseña:** `Admin123*`  
- **Rol:** Administrador  

> ⚠️ **Recomendación:** cambiar la contraseña al primer inicio en un entorno productivo.

---

## 📦 Almacenamiento de Archivos

Los documentos académicos (PDF) se almacenan en la nube utilizando **Cloudflare R2**, lo que permite:

- Bajo costo de almacenamiento
- Sin costos por tráfico de salida
- Escalabilidad
- Integridad del documento mediante hash

---

## ⚠️ Limitaciones Conocidas y Aspectos a Mejorar

Aunque el sistema es completamente funcional, existen algunas **limitaciones conocidas**, propias de un proyecto académico:

- **Importación CSV (Dirección):**  
  El campo *Dirección* no admite comas (`,`) dentro del archivo CSV, ya que estas se interpretan como separadores de columnas, lo que puede provocar errores de importación.

- **Gestión de pagos:**  
  Actualmente no se permite adjuntar el **recibo de pago** del estudiante a la solicitud.  
  En versiones futuras, sería ideal permitir subir este comprobante para evitar el registro de solicitudes no pagadas.

- **Notificaciones a estudiantes:**  
  El sistema no incluye notificaciones automáticas (correo electrónico u otro medio).  
  Una mejora futura podría notificar:
  - Cambios de estado de la solicitud
  - Disponibilidad del documento final
  - Enlaces seguros al PDF

Estas limitaciones representan **oportunidades claras de mejora**, sin afectar el flujo principal del sistema.

---

## 🔮 Proyección y Continuidad del Proyecto

Este proyecto ha sido concebido como una **base sólida y extensible**, diseñada para:

- Ser continuada por **estudiantes** en futuros proyectos académicos  
- Ser mejorada por **desarrolladores profesionales**  
- Evolucionar progresivamente hacia una solución institucional completa  

Aunque fue diseñado inicialmente para un entorno interno de la UCNE, **actualmente no se considera completamente listo para uso productivo**, debido a la necesidad de:

- Más controles administrativos avanzados  
- Integración de pagos y notificaciones  
- Validaciones adicionales y pruebas en producción  

No obstante, el sistema representa un **prototipo académico avanzado**, con una arquitectura moderna y buenas prácticas, capaz de convertirse en una solución real mediante futuras iteraciones.

---

## 👨‍💻 Equipo de Desarrollo

Proyecto desarrollado por estudiantes de la **Universidad Católica Nordestana (UCNE)**:

- **Adonis Mercado Hidalgo** – UI/UX y desarrollo general  
- **James Jesús de Peña Rodríguez** – Backend y arquitectura  
- **Jorge Ariel Moya De Peña** – Base de datos y apoyo técnico  
- **Juan Pablo Guillén Zorrilla** – Desarrollo y análisis  
- **James Enmanuel Ureña Paulino** – Apoyo en desarrollo  
- **Luis Ángel Gabriel Morillo** – Funcionalidades y documentación  

---

## 📄 Licencia y Uso

Este sistema fue desarrollado como **proyecto académico** para uso interno del Departamento de Registro Académico de la UCNE.

El uso del sistema está regulado por un **Contrato de Licencia de Usuario Final (EULA)** incluido en la documentación del proyecto.

---

## 📌 Notas Finales

- El sistema está diseñado para ser **escalable y mantenible**
- Puede servir como base para futuros proyectos académicos o profesionales
- Representa una solución moderna, segura y de bajo costo para la institución

---

🎓 **Universidad Católica Nordestana (UCNE)**  
Departamento de Registro Académico  
Proyecto Académico
