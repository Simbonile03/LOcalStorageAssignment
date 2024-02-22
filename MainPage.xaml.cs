using Microsoft.Maui;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LOcalStorageAssignment
{
    public partial class ProfilePage : ContentPage
    {
        private const string ProfileFileName = "profile.json";

        public ProfilePage()
        {
            InitializeComponent();
            LoadProfileData();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            {
               SaveProfileToFileAsync(ProfileFileName);
                DisplayAlert("Button Clicked", "Profile saved successfully!!", "OK");
            }

            var profile = new Profile
            {
                Name = NameEntry.Text,
                Surname = SurnameEntry.Text,
                Bio = BioEditor.Text
            };

            
            var json = JsonSerializer.Serialize(profile);

           
            await SaveProfileToFileAsync(json);
        }

        private async void LoadProfileData()
        {
            try
            {
                
                var json = await ReadProfileFromFileAsync();

                
                var profile = JsonSerializer.Deserialize<Profile>(json);

                
                NameEntry.Text = profile?.Name;
                SurnameEntry.Text = profile?.Surname;
                BioEditor.Text = profile?.Bio;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error loading profile: {ex.Message}");
            }
        }

        private async Task SaveProfileToFileAsync(string json)
        {
            var file = Path.Combine(FileSystem.AppDataDirectory, ProfileFileName);
            await File.WriteAllTextAsync(file, json);
        }

        private async Task<string> ReadProfileFromFileAsync()
        {
            var file = Path.Combine(FileSystem.AppDataDirectory, ProfileFileName);
            return await File.ReadAllTextAsync(file);
        }
    }

    public class Profile
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
    }
}







