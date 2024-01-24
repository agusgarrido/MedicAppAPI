
# MedicAppAPI - API de Reserva de Turnos Médicos

MedicAppAPI, la API de reserva de turnos médicos diseñada para ayudarte a gestionar turnos de manera eficiente.

### v1.0.0
- Información sobre médicos, especialidades y filtro por especialidades específicas.

### v1.1.0
- Agregar, editar o eliminar médicos y/o especialidades.

### v1.2.0
- Obtener, agregar, editar o eliminar pacientes.

### v1.3.0
- Obtener, filtrar, agregar, editar o eliminar horarios.

### v2.0.0
- Obtener, agregar, editar o eliminar médicos. 

### Mejoras de la versión 2.0.0 en adelante
- Incluye mejoras significativas en la organización y claridad del código para el manejo del modelo.
- Refactorización de áreas para garantizar un mantenimiento más sencillo y una mejora en la compresión del código.
- Reestructuración de la arquitectura del proyecto para seguir mejores prácticas de diseño y desarrollo.

## Endpoints

### Obtener todos los médicos (v2.0.0)

-   **Endpoint:** `/api/Doctor/`
-   **Método:** GET

### Obtener un médico específico (v2.0.0)

-   **Endpoint:** `/api/Doctor/{doctorId}`
-   **Método:** GET
-   **Parámetros:**
	- `{doctorId}`: ID del médico que desea buscar.

### Agregar médico (v2.0.0)

-   **Endpoint:** `/api/Doctor/agregar/`
-   **Método:** POST
	
### Editar médico (v2.0.0)

-   **Endpoint:** `/api/Doctor/editar/{doctorId}`
-   **Método:** PUT
-   **Parámetros:**
	- `{doctorId}`: ID del médico que desea editar.
	
### Eliminar médico (v2.0.0)

-   **Endpoint:** `/api/Doctor/eliminar/{doctorId}`
-   **Método:** GET
-   **Parámetros:**
	- `{doctorId}`: ID del médico que desea eliminar.

### Filtrar horarios por médico (v1.3.0)

-   **Endpoint:**  `/api/Horario/filtrar-doctor/{doctorID}`
-   **Método:**  GET
-   **Parámetros:**
    -   `{doctorID}`: ID del doctor que desea buscar.

### Agregar horario (v1.3.0)

-   **Endpoint:**  `/api/Horario/agregar/{doctorID}`
-   **Método:**  POST
-   **Parámetros:**
    -   `{doctorID}`: ID del doctor al cuál desea agregar un nuevo día y horario de atención.

### Editar horario (v1.3.0)

-   **Endpoint:**  `/api/Horario/editar/{doctorID}/{dia}`
-   **Método:**  PATCH
-   **Parámetros:**
    -   `{doctorID}`: ID del doctor al cuál desea editar su dia y/u horario de atención.
    -   `{dia}`: Día específico que deseas editar del horario del médico.

### Eliminar horario (v1.3.0)

-   **Endpoint:**  `/api/Horario/eliminar/{doctorID}/{dia}`
-   **Método:**  DELETE
-   **Parámetros:**
    -   `{doctorID}`: ID del doctor al cuál desea eliminar un día y horario de atención.
    -   `{dia}`: Día específico que deseas eliminar del horario del médico.

### Obtener todos los pacientes (v1.2.0)

-   **Endpoint:** `/api/Paciente/`
-   **Método:** GET

### Obtener paciente por DNI (v1.2.0)

-   **Endpoint:** `/api/Paciente/buscar/{pacienteDNI}`
-   **Método:** GET
-   **Parámetros:**
	- `{pacienteDNI}`: ID del paciente que desea buscar.

### Agregar paciente (v1.2.0)

-   **Endpoint:** `/api/Paciente/agregar/`
-   **Método:** POST

### Editar paciente (v1.2.0)

-   **Endpoint:** `/api/Paciente/editar/{pacienteDNI}`
-   **Método:** PUT
-   **Parámetros:**
	- `{pacienteDNI}`: DNI del paciente que desea editar.

### Eliminar paciente (v1.2.0)

-   **Endpoint:** `/api/Paciente/eliminar/{pacienteDNI}`
-   **Método:** DELETE
-   **Parámetros:**
	- `{pacienteDNI}`: DNI del paciente que desea eliminar.

### Obtener todas la especialidades (v1.1.0)

-   **Endpoint:** `/api/Especialidad/`
-   **Método:** GET
	
### Agregar especialidad (v1.1.0)

-   **Endpoint:** `/api/Especialidad/agregar/`
-   **Método:** POST

### Editar especialidad (v1.1.0)

-   **Endpoint:** `/api/Especialidad/editar/{especialidadId}`
-   **Método:** POST
-   **Parámetros:**
	- `{especialidadId}`: ID de la especialidad que desea editar.

### Eliminar especialidad (v1.1.0)

-   **Endpoint:** `/api/Especialidad/eliminar/{especialidadId}`
-   **Método:** DELETE
-   **Parámetros:**
	- `{especialidadId}`: ID de la especialidad que desea eliminar.
