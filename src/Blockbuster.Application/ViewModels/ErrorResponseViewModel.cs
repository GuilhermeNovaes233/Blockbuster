namespace Blockbuster.Application.ViewModels
{
    public class ErrorResponseViewModel
    {
        public ErrorResponseViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}