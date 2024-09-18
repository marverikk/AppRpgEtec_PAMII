using AppRpgEtec.ViewModels.Personagens;

namespace AppRpgEtec.Views.Personagens;

public partial class ListagemView : ContentPage
{
	ListagmPersonagemViewModel viewModel;

	public ListagemView()
	{
		InitializeComponent();

		viewModel = new ListagmPersonagemViewModel();
		BindingContext = viewModel;
		Title = "Pesonagens - App Rpg Etec";
	}
}