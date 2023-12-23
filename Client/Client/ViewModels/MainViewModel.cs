using System;
using System.Reactive;
using System.Threading.Tasks;
using Client.Models.Api;
using Common.Model;
using ReactiveUI;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase {
    private bool _isLoading;

    public MainViewModel() {
        LoginCommand = ReactiveCommand.CreateFromTask<LoginProvider>(ExecuteLoginCommand);
    }

    public string Greeting => "Welcome to Avalonia!";
    public ReactiveCommand<LoginProvider, Unit> LoginCommand { get; }

    public bool IsLoading {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }


    private Task ExecuteLoginCommand(LoginProvider provider) {
        IsLoading = true;
        switch (provider) {
            case LoginProvider.Google:
                Console.WriteLine("Google");
                IsLoading = false;
                break;
            case LoginProvider.Microsoft:
                Apis.Identity.Test().ContinueWith(task => {
                    Console.WriteLine(task.Result);
                    IsLoading = false;
                }, TaskContinuationOptions.ExecuteSynchronously);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
        }

        return Task.CompletedTask;
    }
}