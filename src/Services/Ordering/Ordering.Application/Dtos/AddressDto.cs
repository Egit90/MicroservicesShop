namespace Ordering.Application.Dtos;

public sealed record AddressDto(
    string FirstName,
    string LastName,
    string Country,
    string State,
    string ZipCode
);