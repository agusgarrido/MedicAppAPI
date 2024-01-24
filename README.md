
# MedicAppAPI - API de Reserva de Turnos M�dicos

MedicAppAPI, la API de reserva de turnos m�dicos dise�ada para ayudarte a gestionar turnos de manera eficiente.

### v1.0.0
- Informaci�n sobre m�dicos, especialidades y filtro por especialidades espec�ficas.

### v1.1.0
- Agregar, editar o eliminar m�dicos y/o especialidades.

### v1.2.0
- Obtener, agregar, editar o eliminar pacientes.

### v1.3.0
- Obtener, filtrar, agregar, editar o eliminar horarios.

### v2.0.0
- Obtener, agregar, editar o eliminar m�dicos. 

### Mejoras de la versi�n 2.0.0 en adelante
- Incluye mejoras significativas en la organizaci�n y claridad del c�digo para el manejo del modelo.
- Refactorizaci�n de �reas para garantizar un mantenimiento m�s sencillo y una mejora en la compresi�n del c�digo.
- Reestructuraci�n de la arquitectura del proyecto para seguir mejores pr�cticas de dise�o y desarrollo.

## Endpoints

### Obtener todos los m�dicos (v2.0.0)

-   **Endpoint:** `/api/Doctor/`
-   **M�todo:** GET

### Obtener un m�dico espec�fico (v2.0.0)

-   **Endpoint:** `/api/Doctor/{doctorId}`
-   **M�todo:** GET
-   **Par�metros:**
	- `{doctorId}`: ID del m�dico que desea buscar.

### Agregar m�dico (v2.0.0)

-   **Endpoint:** `/api/Doctor/agregar/`
-   **M�todo:** POST
	
### Editar m�dico (v2.0.0)

-   **Endpoint:** `/api/Doctor/editar/{doctorId}`
-   **M�todo:** PUT
-   **Par�metros:**
	- `{doctorId}`: ID del m�dico que desea editar.
	
### Eliminar m�dico (v2.0.0)

-   **Endpoint:** `/api/Doctor/eliminar/{doctorId}`
-   **M�todo:** GET
-   **Par�metros:**
	- `{doctorId}`: ID del m�dico que desea eliminar.

### Filtrar horarios por m�dico (v1.3.0)

-   **Endpoint:**  `/api/Horario/filtrar-doctor/{doctorID}`
-   **M�todo:**  GET
-   **Par�metros:**
    -   `{doctorID}`: ID del doctor que desea buscar.

### Agregar horario (v1.3.0)

-   **Endpoint:**  `/api/Horario/agregar/{doctorID}`
-   **M�todo:**  POST
-   **Par�metros:**
    -   `{doctorID}`: ID del doctor al cu�l desea agregar un nuevo d�a y horario de atenci�n.

### Editar horario (v1.3.0)

-   **Endpoint:**  `/api/Horario/editar/{doctorID}/{dia}`
-   **M�todo:**  PATCH
-   **Par�metros:**
    -   `{doctorID}`: ID del doctor al cu�l desea editar su dia y/u horario de atenci�n.
    -   `{dia}`: D�a espec�fico que deseas editar del horario del m�dico.

### Eliminar horario (v1.3.0)

-   **Endpoint:**  `/api/Horario/eliminar/{doctorID}/{dia}`
-   **M�todo:**  DELETE
-   **Par�metros:**
    -   `{doctorID}`: ID del doctor al cu�l desea eliminar un d�a y horario de atenci�n.
    -   `{dia}`: D�a espec�fico que deseas eliminar del horario del m�dico.

### Obtener todos los pacientes (v1.2.0)

-   **Endpoint:** `/api/Paciente/`
-   **M�todo:** GET

### Obtener paciente por DNI (v1.2.0)

-   **Endpoint:** `/api/Paciente/buscar/{pacienteDNI}`
-   **M�todo:** GET
-   **Par�metros:**
	- `{pacienteDNI}`: ID del paciente que desea buscar.

### Agregar paciente (v1.2.0)

-   **Endpoint:** `/api/Paciente/agregar/`
-   **M�todo:** POST

### Editar paciente (v1.2.0)

-   **Endpoint:** `/api/Paciente/editar/{pacienteDNI}`
-   **M�todo:** PUT
-   **Par�metros:**
	- `{pacienteDNI}`: DNI del paciente que desea editar.

### Eliminar paciente (v1.2.0)

-   **Endpoint:** `/api/Paciente/eliminar/{pacienteDNI}`
-   **M�todo:** DELETE
-   **Par�metros:**
	- `{pacienteDNI}`: DNI del paciente que desea eliminar.

### Obtener todas la especialidades (v1.1.0)

-   **Endpoint:** `/api/Especialidad/`
-   **M�todo:** GET
	
### Agregar especialidad (v1.1.0)

-   **Endpoint:** `/api/Especialidad/agregar/`
-   **M�todo:** POST

### Editar especialidad (v1.1.0)

-   **Endpoint:** `/api/Especialidad/editar/{especialidadId}`
-   **M�todo:** POST
-   **Par�metros:**
	- `{especialidadId}`: ID de la especialidad que desea editar.

### Eliminar especialidad (v1.1.0)

-   **Endpoint:** `/api/Especialidad/eliminar/{especialidadId}`
-   **M�todo:** DELETE
-   **Par�metros:**
	- `{especialidadId}`: ID de la especialidad que desea eliminar.
