- Application Logic "Core Layer"

  - Classes for Command And Query Handling
  - Classes for DTO
  - Interfaces

- CQRS using MediatoR.

- Notes
  - IApplicationDbContext:
    - is an interface that we will use so we don't reference the infrastructure code directly.
    - It uses the `Microsoft.EntityFrameworkCore` as abstraction for `DbSet`.
