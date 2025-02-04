using Models;
using Models.DTO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly ServerConnection serverConnection;
        private Product selectedProduct;
        private Category selectedCategory;
        public event Action OnProductUpdated;
        public EditWindow(Product pro)
        {
            InitializeComponent();
            serverConnection = new ServerConnection();
            selectedProduct = pro;
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
                    CategoryComboBox.SelectedItem = categories.FirstOrDefault(c => c.Id == selectedProduct.CategoryId);
                }
            }
            else
            {
                MessageBox.Show("Failed to load Category");
            }
            NameTextBox.Text = selectedProduct.Name;
            PriceTextBox.Text = selectedProduct.Price.ToString();
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
            var request = new RequestDTO
            {
                ActionType = "EditProduct",
                Data = JsonSerializer.Serialize(new Product
                {
                    Id = selectedProduct.Id,
                    Name = NameTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    CategoryId = selectedCategory.Id
                })
            };
            var response = serverConnection.SendRequest(request);
            if (response != null && response.Status == "Success")
            {
                MessageBox.Show("Product updated successfully");
                OnProductUpdated?.Invoke();
                Close();
            }
            else
            {
                MessageBox.Show("Failed to update Product");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
