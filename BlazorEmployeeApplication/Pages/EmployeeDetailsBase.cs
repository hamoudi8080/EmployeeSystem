using BlazorEmployeeApplication.Services;
using EmployeeManagmentModel;
using Microsoft.AspNetCore.Components;

namespace BlazorEmployeeApplication.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string Id { get; set; }




        protected string ButtonText { get; set; } = "Hide Footer";
        protected string CssClass { get; set; } = null;


        protected async override Task OnInitializedAsync()
        {
            //If Id is null, then it gets assigned the value "1".
            Id = Id ?? "1";
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
        }



        protected void Button_Click()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "HideFooter";
            }
            else
            {
                CssClass = null;
                ButtonText = "Hide Footer";
            }
        }

    }
}
