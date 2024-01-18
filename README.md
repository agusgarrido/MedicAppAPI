# MedicAppAPI - API de Reserva de Turnos M�dicos

MedicAppAPI, la API de reserva de turnos m�dicos dise�ada para ayudarte a gestionar turnos de manera eficiente.

### Primera versi�n:
- Informaci�n sobre m�dicos, especialidades y filtro por especialidades espec�ficas.

### Segunda versi�n:
- Agregar, editar o eliminar m�dicos y/o especialidades.

### Tercera versi�n:
- Obtener, agregar, editar o eliminar pacientes.

## Endpoints

### Obtener todos los m�dicos (V1)

-   **Endpoint:** `/api/Doctor/`
-   **M�todo:** GET

### Obtener m�dicos por especialidad (V1)

-   **Endpoint:** `/api/Doctor/filtrar-especialidad/{especialidad}`
-   **M�todo:** GET

### Agregar m�dico (V2)

-   **Endpoint:** `/api/Doctor/agregar/`
-   **M�todo:** POST
	
### Editar m�dico (V2)

-   **Endpoint:** `/api/Doctor/editar/{doctorId}`
-   **M�todo:** PUT
-   **Par�metros:**
	- `{doctorId}`: ID del m�dico que desea editar.
	
### Editar la especialidad de un m�dico (V2)

-   **Endpoint:** `/api/Doctor/editar-especialidad/{doctorId}`
-   **M�todo:** PUT
-   **Par�metros:**
	- `{doctorId}`: ID del m�dico que desea editar.
	
### Eliminar m�dico (V2)

-   **Endpoint:** `/api/Doctor/eliminar/{doctorId}`
-   **M�todo:** DELETE
-   **Par�metros:**
	- `{doctorId}`: ID del m�dico que desea eliminar.

### Obtener todas la especialidades (V1)

-   **Endpoint:** `/api/Especialidad/`
-   **M�todo:** GET
	
### Agregar especialidad (V2)

-   **Endpoint:** `/api/Especialidad/agregar/`
-   **M�todo:** POST

### Editar especialidad (V2)

-   **Endpoint:** `/api/Especialidad/editar/{especialidadId}`
-   **M�todo:** POST
-   **Par�metros:**
	- `{especialidadId}`: ID de la especialidad que desea editar.

### Eliminar especialidad (V2)

-   **Endpoint:** `/api/Especialidad/eliminar/{especialidadId}`
-   **M�todo:** DELETE
-   **Par�metros:**
	- `{especialidadId}`: ID de la especialidad que desea eliminar.

### Obtener todos los pacientes (V3)

-   **Endpoint:** `/api/Paciente/`
-   **M�todo:** GET

### Obtener paciente por DNI (V3)

-   **Endpoint:** `/api/Paciente/buscar/{pacienteDNI}`
-   **M�todo:** GET
-   **Par�metros:**
	- `{pacienteDNI}`: ID del paciente que desea buscar.

### Agregar paciente (V3)

-   **Endpoint:** `/api/Paciente/agregar/`
-   **M�todo:** POST

### Editar paciente (V3)

-   **Endpoint:** `/api/Paciente/editar/{pacienteDNI}`
-   **M�todo:** PUT
-   **Par�metros:**
	- `{pacienteDNI}`: DNI del paciente que desea editar.

### Eliminar paciente (V3)

-   **Endpoint:** `/api/Paciente/eliminar/{pacienteDNI}`
-   **M�todo:** DELETE
-   **Par�metros:**
	- `{pacienteDNI}`: DNI del paciente que desea eliminar.