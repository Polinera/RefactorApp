using RefactorApp.Core.Services;
using RefactorApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RefactorApp.Core.ViewModels
{
    public class OnboardingViewModel
    {
        public ObservableCollection<WelcomeSlideModel> WelcomeSlides { get; set; }
        public ICommand CompleteCommand { get; }

        public OnboardingViewModel()
        {
            WelcomeSlides = new ObservableCollection<WelcomeSlideModel>
            {
                new WelcomeSlideModel { Title = "weety", Description = "kjkk" },
                new WelcomeSlideModel { Title = "nvn", Description = "ncnnn" },
                new WelcomeSlideModel { Title = "dghdhdhd", Description = "hgjjjj" }
            };

            CompleteCommand = new Command(async () =>
            {
                Preferences.Set("IsFirstTimeUser", false);
                await Shell.Current.GoToAsync("//home");
            });
        }
    }
}