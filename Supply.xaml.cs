using PR2024.Controllers;
using PR2024.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
	/// Логика взаимодействия для Supply.xaml
	/// </summary>
	public partial class Supply : Window
	{
		public class SupplyForView
		{
			public int IdSupply { get; set; }
			public int IdAgent { get; set; }
			public string Agent { get; set; }
			public int IdClient { get; set; }
			public string Client { get; set; }
			public int IdRealEstate { get; set; }
			public string RealEstate { get; set; }
			public int Price { get; set; }
		}
		public Supply()
		{
			InitializeComponent();
			InitializeComponent();
			LoadGrid();
			LoadCbAgent();
			LoadCbClient();
			LoadCbRealEstate();
			SupplyDataGrid.ItemsSource = itemsSFV;
		}
		ObservableCollection<SupplyForView> itemsSFV = new ObservableCollection<SupplyForView>();
		private Connection _connection = new Connection();
		// private SupplyForView _supplyForView = new SupplyForView();
		DataTable _supplyTable = new DataTable();
		SupplyController SupplyController = new SupplyController();
		
		public void LoadCbAgent()
		{
			AgentCb.ItemsSource = _connection.PR.Agents.ToList();
			AgentCb.DisplayMemberPath = "FirstName";
			AgentCb.SelectedValuePath = "Id_Agent";
		}
		public void LoadCbClient()
		{
			ClientCb.ItemsSource = _connection.PR.Clients.ToList();
			ClientCb.DisplayMemberPath = "FirstName";
			ClientCb.SelectedValuePath = "Id_Client";
		}
		public void LoadCbRealEstate()
		{
			RealEstateCb.ItemsSource = _connection.PR.RealEstates.ToList();
			RealEstateCb.DisplayMemberPath = "Adress_Street";
			RealEstateCb.SelectedValuePath = "Id_RealEstate";
		}
		public void LoadGrid()
		{
			Connection connection = new Connection();
			var supply = connection.PR.Supply.ToList();
			var agent = connection.PR.Agents.ToList();
			var client = connection.PR.Clients.ToList();
			var realState = connection.PR.RealEstates.ToList();
			foreach (var s in supply)
			{
				var findagent = agent.FirstOrDefault(x => x.Id_Agent == s.Id_Agent);
				var findRealState = realState.FirstOrDefault(x => x.Id_RealEstate == s.Id_RealEstate);
				var findClient = client.FirstOrDefault(x => x.Id_Client == s.Id_Client);
				SupplyForView supplyForView = new SupplyForView();

				supplyForView.Agent = findagent.LastName + " " + findagent.FirstName + " " + findagent.MiddleName;
				supplyForView.IdAgent = findagent.Id_Agent;

				supplyForView.Client = findClient.LastName + " " + findClient.FirstName + " " + findClient.MiddleName;
				supplyForView.IdClient = findClient.Id_Client;

				supplyForView.RealEstate = findRealState.Adress_City + " " + findRealState.Adress_Street + " " + findRealState.Adress_House;
				supplyForView.IdRealEstate = findRealState.Id_RealEstate;

				supplyForView.Price = s.Price;

				supplyForView.IdSupply = s.Id_Supply;

				itemsSFV.Add(supplyForView);
				SupplyDataGrid.ItemsSource = itemsSFV;
			}
		}
		private void LoadDataFromDatabase()
		{

		}
		private Model.Supply _selectedSupply;
		public int selectedRealEstateId;
		int idSupply;

		private void SupplyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SupplyDataGrid.SelectedItems != null)
			{
				SupplyForView row = (SupplyForView)SupplyDataGrid.SelectedItem;

				if (row != null)
				{
					idSupply = row.IdSupply;
					foreach (Agents item in AgentCb.Items)
					{
						if (item.Id_Agent == row.IdAgent)
						{
							AgentCb.SelectedItem = item;
							break;
						}
					}
					foreach (RealEstates item in RealEstateCb.Items)
					{
						if (item.Id_RealEstate == row.IdRealEstate)
						{
							RealEstateCb.SelectedItem = item;
							break;
						}
					}
					foreach (Clients item in ClientCb.Items)
					{
						if (item.Id_Client == row.IdClient)
						{
							ClientCb.SelectedItem = item;
							break;
						}
					}
					PriceTextBox.Text = row.Price.ToString();
				}
			}
		}

		private void Update_Click(object sender, RoutedEventArgs e)
		{
			//if (SupplyDataGrid.SelectedItems != null)
			//{
				Agents agent = AgentCb.SelectedItem as Agents;
				Clients client = ClientCb.SelectedItem as Clients;
				RealEstates realEstate = RealEstateCb.SelectedItem as RealEstates;

				Model.Supply supply = new Model.Supply();

				supply.Price = Convert.ToInt32(PriceTextBox.Text);
				supply.Id_Supply = idSupply;
				supply.Id_Agent = agent.Id_Agent;
				supply.Id_Client = client.Id_Client;
				supply.Id_RealEstate = realEstate.Id_RealEstate;
				SupplyController.UpdateSupply(idSupply, supply);
			//}
			itemsSFV.Clear();
			LoadGrid();
			PriceTextBox.Text = null;
			AgentCb.SelectedItem = null;
			ClientCb.SelectedItem = null;
			RealEstateCb.SelectedItem = null;
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (SupplyDataGrid.SelectedItems != null)
			{
				SupplyController.DeleteSupply(idSupply);
			}
			itemsSFV.Clear();
			LoadGrid();
			AgentCb.SelectedItem = null;
			ClientCb.SelectedItem = null;
			PriceTextBox.Text = null;
			RealEstateCb.SelectedItem = null;
		}

		private void AddWindow_Click(object sender, RoutedEventArgs e)
		{
			Agents agent = AgentCb.SelectedItem as Agents;
			Clients client = ClientCb.SelectedItem as Clients;
			RealEstates realEstate = RealEstateCb.SelectedItem as RealEstates;

			Model.Supply supply = new Model.Supply();

			supply.Price = Convert.ToInt32(PriceTextBox.Text);
			supply.Id_Agent = agent.Id_Agent;
			supply.Id_Client = client.Id_Client;
			supply.Id_RealEstate = realEstate.Id_RealEstate;
			SupplyController.AddSupply(supply);
			itemsSFV.Clear();
			LoadGrid();
			PriceTextBox.Text = null;
			AgentCb.SelectedItem = null;
			ClientCb.SelectedItem = null;
			RealEstateCb.SelectedItem = null;
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{

		}
		public int updateRealEstateId;

		private void LoadRealEstates()
		{

		}
		private void ClientCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void RealEstateCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			LoadRealEstates();
		}

		private void AgentBtn_Click(object sender, RoutedEventArgs e)
		{
			Agent agent = new Agent();
			this.Close();
			agent.Show();
		}

		private void DealBtn_Click(object sender, RoutedEventArgs e)
		{
			Deal deal = new Deal();
			this.Close();
			deal.Show();
		}
	}
}
