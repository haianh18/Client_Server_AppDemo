using Models;
using Models.DTO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly ServerConnection serverConnection;
        private Category selectedCategory;
        public event Action OnProductAdded;
        public AddWindow()
        {
            InitializeComponent();
            serverConnection = new ServerConnection();
            LoadData();
        }

        private void LoadData()
        {
            var request = new RequestDTO
            {
                ActionType = "GetCategories",
                Data = null
            };
            var response = serverConnection.SendRequest(request);
            if (response != null && response.Status == "Success")
            {
                if (response.Data != null)
                {
                    var categories = JsonSerializer.Deserialize<List<Category>>(response.Data.ToString());
                    CategoryComboBox.ItemsSource = categories;
                }
            }
            else
            {
                MessageBox.Show("Failed to load Category");
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = (Category)CategoryComboBox.SelectedItem;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(PriceTextBox.Text))
            {
                MessageBox.Show("Name and Price are required");
                return;
            }
            if (selectedCategory == null)
            {
                MessageBox.Show("Category is required");
                return;
            }
            if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Price must be a number");
                return;
            }
            var product = new Product
            {
                Name = NameTextBox.Text,
                Price = price,
                CategoryId = selectedCategory.Id
            };
            var request = new RequestDTO
            {
                ActionType = "AddProduct",
                Data = product
            };
            var response = serverConnection.SendRequest(request);
            if (response != null && response.Status == "Success")
            {
                OnProductAdded?.Invoke();
                MessageBox.Show("Product added successfully");
                Close();
            }
            else
            {
                MessageBox.Show("Failed to add product");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
