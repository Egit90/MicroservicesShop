using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;
public class ConfirmationModel() : PageModel
{

    public string Message { get; set; } = default!;

    public void OnGetContact()
    {
        Message = "Your Email was sent";
    }

    public void OnGetOrderSubmitted()
    {
        Message = "Your Order was successfully Submitted";
    }
}
