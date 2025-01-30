using Newtonsoft.Json;
using ReactiveUI;
using RefactorApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorApp.Core.ViewModels
{
    public class DetailedHistoryViewModel : ReactiveObject
    {
        private string _name;
        private string _description;
        private string _image;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public string Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        public async Task LoadPersonData(string routeId)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("historyPeople.json");
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();
                var items = JsonConvert.DeserializeObject<List<HistoryModel>>(json);

                var person = items?.FirstOrDefault(p => p.RouteTo.EndsWith(routeId));
                if (person != null)
                {
                    Name = person.Name;
                    Description = person.Description;
                    Image = person.Image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON in DetailedView: {ex.Message}");
            }
        }
    }
}