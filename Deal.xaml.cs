using PR2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PR2024
{
	/// <summary>
	/// Логика взаимодействия для Deal.xaml
	/// </summary>
	public partial class Deal : Window
	{
		public Connection connection = new Connection();
		public Model.Supply supply = new Model.Supply();
		public Demands demand = new Demands();
		public Deal()
		{
			InitializeComponent();
			SupplyDataGrid.ItemsSource = connection.PR.Supply.ToList();
			DemandDataGrid.ItemsSource = connection.PR.Demands.ToList();
		}

		private void AddDeal_Click(object sender, RoutedEventArgs e)
		{
			Deals deals = new Deals();
			deals.Id_Demand = Convert.ToInt32(DemandPercentTextBox.Text);
			deals.Id_Supply = Convert.ToInt32(SupplyPercentTextBox.Text);
			connection.PR.Deals.Add(deals);
			connection.PR.SaveChanges();
			SupplyPercentTextBox.Text = null;
			DemandPercentTextBox.Text = null;
		}

		private void SupplyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			supply = SupplyDataGrid.SelectedItem as Model.Supply;
			SupplyPercentTextBox.Text = supply.Id_Supply.ToString();
		}

		private void DemandDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			demand = DemandDataGrid.SelectedItem as Demands;
			DemandPercentTextBox.Text = demand.Id_Demand.ToString();
		}

		private void AgentBtn_Click(object sender, RoutedEventArgs e)
		{
			Agent agent = new Agent();
			this.Close();
			agent.Show();
        }

		private void SupplyBtn_Click(object sender, RoutedEventArgs e)
		{
			Supply supply = new Supply();
			this.Close();
			supply.Show();
        }
    }
}
