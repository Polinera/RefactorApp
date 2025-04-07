using RefactorApp.Models.Models;
using System.Collections.ObjectModel;
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
    new WelcomeSlideModel
    {
        Title = "Welcome to SelfRefactor",
        Description = "Unlock your potential and transform your goals into reality."
    },
    new WelcomeSlideModel
    {
        Title = "Set Meaningful Goals",
        Description = "Define your vision and break it down into achievable steps."
    },
    new WelcomeSlideModel
    {
        Title = "Track Your Progress",
        Description = "Monitor your journey, celebrate milestones, and adjust your approach as needed."
    },
    new WelcomeSlideModel
    {
        Title = "Inspire Yourself",
        Description = "Explore inspiring stories and insights to keep you motivated along the way."
    },
    new WelcomeSlideModel
    {
        Title = "Start Your Transformation",
        Description = "Begin your journey of self-growth and achievement with SelfRefactor."
    }
};

            CompleteCommand = new Command(async () =>
            {
                Preferences.Set("IsFirstTimeUser", false);
                await Shell.Current.GoToAsync("//home");
            });
        }
    }
}