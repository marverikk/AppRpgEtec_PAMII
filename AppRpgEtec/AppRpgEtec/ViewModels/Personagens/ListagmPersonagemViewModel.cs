using AppRpgEtec.Services.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using System.Collections.ObjectModel;
using AppRpgEtec.Models;

using AppRpgEtec.Services;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class ListagmPersonagemViewModel : BaseViewModels
    {
        private PersonagemServices pService;
        public ObservableCollection<Personagem> Personagens { get; set; }
        
        
        public ListagmPersonagemViewModel(){

            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemServices(token);
            Personagens = new ObservableCollection<Personagem>();

            _ = ObterPersonagens();

        }

        public async Task ObterPersonagens()
        {
            try
            {

                Personagens = await pService.GetPersonagensAsync();
                OnPropertyChanged(nameof(Personagens));
            }
            catch (Exception ex)
            {

                {
                    await Application.Current.MainPage

            .DisplayAlert("Ops", ex.Message + "Detalhes:" +
            ex.InnerException, "ok");
                }

            }

        }

    }

}
