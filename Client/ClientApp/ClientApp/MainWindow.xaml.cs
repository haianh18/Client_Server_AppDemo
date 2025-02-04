using Models;
using Models.DTO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ServerConnection serverConnection;
        private List<Product> products;
        private List<Category> categories;
        private Product? selectedProduct;
        public MainWindow()
        {
            InitializeComponent();
            serverConnection = new ServerConnection();
            products = new List<Product>();
            categories = new List<Category>();
            Window_Loaded();
        }

        private void Window_Loaded()
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            if (cbCategory.Items.Count > 0)
            {
                cbCategory.Items.Clear();
            }
            var allCateItem = new ComboBoxItem { Content = "All Products" };
            cbCategory.Items.Add(allCateItem);
            cbCategory.SelectedIndex = 0;

            RequestDTO request = new RequestDTO
            {
                ActionType = "GetCategories",
                Data = null
            };
            var response = serverConnection.SendRequest(request);
            if (response != null && response.Status == "Success")
            {
                if (response.Data != null)
                {
                    try
                    {
                        categories = JsonSerializer.Deserialize<List<Category>>(response.Data.ToString()!) ?? new List<Category>();
                        foreach (var category in categories)
                        {
                            var cbItem = new ComboBoxItem
                            {
                                Content = category.Name,
                                Tag = category.Id
                            };
                            cbCategory.Items.Add(cbItem);
                        }
                    }
                    catch (JsonException ex)
                    {
                        MessageBox.Show($"Error deserializing JSON: {ex.Message}");
                        Console.WriteLine(response.Data.ToString());
                    }

                }
            }
            else
            {
                MessageBox.Show(response?.Message ?? "Unknown error occurred.");
            }
        }

        private void LoadProducts()
        {
            RequestDTO request = new RequestDTO
            {
                ActionType = "GetProducts",
                Data = null
            };
            var response = serverConnection.SendRequest(request);
            if (response != null && response.Status == "Success")
            {
                if (response.Data != null)
                {
                    try
                    {
                        var data = response.Data.ToString();
                        products = JsonSerializer.Deserialize<List<Product>>(data!) ?? new List<Product>();
                        ProductListView.ItemsSource = products;
                    }
                    catch (JsonException ex)
                    {
                        MessageBox.Show($"Error deserializing JSON: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show(response?.Message ?? "Unknown error occurred.");
            }
        }



        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategory.SelectedItem is ComboBoxItem cbItem)
            {
                if (cbItem.Content.ToString() == "All Products")
                {
                    LoadProducts();
                    return;
                }
                else if (cbItem.Tag is int categoryId)
                {
                    RequestDTO request = new RequestDTO
                    {
                        ActionType = "GetProductsByCategoryId",
                        Data = categoryId
                    };
                    var response = serverConnection.SendRequest(request);
                    if (response != null && response.Status == "Success")
                    {
                        if (response.Data != null)
                        {
                            products = JsonSerializer.Deserialize<List<Product>>(response.Data.ToString()!) ?? new List<Product>();
                            ProductListView.ItemsSource = products;
                        }
                    }
                    else
                    {
                        MessageBox.Show(response?.Message ?? "Unknown error occurred.");
                    }
                }

            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWindow();
            addWindow.OnProductAdded += () =>
            {
                LoadProducts();
            };
            addWindow.ShowDialog();
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListView.SelectedItems == null)
            {
                MessageBox.Show("Please select a product to edit.");
                return;
            }
            var selectedProduct = ProductListView.SelectedItem as Product;
            var editWindow = new EditWindow(selectedProduct);
            editWindow.OnProductUpdated += () =>
            {
                LoadProducts();
            };
            editWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListView.SelectedItems != null)
            {
                selectedProduct = ProductListView.SelectedItem as Product;
                RequestDTO request = new RequestDTO
                {
                    ActionType = "DeleteProduct",
                    Data = selectedProduct
                };
                var result = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                var response = serverConnection.SendRequest(request);
                if (response != null && response.Status == "Success")
                {
                    MessageBox.Show("Product deleted successfully.");
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show(response?.Message ?? "Unknown error occurred.");
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }
    }
}