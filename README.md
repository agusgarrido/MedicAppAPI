# MedicAppAPI - API de Reserva de Turnos Médicos

MedicAppAPI, la API de reserva de turnos médicos diseñada para ayudarte a gestionar turnos de manera eficiente.

### Primera versión:
- Información sobre médicos, especialidades y filtro por especialidades específicas.

### Segunda versión:
- Agregar, editar o eliminar médicos y/o especialidades.

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