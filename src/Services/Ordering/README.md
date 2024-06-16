- Microservice responsible for the orders.

  - Clean Architecture
  - Domain Driven Design
  - Domain Events
  - Integration Events

- For Infrastructure:

  - EF Core ORM
  - Raise and dispatch Domain Events with Ef core and MediatoR.

- For Application Layer:

  - CQRS CQS pattern.
  - Command and CommandHandler Pattern.
  - MediatR PipeLine.
  - Fluent Validation.

- For Presentation

  - Minimal API

- Dependencies Between Applications.

  - The Core Layer
    - Domain Layer:
      - No dependencies.
    - Application Layer:
      - Reference the Domain.
  - The periphery Layer "Infrastructure and Presentation":
    - Infrastructure:
      - Reference the Application.
    - Presentation "API":
      - Reference the Infrastructure.
      - Reference the Application.
