using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Views.Usuarios;
using System.Net.Http.Headers;
using AppRpgEtec.Views.Personagens;


namespace AppRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModels
    {
        private UsuarioService _uService;
        public ICommand AutenticarCommand { get; set; }
        
        public ICommand DirecionarCadastroCommand { get; set; }

        public ICommand RegistrarCommand { get; set; }

        public UsuarioViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }


            public void InicializarCommands() 
            {
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
           
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
            }

        #region AtributoPropriedades
        private string login = string.Empty;
        private string senha = string.Empty;

        //CTRL +R+E
        

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }


        
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = login;
                u.PasswordString = senha;
                Usuario uAutenticado = await _uService.PostAutenticarUsuarioAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"bem vindo {u.Username}";
                    Preferences.Set("UsuarioToken", uAutenticado.Token);
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);


                    Application.Current.MainPage = new ListagemView();
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados Incorretos", "OK");
                }
            }




            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("informação", ex.Message + "Detalhes:" + ex.InnerException, "ok");
            }
        }
            

                

           
        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CadastroView());
               

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("informação", ex.Message + "Detalhes:" + ex.InnerException, "ok");
            }
        }

            public async Task RegistrarUsuario()
            {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uRegistrado = await _uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0)
                {
                    string mensagem = $"Usuario Id {uRegistrado.Id} registrado com sucesso,";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    await Application.Current.MainPage
                        .Navigation.PopAsync();//Remove a página da pilha de visualização 

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "ou");

            }
        }
    }
}

//teste brainch