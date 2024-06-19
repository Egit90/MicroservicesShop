using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;

internal sealed class InitialData
{
    public static IEnumerable<Customer> Customers => [
        Customer.Create(CustomerId.Of(new Guid("1521e8e0-deac-4c99-b1ae-2202a19c640d")),"Elie", "Elie@test.com"),
        Customer.Create(CustomerId.Of(new Guid ("5f5113e9-061a-4663-9a06-32144d5d749e")),"Rebecca" , "Rebecca@test.com")
    ];


    public static IEnumerable<Product> Products => [
        Product.Create(ProductId.Of(new Guid("65bf5365-de92-9220-58b7-0478d52198ab")), "IPhone X" ,500),
        Product.Create(ProductId.Of(new Guid("2da61073-27f4-a095-bedc-7100ad1ba37e")), "Samsung 10" ,400),
        Product.Create(ProductId.Of(new Guid("9596c260-6df9-048f-9533-8c2bb57239ae")), "Huawei Plus" ,500),
        Product.Create(ProductId.Of(new Guid("542a66e0-d728-dd7f-55f4-c1397f79e1a4")), "Xiaomi Mi" ,450),
    ];

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Elie", "Maatouk", "elie@test.com", "9360 dsa", "USA", "FL", "32257");
            var address2 = Address.Of("Rebecca", "Bull", "rebecca@test.com", "9361 dsa", "USA", "FL", "32257");

            var payment1 = Payment.Of("MyCard", "4324100008174822", "12/28", "355", 1);
            var payment2 = Payment.Of("TestCard", "4324100008174822", "12/28", "355", 1);

            var Order1 = Order.Create(OrderId.Of(Guid.NewGuid()),
                                      CustomerId.Of(new Guid("1521e8e0-deac-4c99-b1ae-2202a19c640d")),
                                      OrderName.Of("TestOrder1"),
                                      address1,
                                      address1, payment1);

            Order1.Add(ProductId.Of(new Guid("65bf5365-de92-9220-58b7-0478d52198ab")), 2, 500);



            var Order2 = Order.Create(OrderId.Of(Guid.NewGuid()),
                                      CustomerId.Of(new Guid("5f5113e9-061a-4663-9a06-32144d5d749e")),
                                      OrderName.Of("TestOrder2"),
                                      address2,
                                      address2,
                                      payment2);

            Order1.Add(ProductId.Of(new Guid("542a66e0-d728-dd7f-55f4-c1397f79e1a4")), 1, 450);

            return [Order1, Order2];
        }
    }
}