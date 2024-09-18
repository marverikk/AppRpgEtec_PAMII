using AppRpgEtec.Views;
using AppRpgEtec.Views.Usuarios;
namespace AppRpgEtec
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.LoginView());
        }
    }
}
