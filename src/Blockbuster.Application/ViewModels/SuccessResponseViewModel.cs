namespace Blockbuster.Application.ViewModels
{
    public class SuccessResponseViewModel
    {
        public SuccessResponseViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}