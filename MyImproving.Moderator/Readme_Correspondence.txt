Create a ViewModelLocator in App.xaml:

<Application ...
             xmlns:vm="clr-namespace:MyImproving.Moderator.ViewModels"
             >

    <Application.Resources>
        <vm:ViewModelLocator x:Key="Locator"/>
    </Application.Resources>

Reverence it in MainPage.xaml:

<UserControl 
    ...
    DataContext="{Binding Main, Source={StaticResource Locator}}">
