- Domain Models "Core Layer"
- Entities
  - Classes that have a base properties that all the Entities should implement.
- Aggregate
  - Is an Entity that also implements the IAggregate interface which is responsible for Domain layer events creation like AddDomainEvent and Clear Domain Events.
  - These Domain events will later be dispatched in the infrastructure layer using Entity framework interceptors and MediatoR.
- Uses the Rich Model Design.
