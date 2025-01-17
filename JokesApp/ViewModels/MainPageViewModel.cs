﻿using JokesApp.Models;
using JokesApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JokesApp.ViewModels
{
    public class MainPageViewModel:ViewModelBase
    {
        private readonly JokeService service;
        private Joke joke;
        private string setup;
        private string delivery;

        public bool IsVisible { get => joke is TwoPartJoke; }
        public string SetUp { get => setup; set { setup = value; OnPropertyChanged(); } }
        public string Delivery { get => delivery; set { delivery = value; OnPropertyChanged(); } }
        
        public ObservableCollection <string> Categories { get; set; }
        public ICommand GetJokeCommand { get; private set; }

        public ICommand SubmitJokeCommand { get; private set; }
        public ICommand GetCattegoeyCommand {  get; private set; }

        public MainPageViewModel(JokeService service) 
        {
            joke = null;
            this.service=service;
            Categories = new ObservableCollection<string>();
            GetCattegoeyCommand = new Command(async () => { var cat = await service.GetCategoriesasync();Categories.Clear(); foreach (var c in cat) { Categories.Add(c); } });
            GetJokeCommand = new Command(async () =>
            {
                joke = await service.GetRandomJoke();
                if (joke is OneLiner)
                {
                    SetUp =((OneLiner)joke).Joke;
                }
                if (joke is TwoPartJoke)
                {
                    SetUp=((TwoPartJoke)joke).Setup;
                    Delivery= ((TwoPartJoke)joke).Delivery;
                    
                }
              OnPropertyChanged(nameof(IsVisible));

            } );

            SubmitJokeCommand= new Command(async () => { await SubmitJoke(); });
        }

        private async Task SubmitJoke()
        {
           OneLiner j = this.joke as OneLiner; 
           MyJoke joke= new MyJoke() { Flags=j.Flags, Joke=j.Joke };
            if (await service.SubmitJokeAsync(joke))
               await AppShell.Current.DisplayAlert("Ha Ha Ha", "LOL", "Ok");
            else
                await AppShell.Current.DisplayAlert("DUH!", "SAD SO SAD!", "Ok");

        }
    }
}
