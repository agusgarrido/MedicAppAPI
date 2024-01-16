# MedicAppAPI - API de Reserva de Turnos Médicos

MedicAppAPI, la API de reserva de turnos médicos diseñada para ayudarte a gestionar turnos de manera eficiente.

### Primera versión:
- Información sobre médicos, especialidades y filtro por especialidades específicas.

## Endpoints

### Obtener todos los médicos

-   **Endpoint:** `/api/Doctor/`
-   **Método:** GET

### Obtener todas la especialidades

-   **Endpoint:** `/api/Especialidad/`
-   **Método:** GET

### Obtener médicos por especialidad

-   **Endpoint:** `/api/Doctor/filtrar-especialidad/{especialidad}`
-   **Método:** GET