# MedicAppAPI - API de Reserva de Turnos Médicos

MedicAppAPI, la API de reserva de turnos médicos diseñada para ayudarte a gestionar turnos de manera eficiente.

### Primera versión:
- Información sobre médicos, especialidades y filtro por especialidades específicas.

### Segunda versión:
- Agregar, editar o eliminar médicos y/o especialidades.

### Tercera versión:
- Obtener, agregar, editar o eliminar pacientes.

## Endpoints

### Obtener todos los médicos (V1)

-   **Endpoint:** `/api/Doctor/`
-   **Método:** GET

### Obtener médicos por especialidad (V1)

-   **Endpoint:** `/api/Doctor/filtrar-especialidad/{especialidad}`
-   **Método:** GET

### Agregar médico (V2)

-   **Endpoint:** `/api/Doctor/agregar/`
-   **Método:** POST
	
### Editar médico (V2)

-   **Endpoint:** `/api/Doctor/editar/{doctorId}`
-   **Método:** PUT
-   **Parámetros:**
	- `{doctorId}`: ID del médico que desea editar.
	
### Editar la especialidad de un médico (V2)

-   **Endpoint:** `/api/Doctor/editar-especialidad/{doctorId}`
-   **Método:** PUT
-   **Parámetros:**
	- `{doctorId}`: ID del médico que desea editar.
	
### Eliminar médico (V2)

-   **Endpoint:** `/api/Doctor/eliminar/{doctorId}`
-   **Método:** DELETE
-   **Parámetros:**
	- `{doctorId}`: ID del médico que desea eliminar.

### Obtener todas la especialidades (V1)

-   **Endpoint:** `/api/Especialidad/`
-   **Método:** GET
	
### Agregar especialidad (V2)

-   **Endpoint:** `/api/Especialidad/agregar/`
-   **Método:** POST

### Editar especialidad (V2)

-   **Endpoint:** `/api/Especialidad/editar/{especialidadId}`
-   **Método:** POST
-   **Parámetros:**
	- `{especialidadId}`: ID de la especialidad que desea editar.

### Eliminar especialidad (V2)

-   **Endpoint:** `/api/Especialidad/eliminar/{especialidadId}`
-   **Método:** DELETE
-   **Parámetros:**
	- `{especialidadId}`: ID de la especialidad que desea eliminar.

### Obtener todos los pacientes (V3)

-   **Endpoint:** `/api/Paciente/`
-   **Método:** GET

### Obtener paciente por DNI (V3)

-   **Endpoint:** `/api/Paciente/buscar/{pacienteDNI}`
-   **Método:** GET
-   **Parámetros:**
	- `{pacienteDNI}`: ID del paciente que desea buscar.

### Agregar paciente (V3)

-   **Endpoint:** `/api/Paciente/agregar/`
-   **Método:** POST

### Editar paciente (V3)

-   **Endpoint:** `/api/Paciente/editar/{pacienteDNI}`
-   **Método:** PUT
-   **Parámetros:**
	- `{pacienteDNI}`: DNI del paciente que desea editar.

### Eliminar paciente (V3)

-   **Endpoint:** `/api/Paciente/eliminar/{pacienteDNI}`
-   **Método:** DELETE
-   **Parámetros:**
	- `{pacienteDNI}`: DNI del paciente que desea eliminar.

### Obtener todos los horarios agrupados por médico (V4)

-   **Endpoint:** `/api/Horario/`
-   **Método:** GET

### Filtrar horarios por médico (V4)

-   **Endpoint:** `/api/Horario/filtrar-doctor/{doctorID}`
-   **Método:** GET
-   **Parámetros:**
	- `{doctorID}`: ID del doctor que desea buscar.

### Agregar horario (V4)

-   **Endpoint:** `/api/Horario/agregar/{doctorID}`
-   **Método:** POST
-   **Parámetros:**
	- `{doctorID}`: ID del doctor al cuál desea agregar un nuevo día y horario de atención.

### Editar horario (V4)

-   **Endpoint:** `/api/Horario/editar/{doctorID}/{dia}`
-   **Método:** PATCH
-   **Parámetros:**
	- `{doctorID}`: ID del doctor al cuál desea editar su dia y/u horario de atención.
	- `{dia}`: Día específico que deseas editar del horario del médico.

### Eliminar horario (V4)

-   **Endpoint:** `/api/Horario/eliminar/{doctorID}/{dia}`
-   **Método:** DELETE
-   **Parámetros:**
	- `{doctorID}`: ID del doctor al cuál desea eliminar un día y horario de atención.
	- `{dia}`: Día específico que deseas eliminar del horario del médico.